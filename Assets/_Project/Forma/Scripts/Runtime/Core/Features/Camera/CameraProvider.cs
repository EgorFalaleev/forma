namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraProvider : ICameraProvider
    {
        public UnityEngine.Camera Camera => UnityEngine.Camera.main; 
    }
}
