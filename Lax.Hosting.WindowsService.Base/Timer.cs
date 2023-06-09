﻿using System;
using System.Threading;

namespace Lax.Hosting.WindowsService.Base {

    public class Timer {

        private Thread _thread;
        private AutoResetEvent _stopRequest;
        private bool _running = true;
        private bool _paused;

        public Action OnTimer { get; set; }

        public Action<Exception> OnException { get; set; }

        public string Name { get; }

        public int Interval { get; set; }

        public Timer(string name, int interval, Action onTimer, Action<Exception> onException = null) {
            OnTimer = onTimer ?? (() => { });
            Name = name;
            Interval = interval;
            OnException = onException ?? ((e) => { });
        }

        private void InternalWork(object arg) {
            while (_running) {
                try {
                    if (!_paused) {
                        OnTimer();
                    }
                } catch {
                    // ignored
                }

                try {
                    if (_stopRequest.WaitOne(Interval)) {
                        return;
                    }
                } catch {
                    // ignored
                }
            }
        }

        public void Start() {
            _stopRequest = new AutoResetEvent(false);
            _running = true;
            _thread = new Thread(InternalWork);
            _thread.Start();
        }

        public void Pause() => _paused = true;

        public void Resume() => _paused = false;

        public void Stop() {
            if (!_running) {
                return;
            }

            _running = false;
            _stopRequest.Set();
            _thread.Join();

            _thread = null;
            _stopRequest.Dispose();
            _stopRequest = null;
        }

    }

}