using System;
using System.Linq;
using Proj;

namespace ProjConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.FromFile(args[0]);
            var first = graph.Values.First();
            Console.Out.WriteLine($"Starting at: {first}");
            foreach (var (vertex, shortestPath) in graph.Dijkstra(first))
            {
                Console.Out.WriteLine($"{vertex} ({shortestPath.Cost}): {shortestPath}");
            }
        }
    }
}