using System;


namespace R5T.O0013
{
    public class SolutionContextOperationSets : ISolutionContextOperationSets
    {
        #region Infrastructure

        public static ISolutionContextOperationSets Instance { get; } = new SolutionContextOperationSets();


        private SolutionContextOperationSets()
        {
        }

        #endregion
    }
}
