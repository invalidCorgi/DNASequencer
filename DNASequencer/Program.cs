using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DNASequencer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filePaths = Directory.GetFiles("../../../input/");
            using(var writer = new StreamWriter("result.csv", false))
            {
                writer.WriteLine("instance;time[s];output sequence;all vertices;used vertices;used vertices[%]");
                foreach (var filePath in filePaths)
                {
                    var graph = new Graph(0, 0, 10, 1);
                    graph.LoadVerticesFromFile(filePath);
                    var startDateTime = DateTime.Now;
                    graph.MakeEmptyArrowsBetweenVertices();
                    graph.GiveWeightToArrows();
                    var startingVertices = graph.Select(x => new
                    {
                        x,
                        pred = x.Predecessors.Where(y => y.Distance <= (graph.L / 2)).Count()
                    });
                    startingVertices = startingVertices.OrderBy(x => x.pred).Take(startingVertices.Count() / 20);
                    var threadResults = new List<Task<Solution>>();
                    foreach (var startingVertex in startingVertices)
                    {
                        threadResults.Add(Task.Run(() => {
                            var threadGraph = graph.Copy();
                            return threadGraph.SolveProblem(startingVertex.x.Sequence);
                        }));
                    }
                    Task.WaitAll(threadResults.ToArray());
                    var bestSolutionThread = threadResults.MaxBy(x => x.Result.UsedVerticesCount).First();
                    var solution = bestSolutionThread.Result;
                    var endDateTime = DateTime.Now;
                    var instance = filePath.Split('/').Last();
                    var executionTimeInSeconds = (endDateTime - startDateTime).TotalSeconds;
                    var usedVerticesCount = solution.UsedVerticesCount;
                    writer.WriteLine($"{instance};" +
                        $"{executionTimeInSeconds};" +
                        $"{solution.Sequence};" +
                        $"{graph.Count};" +
                        $"{usedVerticesCount};" +
                        $"{(double)usedVerticesCount / graph.Count}");
                }
                writer.Flush();
            }
        }
    }
}
