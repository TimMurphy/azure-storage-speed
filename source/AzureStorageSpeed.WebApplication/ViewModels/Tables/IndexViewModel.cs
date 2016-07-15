using System;
using System.ComponentModel.DataAnnotations;
using AzureStorageSpeed.Library;
using EmptyStringGuard;
using NullGuard;
using ValidationFlags = EmptyStringGuard.ValidationFlags;

namespace AzureStorageSpeed.WebApplication.ViewModels.Tables
{
    [EmptyStringGuard(ValidationFlags.None), NullGuard(NullGuard.ValidationFlags.None)]
    public class IndexViewModel
    {
        private SpeedTestResult[] _results;

        public IndexViewModel()
        {
            TableName = "speedtests";
            StringLength = 1024;
            Rows = 10;
            Results = new SpeedTestResult[] {};
        }


        [Display(Name = "Connection String"), Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }

        [Display(Name = "Table Name"), Required(AllowEmptyStrings = false)]
        public string TableName { get; set; }

        [Display(Name = "String Length"), Range(1, 32768)]
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