using System;
using System.Linq;

namespace DNASequencer
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Graph(0, 0, 10, 1);
            test.LoadVerticesFromFile("../../../input/9.200+80");
            test.MakeEmptyArrowsBetweenVertices();
            test.GiveWeightToArrows();
            test.SolveProblem();
        }
    }
}
