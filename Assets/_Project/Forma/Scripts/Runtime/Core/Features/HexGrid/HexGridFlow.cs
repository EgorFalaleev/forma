using System;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow : IDisposable
    {
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexSelectionController _hexSelectionController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexGridActivationController _hexGridActivationController;

        HexGrid _hexGrid;

        public HexGridFlow(HexTileRegistry hexTileRegistry,
            HexSelectionController hexSelectionController,
            HexGridAnimator hexGridAnimator,
            HexGridActivationController hexGridActivationController)
        {
            _hexTileRegistry = hexTileRegistry;
            _hexSelectionController = hexSelectionController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridActivationController = hexGridActivationController;
        }

        public void Initialize()
        {
            _hexTileRegistry.Initialize();
            _hexSelectionController.Initialize();
            _hexGridAnimator.Initialize();
            _hexGridActivationController.Initialize();
        }

        public void Dispose()
        {
            _hexSelectionController.Dispose();
            _hexGrid?.Dispose();
        }
    }
}
