namespace Forma.Runtime.Services.Time
{
    public class UnityTimeService : ITimeService
    {
        public float deltaTime => UnityEngine.Time.deltaTime;
    }
}