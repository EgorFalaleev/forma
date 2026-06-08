using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraController : IDisposable
    {
        readonly CameraView _cameraView;
        readonly ITargetProvider _targetProvider;
        readonly IHexGridEvents _hexGridEvents;

        public CameraController(CameraView cameraView, ITargetProvider targetProvider,
            IHexGridEvents hexGridEvents)
        {
            _cameraView = cameraView;
            _targetProvider = targetProvider;
            _hexGridEvents = hexGridEvents;
        }

        public void Initialize()
        {
            // Transform target = _targetProvider.Target;

            // _cameraView.FollowCamera.Target.TrackingTarget = target;
            // _cameraView.OverviewCamera.Target.TrackingTarget = target;

            _hexGridEvents.OnActivated += OnHexGridActivated;
            _hexGridEvents.OnDeactivated += OnHexGridDeactivated;
        }

        public void Dispose()
        {
            _hexGridEvents.OnActivated -= OnHexGridActivated;
            _hexGridEvents.OnDeactivated -= OnHexGridDeactivated;
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
