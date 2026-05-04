using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridActivationController : IDisposable
    {
        readonly IToggleGridInput _toggleGridInput;
        readonly HexGridBuilder _hexGridBuilder;
        readonly ITargetProvider _targetProvider;
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexTileSelector _hexTileSelector;
        readonly HexTileController _hexTileController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexGridStateController _hexGridStateController;

        public HexGridActivationController(IToggleGridInput toggleGridInput,
            HexGridBuilder hexGridBuilder, ITargetProvider targetProvider,
            HexTileRegistry hexTileRegistry, HexTileSelector hexTileSelector,
            HexTileController hexTileController, HexGridAnimator hexGridAnimator,
            HexGridStateController hexGridStateController)
        {
            _toggleGridInput = toggleGridInput;
            _hexGridBuilder = hexGridBuilder;
            _targetProvider = targetProvider;
            _hexTileRegistry = hexTileRegistry;
            _hexTileSelector = hexTileSelector;
            _hexTileController = hexTileController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridStateController = hexGridStateController;
        }

        public void Initialize()
        {
            _hexGridStateController.SetState(HexGridState.Hidden);

            _toggleGridInput.OnGridModeToggled += OnGridModeToggled;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= OnGridModeToggled;
        }

        void OnGridModeToggled()
        {
            _hexTileSelector.Cleanup();

            switch (_hexGridStateController.State)
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
            _hexGridStateController.SetState(HexGridState.Spawning);

            IEnumerable<HexTileData> gridPositions =
                _hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            foreach (HexTileData hexTileData in gridPositions)
            {
                _hexTileController.PrepareTile(hexTileData);
            }

            await _hexGridAnimator.PlaySpawn(_hexTileRegistry.Tiles);

            _hexGridStateController.SetState(HexGridState.Visible);
        }

        async UniTaskVoid DespawnGridAsync()
        {
            _hexGridStateController.SetState(HexGridState.Despawning);

            await _hexGridAnimator.PlayDespawn(_hexTileRegistry.Tiles);

            _hexGridStateController.SetState(HexGridState.Hidden);
        }
    }
}
