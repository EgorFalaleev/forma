using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Tile;

namespace Forma.Runtime.Core.Features.HexGrid.Grid
{
    public class HexGridController : IDisposable
    {
        readonly IToggleGridInput _toggleGridInput;
        readonly HexGridBuilder _hexGridBuilder;
        readonly ITargetProvider _targetProvider;
        readonly HexGridRegistry _hexGridRegistry;
        readonly HexTileSelector _hexTileSelector;
        readonly HexTileController _hexTileController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexGridStateHolder _hexGridStateHolder;

        public HexGridController(IToggleGridInput toggleGridInput,
            HexGridBuilder hexGridBuilder, ITargetProvider targetProvider,
            HexGridRegistry hexGridRegistry, HexTileSelector hexTileSelector,
            HexTileController hexTileController, HexGridAnimator hexGridAnimator,
            HexGridStateHolder hexGridStateHolder)
        {
            _toggleGridInput = toggleGridInput;
            _hexGridBuilder = hexGridBuilder;
            _targetProvider = targetProvider;
            _hexGridRegistry = hexGridRegistry;
            _hexTileSelector = hexTileSelector;
            _hexTileController = hexTileController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridStateHolder = hexGridStateHolder;
        }

        public void Initialize()
        {
            _hexGridStateHolder.SetState(HexGridState.Hidden);

            _toggleGridInput.OnGridModeToggled += OnGridModeToggled;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= OnGridModeToggled;
        }

        void OnGridModeToggled()
        {
            _hexTileSelector.Cleanup();

            switch (_hexGridStateHolder.State)
            {
                case HexGridState.Hidden:
                    SpawnGridAsync()
                       .Forget();

                    break;

                case HexGridState.Visible:
                    DespawnGridAsync()
                       .Forget();

                    break;

                case HexGridState.Spawning:
                    break;

                case HexGridState.Despawning:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        async UniTaskVoid SpawnGridAsync()
        {
            _hexGridStateHolder.SetState(HexGridState.Spawning);

            IEnumerable<HexTileData> gridPositions =
                _hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            foreach (HexTileData hexTileData in gridPositions)
            {
                _hexTileController.PrepareTile(hexTileData);
            }

            await _hexGridAnimator.PlaySpawn(_hexGridRegistry.Tiles);

            _hexGridStateHolder.SetState(HexGridState.Visible);
        }

        async UniTaskVoid DespawnGridAsync()
        {
            _hexGridStateHolder.SetState(HexGridState.Despawning);

            await _hexGridAnimator.PlayDespawn(_hexGridRegistry.Tiles);

            _hexGridStateHolder.SetState(HexGridState.Hidden);
        }
    }
}
