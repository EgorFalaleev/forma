namespace Forma.Runtime.Camera
{
    public class CameraProvider : ICameraProvider
    {
        public UnityEngine.Camera Camera => UnityEngine.Camera.main;
    }
}
