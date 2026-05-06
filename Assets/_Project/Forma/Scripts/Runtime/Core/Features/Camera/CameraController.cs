using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using R3;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Camera
{
    public class CameraController : IDisposable
    {
        readonly CameraView _cameraView;
        readonly IHexGridStateProvider _hexGridStateProvider;
        readonly ITargetProvider _targetProvider;
        readonly CompositeDisposable _disposables;

        public CameraController(CameraView cameraView,
            IHexGridStateProvider hexGridStateProvider, ITargetProvider targetProvider)
        {
            _cameraView = cameraView;
            _hexGridStateProvider = hexGridStateProvider;
            _targetProvider = targetProvider;

            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            Transform target = _targetProvider.Target;

            _cameraView.FollowCamera.Target.TrackingTarget = target;
            _cameraView.OverviewCamera.Target.TrackingTarget = target;

            _hexGridStateProvider
               .State
               .Subscribe(OnHexGridStateChanged)
               .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        void OnHexGridStateChanged(HexGridState state)
        {
            switch (state)
            {
                case HexGridState.Visible:
                    _cameraView.OverviewCamera.Prioritize();
                    break;

                case HexGridState.Despawning:
                    _cameraView.FollowCamera.Prioritize();
                    break;

                case HexGridState.Hidden:
                    break;

                case HexGridState.Spawning:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
