using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileController : IHexTileController
    {
        readonly IHexGridRegistry _hexGridRegistry;
        readonly HexTileOccupancyController _hexTileOccupancyController;
        readonly HexTileConfig _hexTileConfig;

        public HexTileController(IHexGridRegistry hexGridRegistry,
            HexTileOccupancyController hexTileOccupancyController, HexGridConfig hexGridConfig)
        {
            _hexGridRegistry = hexGridRegistry;
            _hexTileOccupancyController = hexTileOccupancyController;
            _hexTileConfig = hexGridConfig.HexTileConfig;
        }

        public void PrepareTile(HexTileData tileData)
        {
            HexCubeCoordinates tileCoordinates = tileData.Coordinates;

            HexView hexView = _hexGridRegistry.GetView(tileCoordinates);

            hexView.UpdatePosition(tileData.Position);

            if (!_hexTileOccupancyController.IsTileActive(tileCoordinates))
                hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);
            else
                hexView.ResetColor();
        }

        public void OccupyTile(HexCubeCoordinates tileCoordinates)
        {
            _hexTileOccupancyController.Occupy(tileCoordinates);

            HexView hexView = _hexGridRegistry.GetView(tileCoordinates);

            hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);

            HexCubeCoordinates[] tileNeighbours = tileCoordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (_hexTileOccupancyController.IsTileActive(neighbourCoordinates))
                    _hexGridRegistry
                       .GetView(neighbourCoordinates)
                       .ResetColor();
            }
        }
    }
}
