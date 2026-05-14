using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.HexGrid.States
{
    public class DespawningState : IState
    {
        public ITrigger OnGridDespawned => _onGridDespawned;
        
        readonly IHexGridAnimator _hexGridAnimator;
        readonly IHexGridRegistry _hexGridRegistry;
        readonly Trigger _onGridDespawned;

        public DespawningState(IHexGridAnimator hexGridAnimator,
            IHexGridRegistry hexGridRegistry)
        {
            _hexGridAnimator = hexGridAnimator;
            _hexGridRegistry = hexGridRegistry;

            _onGridDespawned = new Trigger();
        }

        public void OnEnter()
        {
            DespawnGridAsync()
               .Forget();
        }

        public void OnExit() { }

        public void Tick() { }

        async UniTaskVoid DespawnGridAsync()
        {
            await _hexGridAnimator.PlayDespawn(_hexGridRegistry.Tiles);
            
            _onGridDespawned.Fire();
        }
    }
}
