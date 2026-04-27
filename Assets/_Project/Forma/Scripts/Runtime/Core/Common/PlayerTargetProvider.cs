using System;
using UnityEngine;

namespace Forma.Runtime.Core.Common
{
    public class PlayerTargetProvider
        : ITargetProvider,
          ITargetSetter
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

        public void SetTarget(Transform target)
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
