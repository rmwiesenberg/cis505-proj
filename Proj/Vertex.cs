using System.Collections.Generic;

namespace Proj
{
    public class Vertex
    {
        public Dictionary<Vertex, uint> Edges { get; } = new Dictionary<Vertex, uint>();
        private string Name { get; }

        public Vertex(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public void Connect(Vertex vertex, uint weight, bool bidirectional = false)
        {
            Edges.Add(vertex, weight);
            if (bidirectional)
            {
                vertex.Connect(this, weight);
            }
        }
    }
}