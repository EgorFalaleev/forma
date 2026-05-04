using System;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow : IDisposable
    {
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexSelectionController _hexSelectionController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexGridActivationController _hexGridActivationController;
        readonly HexTileSelector _hexTileSelector;

        HexGrid _hexGrid;

        public HexGridFlow(HexTileRegistry hexTileRegistry,
            HexSelectionController hexSelectionController,
            HexGridAnimator hexGridAnimator,
            HexGridActivationController hexGridActivationController,
            HexTileSelector hexTileSelector)
        {
            _hexTileRegistry = hexTileRegistry;
            _hexSelectionController = hexSelectionController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridActivationController = hexGridActivationController;
            _hexTileSelector = hexTileSelector;
        }

        public void Initialize()
        {
            _hexTileRegistry.Initialize();
            _hexSelectionController.Initialize();
            _hexGridAnimator.Initialize();
            _hexGridActivationController.Initialize();
            _hexTileSelector.Initialize();
        }

        public void Dispose()
        {
            _hexSelectionController.Dispose();
            _hexGrid?.Dispose();
        }
    }
}
