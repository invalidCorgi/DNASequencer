using System;
using System.Collections.Generic;
using System.Text;

namespace DNASequencer
{
    class Vertex
    {
        public string Sequence { get; set; }
        public List<Arrow> Predecessors { get; set; } = new List<Arrow>();
        public List<Arrow> Successors { get; set; } = new List<Arrow>();
        public int Visited { get; set; } = 0;

        public Vertex FindNextVertex()
        {
            throw new NotImplementedException();
        }
    }
}
