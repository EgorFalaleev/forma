using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Timer
{
    public class TimerSystem : ITickable
    {
        readonly List<Timer> _timers = new();
        readonly CompositeDisposable _disposables = new();

        public void Tick()
            => TickTimers(_timers.ToArray());

        public Timer CreateTimer(float duration, Action callback = null)
        {
            var timer = new Timer(duration, callback);

            timer
               .OnTimerCanceled
               .Subscribe(RemoveTimer)
               .AddTo(_disposables);

            timer
               .OnTimerFinished
               .Subscribe(RemoveTimer)
               .AddTo(_disposables);

            _timers.Add(timer);

            return timer;
        }

        void RemoveTimer(Timer timer)
            => _timers.Remove(timer);

        void TickTimers(IEnumerable<Timer> timers)
        {
            foreach (Timer timer in timers)
            {
                if (!timer.IsActive)
                    continue;

                timer.Update(Time.deltaTime);
            }
        }
    }
}
