using System.Collections.Generic;
using System.Linq;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileRegistry
    {
        public IReadOnlyDictionary<HexCubeCoordinates, HexView> Tiles => _coordsToViews;

        readonly Dictionary<HexCubeCoordinates, HexView> _coordsToViews;
        readonly Dictionary<HexView, HexCubeCoordinates> _viewsToCoords;

        public HexTileRegistry(Dictionary<HexCubeCoordinates, HexView> coordsToViews)
        {
            _coordsToViews = coordsToViews;

            _viewsToCoords = _coordsToViews.ToDictionary(
                keyValuePair => keyValuePair.Value,
                keyValuePair => keyValuePair.Key
            );
        }

        public HexView GetView(HexCubeCoordinates coordinates)
            => _coordsToViews[coordinates];

        public HexCubeCoordinates GetCoordinates(HexView view) => _viewsToCoords[view];
    }
}
