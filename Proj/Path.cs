using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Proj
{
    public class Path
    {
        private Vertex[] Vertices { get; }
        public uint Cost { get; }
        private string Name { get; }

        /// <summary>
        /// create a path object starting at a given vertex
        /// </summary>
        /// <param name="start">starting vertex</param> 
        public Path(Vertex start)
        {
            Vertices = new[] {start};
            Cost = 0;
            Name = start.ToString();
        }
        
        /// <summary>
        /// Internal Path maker to cut down on any re-computation needed for cost/name
        /// instead of an iterative approach
        /// </summary>
        /// <param name="vertices">array of all vertices in the path</param>
        /// <param name="cost">total cost of the path</param>
        /// <param name="name">name of the path (string representation of the costs/vertices)</param>
        private Path(Vertex[] vertices, uint cost, string name)
        {
            Vertices = vertices;
            Cost = cost;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Contains(Vertex vertex)
        {
            return Vertices.Contains(vertex);
        }

        /// <summary>
        /// Makes the next iteration of the path to the next node
        /// </summary>
        /// <param name="next">next vertex in the path</param> 
        /// <param name="path">
        /// When this method returns, contains the path after adding the next vertex,
        /// if the vertex is found along the last vertex's edges;
        /// otherwise, the default value for the type of the value parameter.
        /// </param> 
        /// <returns></returns>
        public bool TryMakeNext(Vertex next, [NotNullWhen(true)] out Path path)
        {
            if (!Vertices[^1].Edges.TryGetValue(next, out var weight))
            {
                path = default!;
                return false;
            }
            
            var vertices = new Vertex[Vertices.Length + 1];
            Vertices.CopyTo(vertices, 0);
            vertices[^1] = next;
            path = new Path(vertices, Cost + weight, $"{Name} -({weight})-> {next}");

            return true;
        }

        public override bool Equals( object? obj)
        {
            return obj is Path path && Vertices.SequenceEqual(path.Vertices) && path.Cost == Cost;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vertices, Cost);
        }

        public static bool operator ==(Path left, Path right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Path left, Path right)
        {
            return !Equals(left, right);
        }
    }
}