using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Grid
{
    public class HexGridRegistry
    {
        public IReadOnlyDictionary<HexCubeCoordinates, HexView> Tiles => _coordsToViews;
        public HexCubeCoordinates GridCenterCoordinates => new(0, 0);

        readonly HexGridBuilder _hexGridBuilder;
        readonly HexViewFactory _hexViewFactory;
        readonly Dictionary<HexCubeCoordinates, HexView> _coordsToViews;
        readonly Dictionary<HexView, HexCubeCoordinates> _viewsToCoords;

        public HexGridRegistry(HexGridBuilder hexGridBuilder,
            HexViewFactory hexViewFactory)
        {
            _hexGridBuilder = hexGridBuilder;
            _hexViewFactory = hexViewFactory;

            _coordsToViews = new Dictionary<HexCubeCoordinates, HexView>();
            _viewsToCoords = new Dictionary<HexView, HexCubeCoordinates>();
        }

        public void Initialize()
        {
            var hexGridGo = new GameObject("HexGrid");

            IEnumerable<HexCubeCoordinates> gridTilesCoordinates =
                _hexGridBuilder.CalculateHexCoordinates();

            foreach (HexCubeCoordinates tileCoordinates in gridTilesCoordinates)
            {
                HexView hexView = _hexViewFactory.Create(
                    tileCoordinates,
                    hexGridGo.transform
                );

                hexView.gameObject.SetActive(false);

                _coordsToViews.Add(tileCoordinates, hexView);
                _viewsToCoords.Add(hexView, tileCoordinates);
            }
        }

        public HexView GetView(HexCubeCoordinates coordinates)
            => _coordsToViews[coordinates];

        public HexCubeCoordinates GetCoordinates(HexView view) => _viewsToCoords[view];
    }
}
