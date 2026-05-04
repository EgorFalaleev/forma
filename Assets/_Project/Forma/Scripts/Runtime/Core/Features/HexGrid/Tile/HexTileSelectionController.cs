using System;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelectionController : IDisposable
    {
        readonly HexTileSelector _hexTileSelector;
        readonly IHexTileSelectionSetter _hexTileSelectionSetter;
        readonly HexGridRegistry _hexGridRegistry;

        public HexTileSelectionController(HexTileSelector hexTileSelector,
            IHexTileSelectionSetter hexTileSelectionSetter,
            HexGridRegistry hexGridRegistry)
        {
            _hexTileSelector = hexTileSelector;
            _hexTileSelectionSetter = hexTileSelectionSetter;
            _hexGridRegistry = hexGridRegistry;
        }

        public void Initialize()
        {
            _hexTileSelector.OnHexSelected += OnHexSelected;
            _hexTileSelector.OnHexDeselected += OnHexDeselected;
        }

        public void Dispose()
        {
            _hexTileSelector.OnHexSelected -= OnHexSelected;
            _hexTileSelector.OnHexDeselected -= OnHexDeselected;
        }

        void OnHexSelected(HexView view)
        {
            _hexTileSelectionSetter.SetSelection(
                new HexTileSelection(
                    view.transform.position,
                    _hexGridRegistry.GetCoordinates(view)
                )
            );
        }

        void OnHexDeselected()
        {
            _hexTileSelectionSetter.ClearSelection();
        }
    }
}
