using System;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelectionController : IDisposable
    {
        readonly HexTileSelector _hexTileSelector;
        readonly IHexSelectionSetter _hexSelectionSetter;
        readonly HexGridRegistry _hexGridRegistry;

        public HexTileSelectionController(HexTileSelector hexTileSelector,
            IHexSelectionSetter hexSelectionSetter, HexGridRegistry hexGridRegistry)
        {
            _hexTileSelector = hexTileSelector;
            _hexSelectionSetter = hexSelectionSetter;
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
            _hexSelectionSetter.SetSelection(
                view.transform.position,
                _hexGridRegistry.GetCoordinates(view)
            );
        }

        void OnHexDeselected()
        {
            _hexSelectionSetter.ClearSelection();
        }
    }
}
