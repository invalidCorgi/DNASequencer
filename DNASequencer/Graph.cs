using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using MoreLinq;

namespace DNASequencer
{
    class Graph : List<Vertex>
    {
        public int N { get; set; }
        public int L { get; set; }
        private double AWeight { get; set; }
        private double BWeight { get; set; }
        private double CWeight { get; set; }
        public static double DWeight { get; set; }

        public Graph(double AWeight, double BWeight, double CWeight, double DWeight)
        {
            this.AWeight = AWeight;
            this.BWeight = BWeight;
            this.CWeight = CWeight;
            Graph.DWeight = DWeight;
        }

        public void LoadVerticesFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Add(new Vertex
                    {
                        Sequence = line
                    });
                }
            }
            var realFileName = fileName.Substring(fileName.LastIndexOf('.')+1);
            N = int.Parse(realFileName.Split(new List<char> { '+', '-' }.ToArray())[0]) + this[0].Sequence.Length - 1;
            L = this[0].Sequence.Length;
        }

        public void MakeEmptyArrowsBetweenVertices()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = i + 1; j < Count; j++)
                {
                    for (int k = this[i].Sequence.Length; k > 0; k--)
                    {
                        if (this[i].Sequence.EndsWith(this[j].Sequence.Substring(0,k)))
                        {
                            Arrow arrow = new Arrow
                            {
                                From = this[i],
                                To = this[j],
                                Distance = this[i].Sequence.Length - k
                            };
                            this[j].Predecessors.Add(arrow);
                            this[i].Successors.Add(arrow);
                        }
                        if (this[j].Sequence.EndsWith(this[i].Sequence.Substring(0,k)))
                        {
                            Arrow arrow = new Arrow
                            {
                                From = this[j],
                                To = this[i],
                                Distance = this[i].Sequence.Length - k
                            };
                            this[i].Predecessors.Add(arrow);
                            this[j].Successors.Add(arrow);
                        }
                    }
                }
            }
        }

        public void GiveWeightToArrows()
        {
            foreach(var node in this)
            {
                foreach (var arrow in node.Predecessors)
                {
                    arrow.Weight = AWeight * node.Successors.Count - BWeight * node.Predecessors.Count - CWeight * arrow.Distance;
                }
            }
        }

        public Solution SolveProblem(string startingVertexSequence)
        {
            var solution = new Solution();
            var startingVertex = this.Where(x => x.Sequence == startingVertexSequence).First();
            solution.Vertices.Add(startingVertex);
            solution.Sequence = startingVertex.Sequence;
            startingVertex.Visited++;
            while(true)
            {
                var lastAddedVertex = solution.Vertices.Last();
                var arrowToNextVertex = lastAddedVertex.FindNext();
                if (arrowToNextVertex != null && arrowToNextVertex.Distance + solution.Sequence.Length <= N)
                {
                    solution.Vertices.Add(arrowToNextVertex.To);
                    solution.Sequence += arrowToNextVertex.To.Sequence.Substring(L - arrowToNextVertex.Distance);
                    arrowToNextVertex.To.Visited++;
                }
                else
                {
                    Vertex last;
                    while((last = solution.Vertices.Last()).Visited > 1)
                    {
                        var beforeLast = solution.Vertices[solution.Vertices.Count - 2];
                        var arrow = beforeLast.Successors.Where(x => x.To == last).First();
                        solution.Vertices.RemoveAt(solution.Vertices.Count - 1);
                        solution.Sequence = solution.Sequence.Remove(solution.Sequence.Length - arrow.Distance);
                        last.Visited--;
                    }
                    if (solution.Sequence.Length + L > N)
                        break;
                    var newVertexCandidates = this.Where(x => x.Visited == 0 && x.Successors.Where(y => y.To.Visited == 0).Count() > 0);
                    if(newVertexCandidates.Count() == 0)
                    {
                        bool zeroNotVisited = false;
                        while(solution.Sequence.Length + L <= N)
                        {
                            var notVisitedVertex = this.Where(x => x.Visited == 0).FirstOrDefault();
                            if(notVisitedVertex != null)
                            {
                                solution.Vertices.Add(notVisitedVertex);
                                solution.Sequence += notVisitedVertex.Sequence;
                                notVisitedVertex.Visited++;
                            }
                            else
                            {
                                zeroNotVisited = true;
                                break;
                            }
                        }
                        if (zeroNotVisited)
                            break;
                    }
                    var newVertex = newVertexCandidates.MaxBy(x => x.Successors.Where(y => y.To.Visited == 0).Max(y => y.Weight + y.To.Visited)).First();
                    solution.Vertices.Add(newVertex);
                    solution.Sequence += newVertex.Sequence;
                    newVertex.Visited++;
                }
            }
            return solution;
        }

        public Graph Copy()
        {
            var graph = new Graph(AWeight, BWeight, CWeight, DWeight)
            {
                N = this.N,
                L = this.L
            };
            foreach (var vertex in this)
            {
                graph.Add(new Vertex
                {
                    Sequence = vertex.Sequence
                });
            }
            for (int i = 0; i < this.Count; i++)
            {
                var original = this[i];
                var copy = graph[i];
                foreach (var arrow in original.Predecessors)
                {
                    copy.Predecessors.Add(new Arrow
                    {
                        Distance = arrow.Distance,
                        Weight = arrow.Weight,
                        To = copy,
                        From = graph.Where(x => x.Sequence == arrow.From.Sequence).First()
                    });
                }
                foreach (var arrow in original.Successors)
                {
                    copy.Successors.Add(new Arrow
                    {
                        Distance = arrow.Distance,
                        Weight = arrow.Weight,
                        From = copy,
                        To = graph.Where(x => x.Sequence == arrow.To.Sequence).First()
                    });
                }
            }
            return graph;
        }
    }
}
