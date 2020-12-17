using System;
using System.Collections.Generic;

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
            List<Vertex> visitedVertices = new List<Vertex>();                                      // List of vertexes we have already explored
            List<Path> pathList = new List<Path>();                                                 // Keep track of all paths form starting node
            uint minCost = uint.MaxValue;
            Path path = new Path(start);                                                            // Path initialized
            String pathStr = string.Empty;

                                                                                                    // Only care about the possible paths from Start to End, so generating all paths from all nodes would be useless and time consuming
            foreach (var edge in start.Edges)
            {
                pathList.Add(TraverseNode(edge.Key, visitedVertices, path));
                visitedVertices.Clear();
                visitedVertices.Add(start);                                                         // Ensure start is always in list
            }
           
           for(int i = 0; i < pathList.Count; i++)                                                  // Iterate through all generated paths
			{
                pathStr = pathList[i].ToString();
                if (pathStr.EndsWith(end.ToString()))                                               // Only care about the generated paths ending at the "end" vertex
				{
                    if (pathList[i].Cost < minCost)                                                 // Check if cost of path is less than the minimum path cost
                    {
                        path = pathList[i];
                    }
                }
			}

            return path;
         }
          
        // Recursively explore a nodes possible paths with respect to the vertices that have been visited
        public Path TraverseNode(Vertex nextNode, List<Vertex> visitedVertices, Path currentPath)
		{
                                                                                                     // If node is available and not in list, add to path then recursively keep exploring this path
            if (!visitedVertices.Contains(nextNode)){
                visitedVertices.Add(nextNode);
                currentPath.TryMakeNext(nextNode,out currentPath);
                foreach(var edge in nextNode.Edges)
				{
                    nextNode = edge.Key;
                    TraverseNode(nextNode, visitedVertices, currentPath);
				}
            }
            // If dead end reached, simply return
            return currentPath;
		}
    }
}