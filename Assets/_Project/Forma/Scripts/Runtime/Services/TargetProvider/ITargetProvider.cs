using UnityEngine;

namespace Forma.Runtime.Services.TargetProvider
{
    public interface ITargetProvider
    {
        Transform Target { get; }
    }
}
