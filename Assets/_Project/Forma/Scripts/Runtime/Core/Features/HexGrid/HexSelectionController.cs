using System;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexSelectionController : IDisposable
    {
        readonly HexTileSelector _hexTileSelector;
        readonly IHexSelectionSetter _hexSelectionSetter;
        readonly HexTileRegistry _hexTileRegistry;

        public HexSelectionController(HexTileSelector hexTileSelector,
            IHexSelectionSetter hexSelectionSetter, HexTileRegistry hexTileRegistry)
        {
            _hexTileSelector = hexTileSelector;
            _hexSelectionSetter = hexSelectionSetter;
            _hexTileRegistry = hexTileRegistry;
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
                _hexTileRegistry.GetCoordinates(view)
            );
        }

        void OnHexDeselected()
        {
            _hexSelectionSetter.ClearSelection();
        }
    }
}
