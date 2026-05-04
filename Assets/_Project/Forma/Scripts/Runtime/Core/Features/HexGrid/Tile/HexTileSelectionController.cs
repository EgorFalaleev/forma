using System;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelectionController : IDisposable
    {
        readonly IHexTileSelectEvents _hexTileSelectEvents;
        readonly IHexTileSelectionSetter _hexTileSelectionSetter;
        readonly IHexGridRegistry _hexGridRegistry;

        public HexTileSelectionController(IHexTileSelectEvents hexTileSelectEvents,
            IHexTileSelectionSetter hexTileSelectionSetter,
            IHexGridRegistry hexGridRegistry)
        {
            _hexTileSelectEvents = hexTileSelectEvents;
            _hexTileSelectionSetter = hexTileSelectionSetter;
            _hexGridRegistry = hexGridRegistry;
        }

        public void Initialize()
        {
            _hexTileSelectEvents.OnHexSelected += OnHexSelected;
            _hexTileSelectEvents.OnHexDeselected += OnHexDeselected;
        }

        public void Dispose()
        {
            _hexTileSelectEvents.OnHexSelected -= OnHexSelected;
            _hexTileSelectEvents.OnHexDeselected -= OnHexDeselected;
        }

        void OnHexSelected(HexView view)
        {
            _hexTileSelectionSetter.SetSelection(
                new HexTileData(
                    _hexGridRegistry.GetCoordinates(view),
                    view.transform.position
                )
            );
        }

        void OnHexDeselected()
        {
            _hexTileSelectionSetter.ClearSelection();
        }
    }
}
