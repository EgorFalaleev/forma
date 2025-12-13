using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreFlow : IStartable
    {
        public void Start()
        {
            Debug.Log("CoreFlow.Start()");
        }
    }
}