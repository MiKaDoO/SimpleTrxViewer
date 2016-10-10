using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nancy;
using TestParser.Core;
using TrxViewer.Models;

namespace TrxViewer.Modules
{
    public class ViewerModule : NancyModule
    {
        public ViewerModule()
        {
            Get["/"] = p =>
            {
                if (Request.Query["path"] == null)
                    throw new Exception("Invalid path");

                var path = Request.Query["path"].ToString();

                var trxFile = new List<string>(Directory.GetFiles(path, "*.trx")).FirstOrDefault();

                if (trxFile == null)
                    throw new Exception("No trx file found");

                var vm = GetTestResults(trxFile);

                return View["Index", vm];
            };
        }

        private TestResultsViewModel GetTestResults(string trxPath)
        {
            var resultsFactory = new TestResultFactory();

            var parsedResults = resultsFactory.CreateResultsFromTestFiles(new List<string>() { trxPath });

            return new TestResultsViewModel()
            {
                FailedTestCount = parsedResults.ResultLines.Count(rl => rl.Outcome == "Failed"),
                NotExecutedTestCount = parsedResults.ResultLines.Count(rl => rl.Outcome == "NotExecuted"),
                PassedTestCount = parsedResults.ResultLines.Count(rl => rl.Outcome == "Passed"),
                FailedTests = parsedResults.ResultLines.SortedByFailedOtherPassed.TakeWhile(t => t.Outcome == "Failed").Select(t => new TestResultViewModel()
                {
                    TestName =  t.TestName,
                    ErrorMessage = t.ErrorMessage,
                    StackTrace = t.StackTrace
                }).ToList()
            };
        }
    }
}
