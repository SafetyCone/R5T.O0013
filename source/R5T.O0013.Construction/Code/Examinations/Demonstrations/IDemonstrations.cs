using System;
using System.Threading.Tasks;

using R5T.L0038;
using R5T.T0141;


namespace R5T.O0013.Construction
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        public async Task In_New_SampleSolutionContext()
        {
            var (humanOutputTextFilePath, logFilePath) = await Instances.ApplicationContextOperator.In_ApplicationContext_Undated(
                Instances.Values.ApplicationName,
                ApplicationContextOperation);

            async Task ApplicationContextOperation(IApplicationContext applicationContext)
            {
                await Instances.SolutionOperations.In_New_SampleSolutionContext(
                    applicationContext.TextOutput,
                    async solutionContext =>
                    {
                        await Instances.SolutionContextOperations.Create_New_SolutionFile(solutionContext);

                        Instances.VisualStudioOperator.OpenSolutionFile(
                            solutionContext.SolutionFilePath.Value);
                    });
            }

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputTextFilePath,
                logFilePath);
        }
    }
}
