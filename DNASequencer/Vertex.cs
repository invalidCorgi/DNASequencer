using System;
using System.Collections.Generic;
using System.Text;

namespace DNASequencer
{
    class Vertex
    {
        public string Sequence { get; set; }
        public IEnumerable<Vertex> Predecessors { get; set; }
        public IEnumerable<Vertex> Successors { get; set; }
        public int Visited { get; set; }

        public Vertex FindNextVertex()
        {
            throw new NotImplementedException();
        }
    }
}
