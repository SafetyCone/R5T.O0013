using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000.Extensions;
using R5T.L0031.Extensions;
using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0172;


namespace R5T.O0013
{
    [ValuesMarker]
    public partial interface ISolutionContextOperations : IValuesMarker
    {
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
    }
}
