using System.Collections.Generic;

namespace Forma.Runtime.Core.Enemy.Abstract
{
    public interface IEnemyRegistry
    {
        IEnumerable<Enemy> Enemies { get; }
    }
}
