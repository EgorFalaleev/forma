using UnityEngine;

namespace Forma.Runtime.Services.CameraProvider
{
    public class CameraProvider : ICameraProvider
    {
        public Camera Camera => Camera.main; 
    }
}
