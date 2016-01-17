﻿namespace OJS.Workers.ExecutionStrategies
{
    using System.Collections.Generic;

    public class ExecutionResult
    {
        public ExecutionResult()
        {
            this.TestResults = new List<TestResult>();
        }

        public bool IsCompiledSuccessfully { get; set; }

        public string CompilerComment { get; set; }

        public List<TestResult> TestResults { get; set; }
    }
}