using System;
using System.ComponentModel.DataAnnotations;
using AzureStorageSpeed.Library;
using EmptyStringGuard;
using NullGuard;
using ValidationFlags = EmptyStringGuard.ValidationFlags;

namespace AzureStorageSpeed.WebApplication.ViewModels.AppendBlobs
{
    [EmptyStringGuard(ValidationFlags.None), NullGuard(NullGuard.ValidationFlags.None)]
    public class IndexViewModel
    {
        private SpeedTestResult[] _results;

        public IndexViewModel()
        {
            ContainerName = "speedtests";
            BlobName = "speedtests";
            StringLength = 1024;
            Rows = 10;
            Results = new SpeedTestResult[] {};
        }

        [Display(Name = "Connection String"), Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }

        [Display(Name = "Container Name"), Required(AllowEmptyStrings = false)]
        public string ContainerName { get; set; }

        [Display(Name = "Blob Name"), Required(AllowEmptyStrings = false)]
        public string BlobName { get; set; }

        [Display(Name = "String Length")]
        public int StringLength { get; set; }

        [Display(Name = "Number of tests"), Range(1, 20)]
        public int Rows { get; set; }

        public SpeedTestResult[] Results
        {
            get { return _results; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Results));
                }
                _results = value;
            }
        }
    }
}