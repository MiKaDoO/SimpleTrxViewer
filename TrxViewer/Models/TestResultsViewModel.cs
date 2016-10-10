using System.Collections.Generic;

namespace TrxViewer.Models
{
    public class TestResultsViewModel
    {
        public int PassedTestCount { get; set; }
        public int NotExecutedTestCount { get; set; }
        public int FailedTestCount { get; set; }
        public List<TestResultViewModel> FailedTests { get; set; }
    }
}
