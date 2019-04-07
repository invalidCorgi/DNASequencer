using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MoreLinq;

namespace DNASequencer
{
    class Vertex
    {
        public string Sequence { get; set; }
        public List<Arrow> Predecessors { get; set; } = new List<Arrow>();
        public List<Arrow> Successors { get; set; } = new List<Arrow>();
        public int Visited { get; set; } = 0;

        public Arrow FindNext()
        {
            return Successors
                    .MaxBy(x => x.Weight + Graph.DWeight * Visited
                        + x.To.Successors.Select(y => y.Weight + Graph.DWeight * y.To.Visited).Max())
                    .First();
        }
    }
}
