using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileController
    {
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexOccupancyController _hexOccupancyController;
        readonly HexTileConfig _hexTileConfig;

        public HexTileController(HexTileRegistry hexTileRegistry,
            HexOccupancyController hexOccupancyController, HexGridConfig hexGridConfig)
        {
            _hexTileRegistry = hexTileRegistry;
            _hexOccupancyController = hexOccupancyController;
            _hexTileConfig = hexGridConfig.HexTileConfig;
        }

        public void PrepareTile(HexTileData tileData)
        {
            HexCubeCoordinates tileCoordinates = tileData.Coordinates;

            HexView hexView = _hexTileRegistry.GetView(tileCoordinates);

            hexView.UpdatePosition(tileData.Position);

            if (!_hexOccupancyController.IsTileActive(tileCoordinates))
                hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);
            else
                hexView.ResetColor();
        }
    }
}
