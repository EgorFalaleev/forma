using R3;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    [RequireComponent(typeof(Collider))]
    public class TriggerZone : MonoBehaviour
    {
        public Observable<Transform> OnTransformEntered => _onTransformEntered;
        public Observable<Transform> OnTransformExited => _onTransformExited;

        readonly Subject<Transform> _onTransformEntered = new();
        readonly Subject<Transform> _onTransformExited = new();

        void OnTriggerEnter(Collider other)
        {
            _onTransformEntered.OnNext(other.transform);
        }

        void OnTriggerExit(Collider other)
        {
            _onTransformExited.OnNext(other.transform);
        }
    }
}
