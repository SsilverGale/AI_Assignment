using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Octrees
{
    public class Octree
    {
        public OctreeNode root;
        public Bounds bounds;
        public Graph graph;

        List<OctreeNode> emptyLeavers = new();

        public Octree(GameObject[] worldObjects, float minNodeSize, Graph graph)
        {
            this.graph = graph;

            CalculateBounds(worldObjects);
            CreateTree(worldObjects,minNodeSize);

            GetEmptyLeaves(root);
            GetEdges();
        }

        public OctreeNode FindClosestNode(Vector3 position) => FindClosestNode(root,position);

        public OctreeNode FindClosestNode(OctreeNode node, Vector3 position)
        {
            OctreeNode found = null;
            for(int i = 0; i< node.children.Length; i++)
            {
                if (node.children[i].bounds.Contains(position))
                {
                    if (node.children[i].IsLeaf)
                    {
                        found = node.children[i];
                        break;
                    }
                    found = FindClosestNode (node.children[i], position);
                }
            }
            return found;
        }

        void GetEdges()
        {
            foreach (OctreeNode leaf in emptyLeavers)
            {
                foreach (OctreeNode otherleaf in emptyLeavers)
                {
                    if (leaf.bounds.Intersects(otherleaf.bounds))
                    {
                        graph.AddEdge(leaf,otherleaf);
                    }
                }
            }
        }
        //Checks which leaves are traversable
        void GetEmptyLeaves(OctreeNode node)
        {
            if (node.IsLeaf && node.objects.Count == 0)
            {
                emptyLeavers.Add(node);
                graph.AddNode(node);
                return;
            }

            if (node.children ==null) return;

            foreach (OctreeNode child in node.children)
            {
                GetEmptyLeaves(child);
            }

            //find all edges between siblings
            for (int i = 0; i < node.children.Length; i++)
            {
                for (int j = i +1; j < node.children.Length; j++)
                {
                    graph.AddEdge(node.children[i], node.children[j]);
                }
            }
        }

        void CreateTree(GameObject[] worldObjects, float minNodeSize)
        {
            root = new OctreeNode(bounds,minNodeSize);

            foreach(var obj in worldObjects)
            {
                root.Divide(obj);
            }
        }

        void CalculateBounds(GameObject[] worldObjects)
        {
            foreach(var obj in worldObjects)
            {
                bounds.Encapsulate(obj.GetComponent<Collider>().bounds);
            }

            //Makes bounds into a cube
            Vector3 size = Vector3.one * Mathf.Max(bounds.size.x, bounds.size.z, bounds.size.z) * 0.6f;
            size.y = 5f;
            bounds.SetMinMax(bounds.center - size, bounds.center + size);
        }
    }
}
