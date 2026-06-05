using Forma.Runtime.Common;
using Forma.Runtime.HexGrid.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.HexGrid
{
    public class TileFactory
    {
        readonly IObjectResolver _objectResolver;

        public TileFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public Tile Create(HexCubeCoordinates coordinates, Transform parent)
        {
            var resource = Resources.Load<Tile>(Constants.Resources.Tile);

            Tile tile = _objectResolver.Instantiate(resource, parent);

            tile.DrawMesh();
            tile.gameObject.AddComponent<BoxCollider>();

            tile.name = $"Tile {coordinates}";

            tile.gameObject.SetActive(false);

            return tile;
        }
    }
}
