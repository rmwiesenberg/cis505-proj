using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        public IEnumerable<KeyValuePair<Vertex, Path>> Dijkstra()
        {
            var visited = new Dictionary<Vertex, Path>();
            var toVisit = new Dictionary<Vertex, Path> {{this, new Path(this)}};

            while (toVisit.Any())
            {
                var shortest = toVisit.First();
                foreach (var vertexPath in toVisit
                    .Where(vertexPath => vertexPath.Value.Cost < shortest.Value.Cost))
                {
                    shortest = vertexPath;
                }

                toVisit.Remove(shortest.Key);
                visited.Add(shortest.Key, shortest.Value);
                yield return shortest;

                foreach (var nextVertex in shortest.Key.Edges.Select(edge => edge.Key)
                    .Where(nextVertex => !visited.ContainsKey(nextVertex)))
                {
                    shortest.Value.TryMakeNext(nextVertex, out var path); 
                    if (toVisit.ContainsKey(nextVertex))
                    {
                        if (path.Cost < toVisit[nextVertex].Cost)
                        {
                            toVisit[nextVertex] = path;
                        }
                    }
                    else
                    {
                        toVisit[nextVertex] = path;
                    }
                }
            }
        }
        
        public bool Dijkstra(Vertex end, [NotNullWhen(true)] out Path path)
        {
            foreach (var (vertex, shortestPath) in Dijkstra())
            {
                if (vertex != end) continue;
                path = shortestPath;
                return true;
            }

            path = default!;
            return false;
        }
    }
}