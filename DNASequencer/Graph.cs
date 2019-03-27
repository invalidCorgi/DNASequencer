using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DNASequencer
{
    class Graph : List<Vertex>
    {
        public int N { get; set; }
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
