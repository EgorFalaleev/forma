using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFactory
    {
        readonly HexGridData _hexGridData;
        readonly ITargetProvider _targetProvider;
        readonly IToggleGridInput _toggleGridInput;

        public HexGridFactory(HexGridData hexGridData, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput)
        {
            _hexGridData = hexGridData;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridLayout = hexGridGo.AddComponent<HexGridLayout>();

            hexGridLayout.Initialize(_hexGridData, _targetProvider.Target);

            var hexGrid = new HexGrid(hexGridLayout, _toggleGridInput);

            return hexGrid;
        }
    }
}
