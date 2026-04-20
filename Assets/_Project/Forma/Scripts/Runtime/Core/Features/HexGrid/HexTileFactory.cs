using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileFactory
    {
        public HexRenderer Create(HexTileData hexTileData, Transform parent,
            HexTileConfig hexTileConfig)
        {
            var tileGo = new GameObject(
                $"Hex {hexTileData.Coordinates.x} {hexTileData.Coordinates.y}"
            );

            tileGo.transform.position = hexTileData.Position;

            var hexRenderer = tileGo.AddComponent<HexRenderer>();

            hexRenderer.Construct(
                hexTileConfig.Material,
                hexTileConfig.InnerSize,
                hexTileConfig.OuterSize,
                hexTileConfig.Height,
                hexTileConfig.IsFlatTopped,
                hexTileConfig.ShouldCastShadows
            );

            hexRenderer.DrawMesh();

            tileGo.transform.SetParent(parent, true);

            return hexRenderer;
        }
    }
}
