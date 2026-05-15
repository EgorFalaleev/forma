using System;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow
        : IHexGridEvents,
          IDisposable
    {
        public event Action OnActivated;
        public event Action OnDeactivated;
        
        readonly HexGridRegistry _hexGridRegistry;
        readonly HexTileSelectionController _hexTileSelectionController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly StatesGraph _statesGraph;
        readonly StateMachine.StateMachine _stateMachine;

        public HexGridFlow(HexGridRegistry hexGridRegistry,
            HexTileSelectionController hexTileSelectionController,
            HexGridAnimator hexGridAnimator, StatesGraph statesGraph)
        {
            _hexGridRegistry = hexGridRegistry;
            _hexTileSelectionController = hexTileSelectionController;
            _hexGridAnimator = hexGridAnimator;
            _statesGraph = statesGraph;
            _stateMachine = _statesGraph.StateMachine;
        }

        public void Initialize()
        {
            _hexGridRegistry.Initialize();
            _hexTileSelectionController.Initialize();
            _hexGridAnimator.Initialize();
            _statesGraph.Initialize();

            _statesGraph.OnGridActivated += OnGridActivated;
            _statesGraph.OnGridDeactivated += OnGridDeactivated;
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }

        public void Dispose()
        {
            _hexTileSelectionController.Dispose();
            _statesGraph.Dispose();
            
            _statesGraph.OnGridActivated -= OnGridActivated;
            _statesGraph.OnGridDeactivated -= OnGridDeactivated;
        }

        void OnGridActivated()
        {
            OnActivated?.Invoke();
        }
        
        void OnGridDeactivated()
        {
            OnDeactivated?.Invoke();
        }
    }
}
