using System;

namespace Proj
{
    public class Graph
    {
        private Vertex[] Vertices { get; }

        public Graph(Vertex[] vertices)
        {
            Vertices = vertices;
        }

        public Path Dijkstra(Vertex start, Vertex end)
        {
            throw new NotImplementedException();
        }
    }
}