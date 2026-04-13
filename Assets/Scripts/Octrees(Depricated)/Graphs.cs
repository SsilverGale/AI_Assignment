using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Octrees
{
    public class Node
    {
        static int nextID;
        public readonly int id;

        public float f,g,h;
        public Node from; //what node we came from

        public List<Edge> edges = new();

        public OctreeNode octreeNode;

        public Node(OctreeNode octreeNode)
        {
            this.id = nextID++;
            this.octreeNode = octreeNode;
        }

        public override bool Equals(object obj) => obj is Node other && id == other.id;
        public override int GetHashCode() => id.GetHashCode();

    }

    public class Edge
    {
        public readonly Node a, b;

        public Edge(Node a, Node b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool Equals(object obj)
        {
            return obj is Edge other && ((a == other.a && b== other.b)|| (a == other.b && b == other.a));
        }

        public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode();

    }
    public class Graph
    {
        public readonly Dictionary<OctreeNode, Node> nodes = new();
        public readonly HashSet<Edge> edges = new();

        List<Node> pathList = new();
        public int GetPathLength() => pathList.Count;

        public OctreeNode GetPathNode(int index)
        {
            if (pathList == null) return null;

            if (index <0 || index >= pathList.Count)
            {
                Debug.Log("Index outta bounds");
                return null;
            }
            return pathList[index].octreeNode;
        }

        public bool Astar(OctreeNode startNode, OctreeNode endNode)
        {
            pathList.Clear();
            Node start = FindNode(startNode);
            Node end = FindNode(endNode);

            if(start == null || end == null)
            {
                Debug.LogError("Star or end node not found in the graph");
                return false;
            }
            SortedSet<Node> openSet = new(new NodeComparer());
            HashSet<Node> closedSet = new();
            int interationCount = 0; //used to make sure we don't over do it

            start.g = 0;
            start.h = Heuristic(start,end);
            start.f = start.g + start.h;
            start.from = null;
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                if (++interationCount > 500000)
                {
                    Debug.LogError("A* exceeded maximum iterations");
                    return false;
                }

                Node current = openSet.First();
                openSet.Remove(current);

                if (current.Equals(end))
                {
                    ReconstructPath(current);
                    return true;
                }

                closedSet.Add(current);

                foreach(Edge edge in current.edges)
                {
                    Node neighbour = Equals(edge.a,current) ? edge.b : edge.a;

                    if (closedSet.Contains(neighbour)) continue;

                    float tentative_gScore = current.g + Heuristic(current, neighbour);

                    if (tentative_gScore < neighbour.g || !openSet.Contains(neighbour))
                    {
                        neighbour.g = tentative_gScore;
                        neighbour.h = Heuristic(neighbour,end);
                        neighbour.f = neighbour.g + neighbour.h;
                        neighbour.from = current;
                        openSet.Add(neighbour);
                    }
                }
            }
            Debug.Log("No path found");

            return false;

        }

        void ReconstructPath(Node current)
        {
            while(current != null)
            {
                pathList.Add(current);
                current = current.from;
            }

            pathList.Reverse();
        }


        float Heuristic(Node a, Node b) => (a.octreeNode.bounds.center - b.octreeNode.bounds.center).sqrMagnitude;

        public class NodeComparer : IComparer<Node>
        {
            public int Compare(Node x, Node y){
                if (x== null || y ==null) return 0;

                int compare = x.f.CompareTo(y.f);
                if (compare == 0)
            {
                return x.id.CompareTo(y.id);
            }
            return compare;
            }
        }


        public void AddNode(OctreeNode octreeNode)
        {
            if (!nodes.ContainsKey(octreeNode))
            {
                nodes.Add(octreeNode, new Node(octreeNode));
            }
        }

        public void AddEdge(OctreeNode a, OctreeNode b)
        {
            Node nodeA = FindNode(a);
            Node nodeB = FindNode(b);

            //Just in case no nodes are found
            if(nodeA == null || nodeB == null)
            {
                return;
            }

            var edge = new Edge(nodeA, nodeB);
            if (edges.Add(edge)) //Check if it's unique
            {
                nodeA.edges.Add(edge);
                nodeB.edges.Add(edge);
            }
        }
        public void DrawGraph()
        {
            Gizmos.color = Color.yellow;
            //Draw all edges
            foreach (Edge edge in edges)
            {
                Gizmos.DrawLine(edge.a.octreeNode.bounds.center, edge.b.octreeNode.bounds.center);
            }

            //Draws every node
            foreach (var node in nodes.Values)
            {
                Gizmos.DrawWireSphere(node.octreeNode.bounds.center,0.2f);
            }
        }

        Node FindNode(OctreeNode octreeNode)
        {
            nodes.TryGetValue(octreeNode,out Node node);
            return node;
        }
    }

}
