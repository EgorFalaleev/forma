using Cysharp.Threading.Tasks;
using Forma.Runtime.HexGrid.Data;
using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.HexGrid
{
    public class GridController
    {
        readonly PlayerRepository _playerRepository;
        readonly GridRepository _gridRepository;
        readonly GridBuilder _gridBuilder;
        readonly TileFactory _tileFactory;
        readonly GridAnimator _gridAnimator;

        bool _warmedUp;

        public GridController(PlayerRepository playerRepository,
            GridRepository gridRepository, GridBuilder gridBuilder,
            TileFactory tileFactory, GridAnimator gridAnimator)
        {
            _playerRepository = playerRepository;
            _gridRepository = gridRepository;
            _gridBuilder = gridBuilder;
            _tileFactory = tileFactory;
            _gridAnimator = gridAnimator;
        }

        public async UniTask SpawnGrid()
        {
            if (!_warmedUp)
                WarmUp();

            Vector3 origin = _playerRepository.Transform.position;

            PrepareTiles(origin);

            await _gridAnimator.PlaySpawn();
        }

        public async UniTask DespawnGrid()
            => await _gridAnimator.PlayDespawn();

        void WarmUp()
        {
            Transform gridParent = new GameObject("Grid").transform;

            foreach ((HexCubeCoordinates coordinates, Vector3 _) in _gridBuilder.Layout)
            {
                Tile tile = _tileFactory.Create(coordinates, gridParent);

                _gridRepository.Register(tile, coordinates);
            }

            _gridAnimator.WarmUp();

            _warmedUp = true;
        }

        void PrepareTiles(Vector3 center)
        {
            foreach ((HexCubeCoordinates coordinates, Vector3 offset) in _gridBuilder
               .Layout)
            {
                Tile tile = _gridRepository.GetView(coordinates);
                tile.UpdatePosition(center + offset);

                if (_gridRepository.IsTileActive(coordinates))
                    tile.PrepareActive();
                else
                    tile.PrepareInactive();
            }
        }
    }
}
