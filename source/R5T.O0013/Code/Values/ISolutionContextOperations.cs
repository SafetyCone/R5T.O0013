using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0000.Extensions;
using R5T.L0031.Extensions;
using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0172;
using R5T.T0198;
using R5T.T0201;
using R5T.T0207;


namespace R5T.O0013
{
    [ValuesMarker]
    public partial interface ISolutionContextOperations : IValuesMarker
    {
        /// <summary>
        /// Create a web library for components meant to be consumed via a Blazor client.
        /// Includes both server and Blazor client construction projects.
        /// </summary>
        public Func<ISolutionContext, Task> Create_WebLibraryForBlazorWithConstructionServerAndClient(
            WebLibraryWithConstructionSolutionSpecification solutionSpecification,
            IsSet<IRepositoryUrl> repositoryUrl,
            // Take in a creation output instance to allow capturing all solutionp and project path values.
            WebLibraryWithConstructionCreationOutput creationOutput)
        {
            return solutionContext =>
            {
                var solutionContextOperations = Instances.SolutionContextOperationSets.Create_WebLibraryForBlazorWithConstructionServerAndClient(
                    solutionSpecification,
                    repositoryUrl,
                    creationOutput);

                return solutionContext.Run(
                    Instances.SolutionContextOperations_Generation.Create_Solution(
                        solutionContextOperations
                    )
                );
            };
        }

        public Func<ISolutionContext, Task> Create_Solution(
            Action<ISolutionFilePath> solutionFilePathHandler,
            IEnumerable<Func<ISolutionContext, Task>> createSolutionOperations)
        {
            Task Internal(ISolutionContext solutionContext)
            {
                var createSolutionOperations_Modified = createSolutionOperations
                    .Prepend(
                        Instances.SolutionContextOperations.Create_New_SolutionFile,
                        context =>
                        {
                            solutionFilePathHandler(context.SolutionFilePath);

                            return Task.CompletedTask;
                        }
                    )
                    ;

                return solutionContext.Run(
                    createSolutionOperations_Modified);
            }

            return Internal;
        }

        public Func<ISolutionContext, Task> Create_Solution(
            Action<ISolutionFilePath> solutionFilePathHandler,
            params Func<ISolutionContext, Task>[] createSolutionOperations)
        {
            return this.Create_Solution(
                solutionFilePathHandler,
                createSolutionOperations.AsEnumerable());
        }

        public Func<ISolutionContext, Task> Create_Solution(
            IEnumerable<Func<ISolutionContext, Task>> createSolutionOperations)
        {
            Task Internal(ISolutionContext solutionContext)
            {
                var createSolutionOperations_Modified = createSolutionOperations
                    .Prepend(
                        Instances.SolutionContextOperations.Create_New_SolutionFile
                    )
                    ;

                return solutionContext.Run(
                    createSolutionOperations_Modified);
            }

            return Internal;
        }

        public Func<ISolutionContext, Task> Create_Solution(
            params Func<ISolutionContext, Task>[] createSolutionOperations)
        {
            return this.Create_Solution(
                createSolutionOperations.AsEnumerable());
        }
    }
}
