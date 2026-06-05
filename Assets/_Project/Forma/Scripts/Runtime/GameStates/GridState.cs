using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;
using Forma.Runtime.HexGrid;
using Forma.Runtime.Input;
using R3;

namespace Forma.Runtime.GameStates
{
    public class GridState : IState
    {
        public ITrigger OnGridDespawned => _onGridDespawned;

        readonly GridController _gridController;
        readonly ToggleGridInputHandler _toggleGridInputHandler;
        readonly ClickGridTileInputHandler _clickGridTileInputHandler;
        readonly TileController _tileController;
        readonly CompositeDisposable _disposables = new();
        readonly Trigger _onGridDespawned = new();

        public GridState(GridController gridController,
            ToggleGridInputHandler toggleGridInputHandler,
            ClickGridTileInputHandler clickGridTileInputHandler,
            TileController tileController)
        {
            _gridController = gridController;
            _toggleGridInputHandler = toggleGridInputHandler;
            _clickGridTileInputHandler = clickGridTileInputHandler;
            _tileController = tileController;
        }

        public void OnEnter()
        {
            SpawnGrid()
               .Forget();
        }

        public void OnExit()
        {
            _toggleGridInputHandler.Disable();

            _disposables.Clear();
        }

        async UniTaskVoid SpawnGrid()
        {
            await _gridController.SpawnGrid();

            _toggleGridInputHandler.Enable();
            _clickGridTileInputHandler.Enable();

            _toggleGridInputHandler
               .OnGridToggled
               .Subscribe(DespawnGrid)
               .AddTo(_disposables);

            _clickGridTileInputHandler
               .OnClickedTile
               .Subscribe(OnClickedTile)
               .AddTo(_disposables);
        }

        void DespawnGrid(Unit _)
        {
            _toggleGridInputHandler.Disable();
            _clickGridTileInputHandler.Disable();

            DespawnGrid()
               .Forget();
        }

        void OnClickedTile(Tile tile)
        {
            _tileController.ProcessTileSelection(tile);
        }

        async UniTaskVoid DespawnGrid()
        {
            _tileController.Reset();
            
            await _gridController.DespawnGrid();

            _onGridDespawned.Fire();
        }
    }
}
