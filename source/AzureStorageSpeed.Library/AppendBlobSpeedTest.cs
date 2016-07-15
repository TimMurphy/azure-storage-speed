using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageSpeed.Library
{
    public class AppendBlobSpeedTest
    {
        private readonly string _blobName;
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly int _rows;
        private readonly int _stringLength;
        private CloudAppendBlob _blob;
        private Profiler _profiler;
        private string _stringValue;

        public AppendBlobSpeedTest(string connectionString, string containerName, string blobName, int stringLength, int rows)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _blobName = blobName;
            _stringLength = stringLength;
            _rows = rows;
        }

        public async Task<IEnumerable<SpeedTestResult>> RunAsync()
        {
            _profiler = new Profiler();

            await SetupAsync();
            await AppendRowsAsync();
            await ReadRowsAsync();

            return _profiler.Results;
        }

        private async Task AppendRowsAsync()
        {
            for (var i = 0; i < _rows; i++)
            {
                await RunTestAsync(i);
            }
        }

        private async Task RunTestAsync(int testNumber)
        {
            using (_profiler.Start($"Append string ({_stringLength:N0} characters) to Azure AppendBlob ({testNumber + 1:N0} of {_rows:N0})"))
            {
                await _blob.AppendTextAsync(_stringValue + "\n");
            }
        }

        private async Task ReadRowsAsync()
        {
            using (_profiler.Start($"Read"))
            {
                using (var stream = await _blob.OpenReadAsync())
                using (var streamReader = new StreamReader(stream))
                {
                    var content = await streamReader.ReadToEndAsync();
                    var rows = content.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

                    if (rows.Length != _rows)
                    {
                        throw new Exception($"Expected to read {_rows} but read {rows.Length}");
                    }
                }
            }
        }

        private async Task SetupAsync()
        {
            using (_profiler.Start("Setup"))
            {
                _stringValue = "".PadRight(_stringLength, 'a');

                var account = CloudStorageAccount.Parse(_connectionString);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference(_containerName);

                await container.CreateIfNotExistsAsync();

                _blob = container.GetAppendBlobReference(_blobName);

                await _blob.CreateOrReplaceAsync();
            }
        }
    }
}