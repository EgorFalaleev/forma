using System;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Tile;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow : IDisposable
    {
        readonly HexGridRegistry _hexGridRegistry;
        readonly HexTileSelectionController _hexTileSelectionController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexGridController _hexGridController;
        readonly HexTileSelector _hexTileSelector;

        public HexGridFlow(HexGridRegistry hexGridRegistry,
            HexTileSelectionController hexTileSelectionController,
            HexGridAnimator hexGridAnimator,
            HexGridController hexGridController,
            HexTileSelector hexTileSelector)
        {
            _hexGridRegistry = hexGridRegistry;
            _hexTileSelectionController = hexTileSelectionController;
            _hexGridAnimator = hexGridAnimator;
            _hexGridController = hexGridController;
            _hexTileSelector = hexTileSelector;
        }

        public void Initialize()
        {
            _hexGridRegistry.Initialize();
            _hexTileSelectionController.Initialize();
            _hexGridAnimator.Initialize();
            _hexGridController.Initialize();
            _hexTileSelector.Initialize();
        }

        public void Dispose()
        {
            _hexTileSelectionController.Dispose();
            _hexGridController.Dispose();
            _hexTileSelector.Dispose();
        }
    }
}
