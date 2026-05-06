using Unity.Cinemachine;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] CinemachineCamera _followCamera;
        [SerializeField] CinemachineCamera _overviewCamera;

        public CinemachineCamera FollowCamera => _followCamera;
        public CinemachineCamera OverviewCamera => _overviewCamera;
    }
}
