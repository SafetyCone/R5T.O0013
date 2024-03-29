using System;


namespace R5T.O0013
{
    public static class Instances
    {
        public static O0005.IProjectOperations ProjectOperations => O0005.ProjectOperations.Instance;
        public static O0010.IProjectContextOperations ProjectContextOperations => O0010.ProjectContextOperations.Instance;
        public static L0039.F000.ISolutionDirectoryContextOperator SolutionDirectoryContextOperator => L0039.F000.SolutionDirectoryContextOperator.Instance;
        public static L0039.O000.ISolutionDirectoryContextOperations SolutionDirectoryContextOperations => L0039.O000.SolutionDirectoryContextOperations.Instance;
        public static L0039.O001.ISolutionContextOperations SolutionContextOperations => L0039.O001.SolutionContextOperations.Instance;
        public static ISolutionContextOperationSets SolutionContextOperationSets => O0013.SolutionContextOperationSets.Instance;
        public static ISolutionContextOperations SolutionContextOperations_Generation => O0013.SolutionContextOperations.Instance;
    }
}