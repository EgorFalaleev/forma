using Forma.Runtime.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexViewFactory
    {
        public HexView Create(HexTileData hexTileData, Transform parent,
            HexTileConfig hexTileConfig)
        {
            var tileGo = new GameObject(
                $"Hex {hexTileData.Coordinates.x} {hexTileData.Coordinates.y}"
            );

            tileGo.transform.position = hexTileData.Position;
            tileGo.layer = Constants.Layers.HexGrid;

            var hexView = tileGo.AddComponent<HexView>();

            hexView.Construct(
                hexTileConfig.Material,
                hexTileConfig.InnerSize,
                hexTileConfig.OuterSize,
                hexTileConfig.Height,
                hexTileConfig.IsFlatTopped,
                hexTileConfig.ShouldCastShadows
            );

            hexView.DrawMesh();

            tileGo.AddComponent<BoxCollider>();
            
            tileGo.transform.SetParent(parent, true);

            return hexView;
        }
    }
}
