using System;
using System.Collections.Generic;
using Forma.Runtime.Core.StateMachine.Predicates;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Transitions;

namespace Forma.Runtime.Core.StateMachine
{
    public class StateMachine
    {
        readonly Dictionary<Type, StateNode> _nodes = new();
        
        StateNode _current;

        public void Tick()
        {
            ITransition transition = GetTransition();

            if (transition != null)
                ChangeState(transition.NextState);

            _current.State?.Tick();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        public StateMachine AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from)
               .AddTransition(
                    GetOrAddNode(to)
                       .State,
                    condition
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

            IState previousState = _current.State;
            IState nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _current = _nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (ITransition transition in _current.Transitions)
            {
                if (transition.Condition.Evaluate())
                    return transition;
            }

            return null;
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

            public void AddTransition(IState nextState, IPredicate condition)
            {
                _transitions.Add(new Transition(nextState, condition));
            }
        }
    }
}
