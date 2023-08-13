using System;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.L0039.T000;
using R5T.T0131;
using R5T.T0195.Extensions;
using R5T.T0198;
using R5T.T0201;
using R5T.T0207;


namespace R5T.O0013
{
    [ValuesMarker]
    public partial interface ISolutionContextOperationSets : IValuesMarker
    {
        public Func<ISolutionContext, Task>[] Create_WebLibraryForBlazorWithConstructionServerAndClient(
            WebLibraryWithConstructionSolutionSpecification solutionSpecification,
            IsSet<IRepositoryUrl> repositoryUrl,
            // Take in a creation output instance to allow capturing all solutionp and project path values.
            WebLibraryWithConstructionCreationOutput creationOutput)
        {
            return new[]
            {
                Instances.SolutionContextOperations.Create_New_SolutionFile,
                solutionContext =>
                {
                    creationOutput.SolutionFilePath = solutionContext.SolutionFilePath;

                    return Task.CompletedTask;
                },
                Instances.SolutionContextOperations.In_Add_ProjectContext(
                    solutionSpecification.WebLibraryProjectSpecification.ProjectName,
                    Instances.ProjectContextOperations.Create_RazorClassLibrary(
                        solutionSpecification.WebLibraryProjectSpecification.ProjectDescription,
                        repositoryUrl
                    ),
                    projectContext =>
                    {
                        creationOutput.WebLibraryProjectFilePath = projectContext.ProjectFilePath;

                        return Task.CompletedTask;
                    }
                ),
                Instances.SolutionContextOperations.In_Add_ProjectContext(
                    solutionSpecification.BlazorClientProjectSpecification.ProjectName,
                    Instances.ProjectContextOperations.Create_BlazorClient(
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
                ),
                Instances.SolutionContextOperations.In_Add_ProjectContext(
                    solutionSpecification.ServerProjectSpecification.ProjectName,
                    Instances.ProjectContextOperations.Create_WebServerForBlazorClient(
                        solutionSpecification.ServerProjectSpecification.ProjectDescription,
                        repositoryUrl,
                        () => creationOutput.BlazorClientProjectFilePath,
                        projectFilePath =>
                        {
                            creationOutput.WebServerProjectFilePath = projectFilePath;

                            return Task.CompletedTask;
                        }
                    )
                ),
                Instances.SolutionContextOperations.Set_DefaultStartupProject(
                    () => creationOutput.WebServerProjectFilePath)
            };
        }
    }
}
