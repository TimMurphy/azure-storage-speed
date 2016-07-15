using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageSpeed.Library
{
    public class TableSpeedTest
    {
        private readonly string _connectionString;
        private readonly int _rows;
        private readonly int _stringLength;
        private readonly string _tableName;
        private string _partitionKey;
        private Profiler _profiler;
        private string _stringValue;
        private CloudTable _table;

        public TableSpeedTest(string connectionString, string tableName, int stringLength, int rows)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _stringLength = stringLength;
            _rows = rows;
        }

        public async Task<IEnumerable<SpeedTestResult>> RunAsync()
        {
            _profiler = new Profiler();

            await SetupAsync();
            await WriteRowsAsync();
            await ReadRowsAsync();

            return _profiler.Results;
        }

        private async Task WriteRowsAsync()
        {
            for (var i = 0; i < _rows; i++)
            {
                await RunTestAsync(i);
            }
        }

        private async Task RunTestAsync(int testNumber)
        {
            using (_profiler.Start($"Append string ({_stringLength:N0} characters) to Azure Table ({testNumber + 1:N0} of {_rows:N0})"))
            {
                var entity = new SpeedTestTableEntity(_partitionKey, testNumber, _stringValue);
                var operation = TableOperation.Insert(entity);

                await _table.ExecuteAsync(operation);
            }
        }

        private async Task ReadRowsAsync()
        {
            using (_profiler.Start($"Read"))
            {
                var rows = new List<SpeedTestTableEntity>();
                var query = new TableQuery<SpeedTestTableEntity>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partitionKey));

                TableContinuationToken continuationToken = null;

                do
                {
                    var result = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                    rows.AddRange(result.Results);
                    continuationToken = result.ContinuationToken;
                } while (continuationToken != null);

                if (rows.Count != _rows)
                {
                    throw new Exception($"Expected to read {_rows} but read {rows.Count}");
                }
            }
        }

        private async Task SetupAsync()
        {
            using (_profiler.Start("Setup"))
            {
                _stringValue = "".PadRight(_stringLength, 'a');
                _partitionKey = DateTime.UtcNow.ToString("yyyy.mm.dd HH:mm:ss.fff");

                var account = CloudStorageAccount.Parse(_connectionString);
                var client = account.CreateCloudTableClient();

                _table = client.GetTableReference(_tableName);
                await _table.CreateIfNotExistsAsync();
            }
        }
    }
}