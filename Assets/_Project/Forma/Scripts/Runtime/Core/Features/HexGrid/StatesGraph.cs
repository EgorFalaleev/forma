using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.States;
using Forma.Runtime.Core.Features.HexGrid.Tile;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class StatesGraph : IDisposable
    {
        public event Action OnGridActivated;
        public event Action OnGridDeactivated;

        public StateMachine.StateMachine StateMachine => _stateMachine;

        readonly HiddenState _hiddenState;
        readonly SpawningState _spawningState;
        readonly VisibleState _visibleState;
        readonly DespawningState _despawningState;
        readonly StateMachine.StateMachine _stateMachine;

        public StatesGraph(HexTileSelector hexTileSelector,
            IToggleGridInput toggleGridInput, HexGridBuilder hexGridBuilder,
            ITargetProvider targetProvider, HexTileController hexTileController,
            IHexGridAnimator hexGridAnimator, IHexGridRegistry hexGridRegistry)
        {
            _hiddenState = new HiddenState(hexTileSelector, toggleGridInput);

            _spawningState = new SpawningState(
                hexGridBuilder,
                targetProvider,
                hexTileController,
                hexGridAnimator,
                hexGridRegistry
            );

            _visibleState = new VisibleState(hexTileSelector, toggleGridInput);
            _despawningState = new DespawningState(hexGridAnimator, hexGridRegistry);

            _stateMachine = new StateMachine.StateMachine();
        }

        public void Initialize()
        {
            _stateMachine
               .AddTransition(
                    _hiddenState,
                    _spawningState,
                    _hiddenState.OnGridSpawnRequested
                )
               .AddTransition(_spawningState, _visibleState, _spawningState.OnGridSpawned)
               .AddTransition(
                    _visibleState,
                    _despawningState,
                    _visibleState.OnGridDespawnRequested
                )
               .AddTransition(
                    _despawningState,
                    _hiddenState,
                    _despawningState.OnGridDespawned
                );

            _stateMachine.SetState(_hiddenState);

            _hiddenState.OnBecameHidden += OnBecameHidden;
            _visibleState.OnBecameVisible += OnBecameVisible;
        }

        public void Dispose()
        {
            _hiddenState.OnBecameHidden -= OnBecameHidden;
            _visibleState.OnBecameVisible -= OnBecameVisible;
        }

        void OnBecameVisible()
        {
            OnGridActivated?.Invoke();
        }

        void OnBecameHidden()
        {
            OnGridDeactivated?.Invoke();
        }
    }
}
