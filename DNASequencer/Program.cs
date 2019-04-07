using System;
using System.IO;
using System.Linq;

namespace DNASequencer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filePaths = Directory.GetFiles(@"../../../input/");
            var test = new Graph(0, 0, 10, 1);
            test.LoadVerticesFromFile("../../../input/9.200+80");
            test.MakeEmptyArrowsBetweenVertices();
            test.GiveWeightToArrows();
            var a = test.Copy();
            var b = test.SolveProblem();
            var c = a.SolveProblem();
        }
    }
}
