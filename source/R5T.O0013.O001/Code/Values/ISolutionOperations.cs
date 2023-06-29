using System;
using System.Threading.Tasks;

using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0159;


namespace R5T.O0013.O001
{
    [ValuesMarker]
    public partial interface ISolutionOperations : IValuesMarker
    {
        private static Internal.ISolutionOperations Internal => O001.Internal.SolutionOperations.Instance;


        /// <inheritdoc cref="Internal.ISolutionOperations.PrepareAndGetContext(ITextOutput)"/>
        public Task In_New_SampleSolutionContext(
            ITextOutput textOutput,
            Func<ISolutionContext, Task> solutionContextAction = default)
        {
            var solutionContext = Internal.PrepareAndGetContext(textOutput);

            return Instances.ActionOperator.Run(
                solutionContextAction,
                solutionContext);
        }

        /// <inheritdoc cref="Internal.ISolutionOperations.PrepareAndGetContext(ITextOutput)"/>
        public void In_New_SampleSolutionContext(
            ITextOutput textOutput,
            Action<ISolutionContext> solutionContextAction = default)
        {
            var solutionContext = Internal.PrepareAndGetContext(textOutput);

            Instances.ActionOperator.Run(
                solutionContextAction,
                solutionContext);
        }
    }
}
