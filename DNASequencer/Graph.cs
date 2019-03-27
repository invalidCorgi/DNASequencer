using System;
using System.Collections.Generic;
using System.Text;

namespace DNASequencer
{
    class Graph : List<Vertex>
    {
        public void LoadVerticesFromFile()
        {
            throw new NotImplementedException();
        }

        public void MakeEmptyArrowsBetweenVertices()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = i + 1; j < Count; j++)
                {
                    for (int k = 0; k < this[i].Sequence.Length; k++)
                    {
                        if (this[i].Sequence.EndsWith(this[j].Sequence.Substring(k)))
                        {
                            Arrow arrow = new Arrow();
                            arrow.From = this[i];
                            arrow.To = this[j];
                            arrow.Distance = k;
                            this[j].Predecessors.Add(arrow);
                            this[i].Successors.Add(arrow);
                        }
                    }
                }
            }
            throw new NotImplementedException();
        }

        public void GiveWeightToArrows()
        {
            throw new NotImplementedException();
        }
    }
}
