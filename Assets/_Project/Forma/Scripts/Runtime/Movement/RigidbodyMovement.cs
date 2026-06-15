using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement: MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;


    public void Move(Vector3 velocity)
    { 
        _rigidbody.linearVelocity = velocity;
    }   
}