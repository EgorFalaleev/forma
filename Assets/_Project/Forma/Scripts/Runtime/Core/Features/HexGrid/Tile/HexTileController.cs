using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileController : IHexTileController
    {
        readonly IHexGridRegistry _hexGridRegistry;
        readonly HexTileConfig _hexTileConfig;
        readonly IHexTileDeselector _hexTileDeselector;

        public HexTileController(IHexGridRegistry hexGridRegistry,
            HexGridConfig hexGridConfig, IHexTileDeselector hexTileDeselector)
        {
            _hexGridRegistry = hexGridRegistry;
            _hexTileDeselector = hexTileDeselector;
            _hexTileConfig = hexGridConfig.HexTileConfig;
        }

        public async UniTask OccupyTile(HexCubeCoordinates tileCoordinates)
        {
            await _hexTileDeselector.DeselectTile();
            
            // _hexTileOccupancyController.Occupy(tileCoordinates);

            HexView hexView = _hexGridRegistry.GetView(tileCoordinates);

            hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);

            HexCubeCoordinates[] tileNeighbours = tileCoordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                // if (_hexTileOccupancyController.IsTileActive(neighbourCoordinates))
                    // _hexGridRegistry
                       // .GetView(neighbourCoordinates)
                       // .ResetColor();
            }
        }
    }
}
