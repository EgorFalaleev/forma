using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Views
{
    public interface ITurretView
    {
        Transform Transform { get; }
        void StartIdleRotation();
        void StopIdleRotation();
        void LookAtTarget(Vector3 position, float delta);
    }
}
