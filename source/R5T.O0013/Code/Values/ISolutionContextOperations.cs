using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0000.Extensions;
using R5T.L0031.Extensions;
using R5T.L0039.T000;
using R5T.L0040.T000;
using R5T.T0131;
using R5T.T0172;
using R5T.T0195.Extensions;
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
                return solutionContext.Run(
                    Instances.SolutionContextOperations_Generation.Create_Solution(
                        solutionFilePath =>
                        {
                            creationOutput.SolutionFilePath = solutionFilePath;
                        },
                        Instances.SolutionContextOperations.In_Add_ProjectContext(
                            solutionSpecification.WebLibraryProjectSpecification.ProjectName,
                            Create_BlazorClassLibrary
                        ),
                        Instances.SolutionContextOperations.In_Add_ProjectContext(
                            solutionSpecification.BlazorClientProjectSpecification.ProjectName,
                            Create_Client
                        ),
                        Instances.SolutionContextOperations.In_Add_ProjectContext(
                            solutionSpecification.ServerProjectSpecification.ProjectName,
                            Create_Server
                        ),
                        // Make sure the server is the default startup project.
                        Instances.SolutionContextOperations.Set_DefaultStartupProject(
                            () => creationOutput.WebServerProjectFilePath)
                    )
                );
            };

            Task Create_BlazorClassLibrary(IProjectContext projectContext)
            {
                return projectContext.Run(
                    Instances.ProjectContextOperations_Generation.Create_RazorClassLibrary(
                        solutionSpecification.WebLibraryProjectSpecification.ProjectDescription,
                        repositoryUrl
                    ),
                    projectContext =>
                    {
                        creationOutput.WebLibraryProjectFilePath = projectContext.ProjectFilePath;

                        return Task.CompletedTask;
                    }
                );
            }

            Task Create_Client(IProjectContext projectContext)
            {
                return projectContext.Run(
                    Instances.ProjectContextOperations_Generation.Create_BlazorClient(
                        solutionSpecification.BlazorClientProjectSpecification.ProjectDescription,
                        repositoryUrl,
                        projectFilePath =>
                        {
                            creationOutput.BlazorClientProjectFilePath = projectFilePath;

                            return Task.CompletedTask;
                        }
                    ),
                    // Add the web library as a dependency of the front-end client.
                    async projectContext =>
                    {
                        await Instances.ProjectOperations.Add_ProjectReference_WithoutSolutionUpdate(
                            projectContext.ProjectFilePath,
                            creationOutput.WebLibraryProjectFilePath.ToProjectFileReference());
                    }
                );
            }

            Task Create_Server(IProjectContext projectContext)
            {
                return projectContext.Run(
                    Instances.ProjectContextOperations_Generation.Create_WebServerForBlazorClient(
                        solutionSpecification.ServerProjectSpecification.ProjectDescription,
                        repositoryUrl,
                        // Be careful with the Blazor client project file path: it may be captured as null!
                        creationOutput.BlazorClientProjectFilePath,
                        projectFilePath =>
                        {
                            creationOutput.WebServerProjectFilePath = projectFilePath;

                            return Task.CompletedTask;
                        }
                    )
                );
            }
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
    }
}
