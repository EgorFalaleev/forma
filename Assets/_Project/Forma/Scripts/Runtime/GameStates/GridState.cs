using Cysharp.Threading.Tasks;
using Forma.Runtime.Camera;
using Forma.Runtime.HexGrid;
using Forma.Runtime.Input;
using Forma.Runtime.StateMachine.States;
using Forma.Runtime.StateMachine.Triggers;
using Forma.Runtime.Turret;
using R3;
using UnityEngine;

namespace Forma.Runtime.GameStates
{
    public class GridState : IState
    {
        public ITrigger OnGridDespawned => _onGridDespawned;

        readonly GridController _gridController;
        readonly ToggleGridInputHandler _toggleGridInputHandler;
        readonly ClickGridTileInputHandler _clickGridTileInputHandler;
        readonly TileController _tileController;
        readonly TileSelector _tileSelector;
        readonly PlaceTurretInputHandler _placeTurretInputHandler;
        readonly GridRepository _gridRepository;
        readonly TurretController _turretController;
        readonly CameraController _cameraController;
        readonly CompositeDisposable _disposables = new();
        readonly Trigger _onGridDespawned = new();

        public GridState(GridController gridController,
            ToggleGridInputHandler toggleGridInputHandler,
            ClickGridTileInputHandler clickGridTileInputHandler,
            TileController tileController, TileSelector tileSelector,
            PlaceTurretInputHandler placeTurretInputHandler,
            GridRepository gridRepository, TurretController turretController,
            CameraController cameraController)
        {
            _gridController = gridController;
            _toggleGridInputHandler = toggleGridInputHandler;
            _clickGridTileInputHandler = clickGridTileInputHandler;
            _tileController = tileController;
            _tileSelector = tileSelector;
            _placeTurretInputHandler = placeTurretInputHandler;
            _gridRepository = gridRepository;
            _turretController = turretController;
            _cameraController = cameraController;
        }

        public void OnEnter()
        {
            SpawnGrid()
               .Forget();
        }

        public void OnExit()
        {
            _disposables.Clear();
        }

        async UniTaskVoid SpawnGrid()
        {
            await _gridController.SpawnGrid();
            
            _cameraController.ShowOverview();

            _toggleGridInputHandler.Enable();
            _clickGridTileInputHandler.Enable();
            _placeTurretInputHandler.Enable();

            _toggleGridInputHandler
               .OnGridToggled
               .Subscribe(DespawnGrid)
               .AddTo(_disposables);

            _clickGridTileInputHandler
               .OnClickedTile
               .Subscribe(OnClickedTile)
               .AddTo(_disposables);

            _placeTurretInputHandler
               .OnPlaceTurretClicked
               .Subscribe(OnPlaceTurretClicked)
               .AddTo(_disposables);
        }

        void DespawnGrid(Unit _)
        {
            _toggleGridInputHandler.Disable();
            _clickGridTileInputHandler.Disable();
            _placeTurretInputHandler.Disable();

            DespawnGrid()
               .Forget();
        }

        void OnClickedTile(Tile tile)
        {
            _tileController.ProcessTileSelection(tile);
        }

        void OnPlaceTurretClicked(Unit _)
        {
            if (!_tileSelector.HasSelectedTile)
                return;

            Tile selectedTile = _tileSelector.SelectedTile;

            if (!_gridRepository.IsTileActive(selectedTile))
                return;

            Vector3 selectedTilePosition = selectedTile.transform.position;

            _turretController.PlaceTurret(
                new Vector2(selectedTilePosition.x, selectedTilePosition.z)
            );

            _tileController.Reset();
            _tileController.OccupyTile(selectedTile);
        }

        async UniTaskVoid DespawnGrid()
        {
            _tileController.Reset();

            await _gridController.DespawnGrid();

            _onGridDespawned.Fire();
        }
    }
}
