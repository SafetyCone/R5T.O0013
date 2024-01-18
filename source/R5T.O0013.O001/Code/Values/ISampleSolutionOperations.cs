using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0159;


namespace R5T.O0013.O001
{
    [ValuesMarker]
    public partial interface ISampleSolutionOperations : IValuesMarker
    {
        private static Internal.ISampleSolutionOperations Internal => O001.Internal.SampleSolutionOperations.Instance;


        /// <inheritdoc cref="Internal.ISampleSolutionOperations.PrepareAndGetContext(ITextOutput)"/>
        public Task In_New_SampleSolutionContext(
            ITextOutput textOutput,
            Func<ISolutionContext, Task> solutionContextAction = default)
        {
            var solutionContext = Internal.PrepareAndGetContext(textOutput);

            return Instances.ActionOperator.Run(
                solutionContextAction,
                solutionContext);
        }

        public Task In_New_SampleSolutionContext(
            ITextOutput textOutput,
            IEnumerable<Func<ISolutionContext, Task>> solutionContextActions)
        {
            var solutionContext = Internal.PrepareAndGetContext(textOutput);

            return Instances.ActionOperator.Run(
                solutionContext,
                solutionContextActions);
        }

        /// <inheritdoc cref="Internal.ISampleSolutionOperations.PrepareAndGetContext(ITextOutput)"/>
        public void In_New_SampleSolutionContext(
            ITextOutput textOutput,
            Action<ISolutionContext> solutionContextAction = default)
        {
            var solutionContext = Internal.PrepareAndGetContext(textOutput);

            Instances.ActionOperator.Run(
                solutionContext,
                solutionContextAction);
        }
    }
}
