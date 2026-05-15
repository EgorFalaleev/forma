using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.HexGrid.States
{
    public class SpawningState : IState
    {
        public ITrigger OnGridSpawned => _onGridSpawned;
        
        readonly HexGridBuilder _hexGridBuilder;
        readonly ITargetProvider _targetProvider;
        readonly HexTileController _hexTileController;
        readonly IHexGridAnimator _hexGridAnimator;
        readonly IHexGridRegistry _hexGridRegistry;
        readonly Trigger _onGridSpawned;

        public SpawningState(HexGridBuilder hexGridBuilder,
            ITargetProvider targetProvider, HexTileController hexTileController,
            IHexGridAnimator hexGridAnimator, IHexGridRegistry hexGridRegistry)
        {
            _hexGridBuilder = hexGridBuilder;
            _targetProvider = targetProvider;
            _hexTileController = hexTileController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridRegistry = hexGridRegistry;

            _onGridSpawned = new Trigger();
        }

        public void OnEnter()
        {
            SpawnGridAsync()
               .Forget();
        }

        public void OnExit() { }

        public void Tick() { }

        async UniTaskVoid SpawnGridAsync()
        {
            IEnumerable<HexTileData> gridPositions =
                _hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            foreach (HexTileData hexTileData in gridPositions)
            {
                _hexTileController.PrepareTile(hexTileData);
            }

            await _hexGridAnimator.PlaySpawn(_hexGridRegistry.Tiles);
            
            _onGridSpawned.Fire();
        }
    }
}
