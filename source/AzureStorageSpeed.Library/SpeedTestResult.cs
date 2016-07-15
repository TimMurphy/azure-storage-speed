using System;
using System.Diagnostics;

namespace AzureStorageSpeed.Library
{
    public class SpeedTestResult : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        public SpeedTestResult(string message)
        {
            Message = message;
            _stopwatch = Stopwatch.StartNew();
        }

        public SpeedTestResult(string message, TimeSpan elapsed)
            : this(message)
        {
            Elapsed = elapsed;
        }

        public string Message { get; }
        public TimeSpan Elapsed { get; private set; }

        public void Dispose()
        {
            _stopwatch.Stop();
            Elapsed = _stopwatch.Elapsed;
        }
    }
}