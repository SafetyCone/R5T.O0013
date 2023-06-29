using System;


namespace R5T.O0013.O001.Internal
{
    public class SolutionOperations : ISolutionOperations
    {
        #region Infrastructure

        public static ISolutionOperations Instance { get; } = new SolutionOperations();


        private SolutionOperations()
        {
        }

        #endregion
    }
}
