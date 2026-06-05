using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Grid.Abstract
{
    public interface IHexGridAnimator
    {
        UniTask PlaySpawn(
            IReadOnlyDictionary<HexCubeCoordinates, HexView> tiles);

        UniTask PlayDespawn(
            IReadOnlyDictionary<HexCubeCoordinates, HexView> tiles);
    }
}
