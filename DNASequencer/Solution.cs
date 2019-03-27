using System;
using System.Collections.Generic;
using System.Text;

namespace DNASequencer
{
    class Solution
    {
        public IEnumerable<Vertex> Vertices { get; set; }
        public string Sequence { get; set; }
        public int Length {
            get
            {
                return Sequence.Length;
            }
        }
    }
}
