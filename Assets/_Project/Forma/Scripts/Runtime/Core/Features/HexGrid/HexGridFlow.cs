using System;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Tile;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow : IDisposable
    {
        readonly HexGridRegistry _hexGridRegistry;
        readonly HexTileSelectionController _hexTileSelectionController;
        readonly HexGridAnimator _hexGridAnimator;
        readonly HexTileSelector _hexTileSelector;
        readonly StateMachineFactory _stateMachineFactory;

        StateMachine.StateMachine _stateMachine;

        public HexGridFlow(HexGridRegistry hexGridRegistry,
            HexTileSelectionController hexTileSelectionController,
            HexGridAnimator hexGridAnimator, HexTileSelector hexTileSelector,
            StateMachineFactory stateMachineFactory)
        {
            _hexGridRegistry = hexGridRegistry;
            _hexTileSelectionController = hexTileSelectionController;
            _hexGridAnimator = hexGridAnimator;
            _hexTileSelector = hexTileSelector;
            _stateMachineFactory = stateMachineFactory;
        }

        public void Initialize()
        {
            _hexGridRegistry.Initialize();
            _hexTileSelectionController.Initialize();
            _hexGridAnimator.Initialize();
            _hexTileSelector.Initialize();
            _stateMachine = _stateMachineFactory.Create();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }

        public void Dispose()
        {
            _hexTileSelectionController.Dispose();
            _hexTileSelector.Dispose();
        }
    }
}
