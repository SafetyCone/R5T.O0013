using System;


namespace R5T.O0013.Construction
{
    public static class Instances
    {
        public static L0038.IApplicationContextOperator ApplicationContextOperator => L0038.ApplicationContextOperator.Instance;
        public static F0033.INotepadPlusPlusOperator NotepadPlusPlusOperator => F0033.NotepadPlusPlusOperator.Instance;
        public static L0039.O000.ISolutionContextOperations SolutionContextOperations => L0039.O000.SolutionContextOperations.Instance;
        public static O001.ISampleSolutionOperations SolutionOperations => O001.SampleSolutionOperations.Instance;
        public static IValues Values => Construction.Values.Instance;
        public static F0088.IVisualStudioOperator VisualStudioOperator => F0088.VisualStudioOperator.Instance;
    }
}