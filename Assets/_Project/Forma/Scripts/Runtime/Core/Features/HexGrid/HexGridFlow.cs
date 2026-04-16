using System;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFlow : IDisposable
    {
        readonly HexGridFactory _hexGridFactory;

        HexGrid _hexGrid;
        
        public HexGridFlow(HexGridFactory hexGridFactory)
        {
            _hexGridFactory = hexGridFactory;
        }

        public void Initialize()
        {
            _hexGrid = _hexGridFactory.Create();
        }

        public void Dispose()
        {
            _hexGrid?.Dispose();
        }
    }
}
