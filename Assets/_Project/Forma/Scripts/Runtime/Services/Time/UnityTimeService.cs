using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Services.Time
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}