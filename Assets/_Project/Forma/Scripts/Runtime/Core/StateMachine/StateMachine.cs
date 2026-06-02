using System;
using System.Collections.Generic;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Transitions;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.StateMachine
{
    public class StateMachine
    {
        readonly Dictionary<Type, StateNode> _nodes = new();

        readonly IDictionary<ITrigger, Action> _triggersEventsSubscriptions =
            new Dictionary<ITrigger, Action>();

        StateNode _current;

        public void Tick()
        {
            if (_current.State is ITickableState tickableState)
                tickableState.Tick();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();

            SubscribeTriggers();
        }

        public StateMachine AddTransition(IState from, IState to, ITrigger trigger)
        {
            GetOrAddNode(from)
               .AddTransition(
                    GetOrAddNode(to)
                       .State,
                    trigger
                );

            return this;
        }

        StateNode GetOrAddNode(IState state)
        {
            StateNode node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        void ChangeState(IState state)
        {
            if (state == _current.State)
                return;

            UnsubscribeTriggers();

            IState previousState = _current.State;
            IState nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _current = _nodes[state.GetType()];

            SubscribeTriggers();
        }

        void SubscribeTriggers()
        {
            HashSet<ITransition> transitions = _current.Transitions;

            foreach (ITransition transition in transitions)
            {
                var callback = new Action(() => ChangeState(transition.NextState));

                transition.Trigger.OnFired += callback;
                _triggersEventsSubscriptions.Add(transition.Trigger, callback);
            }
        }

        void UnsubscribeTriggers()
        {
            foreach (KeyValuePair<ITrigger, Action> keyValuePair in
                _triggersEventsSubscriptions)
            {
                ITrigger trigger = keyValuePair.Key;
                Action changeStateAction = keyValuePair.Value;

                trigger.OnFired -= changeStateAction;
            }

            _triggersEventsSubscriptions.Clear();
        }

        class StateNode
        {
            public IState State => _state;
            public HashSet<ITransition> Transitions => _transitions;

            readonly IState _state;
            readonly HashSet<ITransition> _transitions;

            public StateNode(IState state)
            {
                _state = state;
                _transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState nextState, ITrigger trigger)
            {
                _transitions.Add(new Transition(nextState, trigger));
            }
        }
    }
}
