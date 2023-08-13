using System;

using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0159;


namespace R5T.O0013.O001.Internal
{
    [ValuesMarker]
    public partial interface ISampleSolutionOperations : IValuesMarker
    {
        /// <summary>
        /// The sample solution name is <see cref="Z0046.IValues.Sample_SolutionName"/>.
        /// </summary>
        public ISolutionContext PrepareAndGetContext(
            ITextOutput textOutput)
        {
            var solutionName = Instances.Values.Sample_SolutionName;
            var solutionDirectoryParentDirectoryPath = Instances.DirectoryPaths.Sample_SolutionsDirectoryPath;

            var solutionContext = Instances.SolutionContextConstructor.Get_SolutionContext(
                solutionName,
                solutionDirectoryParentDirectoryPath,
                textOutput);

            // If the solution directory exists, delete it.
            var solutionDirectoryPath = Instances.SolutionPathsOperator.Get_SolutionDirectoryPath(
                solutionContext.SolutionFilePath);

            Instances.FileSystemOperator.DeleteDirectory_Idempotent(
                solutionDirectoryPath.Value);

            return solutionContext;
        }
    }
}
