using Forma.Runtime.Core.Common;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineCamera _followCamera;
        [SerializeField] CinemachineCamera _overviewCamera;

        ITargetProvider _targetProvider;

        [Inject]
        public void Construct(ITargetProvider targetProvider)
        {
            _targetProvider = targetProvider;
        }

        public void Initialize()
        {
            Transform target = _targetProvider.Transform;

            _followCamera.Target.TrackingTarget = target;
            _overviewCamera.Target.TrackingTarget = target;

            ShowFollow();
        }

        public void ShowFollow()
            => _followCamera.Prioritize();

        public void ShowOverview()
            => _overviewCamera.Prioritize();
    }
}
