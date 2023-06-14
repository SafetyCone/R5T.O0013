using System;


namespace R5T.O0013
{
    public class SolutionSetContextOperations : ISolutionSetContextOperations
    {
        #region Infrastructure

        public static ISolutionSetContextOperations Instance { get; } = new SolutionSetContextOperations();


        private SolutionSetContextOperations()
        {
        }

        #endregion
    }
}
