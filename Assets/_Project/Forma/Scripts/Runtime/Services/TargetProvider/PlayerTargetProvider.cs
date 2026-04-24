using System;
using Forma.Runtime.Core.Common;
using UnityEngine;

namespace Forma.Runtime.Services.TargetProvider
{
    public class PlayerTargetProvider : ITargetProvider
    {
        public Transform Target
        {
            get
            {
                if (!_isInitialized)
                {
                    throw new Exception("Player does not exist");
                }

                return _player;
            }
        }

        Transform _player;

        bool _isInitialized;

        public void Initialize(Transform target)
        {
            if (_isInitialized)
            {
                Debug.LogWarning(
                    $"{nameof(PlayerTargetProvider)}: Trying to initialize with existing player"
                );
            }

            _player = target;
            _isInitialized = true;
        }
    }
}
