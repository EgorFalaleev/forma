using System;
using System.Collections.Generic;
using Forma.Runtime.Services.Input;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreFlow : IStartable, IDisposable
    {
        readonly IEnumerable<BaseInputService> _inputServices;

        public CoreFlow(IEnumerable<BaseInputService> inputServices)
        {
            _inputServices = inputServices;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            foreach (BaseInputService inputService in _inputServices)
                inputService.Enable();
        }

        public void Dispose()
        {
            foreach (BaseInputService inputService in _inputServices)
                inputService.Disable();
        }
    }
}