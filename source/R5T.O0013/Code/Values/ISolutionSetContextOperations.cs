using System;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.L0031.Extensions;
using R5T.L0039.T000;
using R5T.L0040.T000;
using R5T.T0131;
using R5T.T0172;
using R5T.T0187;
using R5T.T0198;
using R5T.T0201;
using R5T.T0204;


namespace R5T.O0013
{
    [ValuesMarker]
    public partial interface ISolutionSetContextOperations : IValuesMarker
    {
        /// <summary>
        /// Prior work: R5T.S0053.SolutionScripts.New_WebBlazorClientAndServer()
        /// </summary>
        public Func<ISolutionSetContext, Task> Create_BlazorClientAndServer(
            ISolutionDirectoryPath solutionDirectoryPath,
            BlazorClientWithWebServerCreationOutput creationResult,
            IsSet<IRepositoryUrl> repositoryUrl,
            ISolutionName solutionName,
            IProjectSpecification blazorClientProjectSpecification,
            IProjectSpecification serverProjectSpecification)
        {
            return async solutionSetContext =>
            {
                await Instances.SolutionDirectoryContextOperator.In_SolutionDirectoryContext(
                    solutionDirectoryPath,
                    solutionSetContext.TextOutput,
                    Instances.SolutionDirectoryContextOperations.In_SolutionContext(
                        solutionName,
                        Create_SolutionOperation
                    )
                );

                Task Create_SolutionOperation(ISolutionContext solutionContext)
                {
                    return solutionContext.Run(
                        Instances.SolutionContextOperations_Generation.Create_Solution(
                            solutionFilePath =>
                            {
                                creationResult.SolutionFilePath = solutionFilePath;
                            },
                            Instances.SolutionContextOperations.In_Add_ProjectContext(
                                blazorClientProjectSpecification.ProjectName,
                                Create_Client
                            ),
                            Instances.SolutionContextOperations.In_Add_ProjectContext(
                                serverProjectSpecification.ProjectName,
                                Create_Server
                            )
                        )
                    );
                }

                Task Create_Client(IProjectContext projectContext)
                {
                    return projectContext.Run(
                        Instances.ProjectContextOperations_Generation.Create_BlazorClient(
                            blazorClientProjectSpecification.ProjectDescription,
                            repositoryUrl,
                            projectFilePath =>
                            {
                                creationResult.BlazorClientProjectFilePath = projectFilePath;

                                return Task.CompletedTask;
                            }
                        )
                    );
                }

                Task Create_Server(IProjectContext projectContext)
                {
                    return projectContext.Run(
                        Instances.ProjectContextOperations_Generation.Create_WebServerForBlazorClient(
                            serverProjectSpecification.ProjectDescription,
                            repositoryUrl,
                            // Be careful with the Blazor client project file path: it may be captured as null!
                            creationResult.BlazorClientProjectFilePath,
                            projectFilePath =>
                            {
                                creationResult.WebServerProjectFilePath = projectFilePath;

                                return Task.CompletedTask;
                            }
                        )
                    );
                }
            };
        }
    }
}
