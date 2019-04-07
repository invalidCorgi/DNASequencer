using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNASequencer
{
    class Solution
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public string Sequence { get; set; }
        public int UsedVerticesCount {
            get
            {
                return Vertices.Distinct().Count();
            }
        }
    }
}
