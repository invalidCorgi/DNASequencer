using System;

namespace DNASequencer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Graph().LoadVerticesFromFile("../../../input/10.500+200");
            Console.WriteLine("Hello World!");
        }
    }
}
