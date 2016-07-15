using System.Collections.Generic;
using System.Linq;

namespace AzureStorageSpeed.Library
{
    public class Profiler
    {
        private readonly List<SpeedTestResult> _results = new List<SpeedTestResult>();

        public IEnumerable<SpeedTestResult> Results => _results.AsEnumerable();

        public SpeedTestResult Start(string message)
        {
            var step = new SpeedTestResult(message);

            _results.Add(step);

            return step;
        }
    }
}