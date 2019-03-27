using System;
using System.Collections.Generic;
using System.Text;

namespace DNASequencer
{
    class Arrow
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Distance { get; set; }
        public double Weight { get; set; }
    }
}
