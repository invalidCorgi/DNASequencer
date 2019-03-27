using System;

namespace DNASequencer
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Graph();
            test.LoadVerticesFromFile("../../../input/10.500+200");
            test.MakeEmptyArrowsBetweenVertices();
        }
    }
}
