using Forma.Runtime.Player;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Forma.Runtime.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineCamera _followCamera;
        [SerializeField] CinemachineCamera _overviewCamera;

        IPlayerProvider _playerProvider;

        [Inject]
        public void Construct(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            Transform target = _playerProvider.Transform;

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
