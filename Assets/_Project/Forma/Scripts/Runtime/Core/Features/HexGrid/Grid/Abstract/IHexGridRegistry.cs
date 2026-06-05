using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Grid.Abstract
{
    public interface IHexGridRegistry
    {
        IReadOnlyDictionary<HexCubeCoordinates, HexView> Tiles { get; }
        HexCubeCoordinates GridCenterCoordinates { get; }
        HexView GetView(HexCubeCoordinates coordinates);
        HexCubeCoordinates GetCoordinates(HexView view);
    }
}
