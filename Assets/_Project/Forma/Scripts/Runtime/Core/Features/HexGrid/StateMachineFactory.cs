using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.States;
using Forma.Runtime.Core.Features.HexGrid.Tile;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class StateMachineFactory
    {
        readonly IHexTileDeselector _hexTileDeselector;
        readonly IToggleGridInput _toggleGridInput;
        readonly HexGridBuilder _hexGridBuilder;
        readonly ITargetProvider _targetProvider;
        readonly HexTileController _hexTileController;
        readonly IHexGridAnimator _hexGridAnimator;
        readonly IHexGridRegistry _hexGridRegistry;

        public StateMachineFactory(IHexTileDeselector hexTileDeselector,
            IToggleGridInput toggleGridInput, HexGridBuilder hexGridBuilder,
            ITargetProvider targetProvider, HexTileController hexTileController,
            IHexGridAnimator hexGridAnimator, IHexGridRegistry hexGridRegistry)
        {
            _hexTileDeselector = hexTileDeselector;
            _toggleGridInput = toggleGridInput;
            _hexGridBuilder = hexGridBuilder;
            _targetProvider = targetProvider;
            _hexTileController = hexTileController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridRegistry = hexGridRegistry;
        }

        public StateMachine.StateMachine Create()
        {
            var hiddenState = new HiddenState(_hexTileDeselector, _toggleGridInput);

            var spawningState = new SpawningState(
                _hexGridBuilder,
                _targetProvider,
                _hexTileController,
                _hexGridAnimator,
                _hexGridRegistry
            );

            var visibleState = new VisibleState(_hexTileDeselector, _toggleGridInput);
            var despawningState = new DespawningState(_hexGridAnimator, _hexGridRegistry);

            StateMachine.StateMachine stateMachine = new StateMachine.StateMachine()
               .AddTransition(
                    hiddenState,
                    spawningState,
                    hiddenState.OnGridSpawnRequested
                )
               .AddTransition(spawningState, visibleState, spawningState.OnGridSpawned)
               .AddTransition(
                    visibleState,
                    despawningState,
                    visibleState.OnGridDespawnRequested
                )
               .AddTransition(
                    despawningState,
                    hiddenState,
                    despawningState.OnGridDespawned
                );
                
               stateMachine.SetState(hiddenState);

               return stateMachine;
        }
    }
}
