using System;
using Forma.Runtime.Core.Common;

namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraController : IDisposable
    {
        readonly CameraView _cameraView;
        readonly ITargetProvider _targetProvider;

        public CameraController(CameraView cameraView, ITargetProvider targetProvider)
        {
            _cameraView = cameraView;
            _targetProvider = targetProvider;
        }

        public void Initialize()
        {
            // Transform target = _targetProvider.Target;

            // _cameraView.FollowCamera.Target.TrackingTarget = target;
            // _cameraView.OverviewCamera.Target.TrackingTarget = target;

        }

        public void Dispose()
        {
        }

        void OnHexGridDeactivated()
        {
            _cameraView.FollowCamera.Prioritize();
        }

        void OnHexGridActivated()
        {
            _cameraView.OverviewCamera.Prioritize();
        }
    }
}
