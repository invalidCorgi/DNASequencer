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
            throw new NotImplementedException();
        }

        public void GiveWeightToArrows()
        {
            throw new NotImplementedException();
        }
    }
}
