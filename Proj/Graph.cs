using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Proj
{
    public class Graph : Dictionary<string, Vertex>
    {
        public IEnumerable<KeyValuePair<Vertex, Path>> Dijkstra(Vertex start)
        {
            foreach (var output in start.Dijkstra())
            {
                yield return output;
            }
        }

        public bool Dijkstra(Vertex start, Vertex end, [NotNullWhen(true)] out Path path)
        {
            return start.Dijkstra(end, out path);
        }

        public static Graph FromFile(string filepath)
        {
            var rand = new Random(505);
            var graph = new Graph();

            foreach (var line in File.ReadLines(filepath).Skip(1))
            {
                var segments = line.Split(" ");
                var startName = segments[0];
                if (!graph.TryGetValue(startName, out var startVertex))
                {
                    startVertex = new Vertex(startName);
                    graph.Add(startName, startVertex);
                }

                var endName = segments[1];
                if (!graph.TryGetValue(endName, out var endVertex))
                {
                    endVertex = new Vertex(endName);
                    graph.Add(endName, endVertex);
                }

                var weight = (uint) Math.Abs(rand.Next(1, 10));
                if (segments[2] == "1")
                {
                    startVertex.Connect(endVertex, weight);
                }
                else
                {
                    endVertex.Connect(endVertex, weight);
                }
            }
            return graph;
        }
    }
}