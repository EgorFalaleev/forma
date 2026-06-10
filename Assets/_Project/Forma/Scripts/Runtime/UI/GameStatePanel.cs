using System;
using Forma.Runtime.StateMachine.States;
using TMPro;
using UnityEngine;

namespace Forma.Runtime.UI
{
    public class GameStatePanel : MonoBehaviour
    {
        [SerializeField] TMP_Text _stateNameText;

        StateMachine.StateMachine _stateMachine;
        
        public void Construct(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.OnStateChanged += UpdateState;
        }

        void OnDestroy()
        {
            _stateMachine.OnStateChanged -= UpdateState;
        }

        void UpdateState(IState state)
        {
            _stateNameText.text = state.GetType()
               .Name;
        }
    }
}
