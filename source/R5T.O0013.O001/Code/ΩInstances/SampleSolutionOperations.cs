using System;


namespace R5T.O0013.O001
{
    public class SampleSolutionOperations : ISampleSolutionOperations
    {
        #region Infrastructure

        public static ISampleSolutionOperations Instance { get; } = new SampleSolutionOperations();


        private SampleSolutionOperations()
        {
        }

        #endregion
    }
}
