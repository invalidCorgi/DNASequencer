using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            var successors = Successors.Select(x => new
            {
                x,
                Weight = x.Weight + Graph.DWeight * Visited + x.To.Successors.Select(y => y.Weight + Graph.DWeight * y.To.Visited).Max(y => y)
            });
            return successors.Where(x => x.Weight == successors.Max(y => y.Weight)).Select(x => x.x).First();
        }
    }
}
