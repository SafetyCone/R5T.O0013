using System;


namespace R5T.O0013
{
    public class SolutionContextOperations : ISolutionContextOperations
    {
        #region Infrastructure

        public static ISolutionContextOperations Instance { get; } = new SolutionContextOperations();


        private SolutionContextOperations()
        {
        }

        #endregion
    }
}
