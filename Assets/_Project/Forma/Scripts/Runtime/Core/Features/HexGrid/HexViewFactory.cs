using Forma.Runtime.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexViewFactory
    {
        readonly HexTileConfig _hexTileConfig;
        
        public HexViewFactory(HexGridConfig hexGridConfig)
        {
            _hexTileConfig = hexGridConfig.HexTileConfig;
        }
        
        public HexView Create(HexCubeCoordinates coordinates, Transform parent)
        {
            var tileGo = new GameObject($"Hex {coordinates}");
            tileGo.layer = Constants.Layers.HexGrid;

            var hexView = tileGo.AddComponent<HexView>();

            hexView.Construct(
                _hexTileConfig.Material,
                _hexTileConfig.InnerSize,
                _hexTileConfig.OuterSize,
                _hexTileConfig.Height,
                _hexTileConfig.IsFlatTopped,
                _hexTileConfig.ShouldCastShadows
            );

            hexView.DrawMesh();

            tileGo.AddComponent<BoxCollider>();
            
            tileGo.transform.SetParent(parent, true);

            return hexView;
        }
    }
}
