using System;
using R3;

namespace Forma.Runtime.Timer
{
    public class Timer
    {
        public Observable<Timer> OnTimerCanceled => _onTimerCanceled;
        public Observable<Timer> OnTimerFinished => _onTimerFinished;
        public bool IsActive => _isActive;

        readonly float _duration;
        readonly Action<Timer> _callback;
        readonly Subject<Timer> _onTimerCanceled = new();
        readonly Subject<Timer> _onTimerFinished = new();
        bool _isActive;
        float _elapsed;

        public Timer(float duration, Action<Timer> callback)
        {
            _duration = duration;
            _callback = callback;
        }

        public void Start()
            => _isActive = true;

        public void Pause()
            => _isActive = false;

        public void Cancel()
        {
            _isActive = false;
            _onTimerCanceled.OnNext(this);
        }

        public void Update(float delta)
        {
            if (delta < 0)
                throw new Exception($"Delta cannot be negative: {delta}");

            _elapsed += delta;

            if (_elapsed >= _duration)
                Finish();
        }

        void Finish()
        {
            _callback?.Invoke(this);
            _onTimerFinished.OnNext(this);
        }
    }
}
