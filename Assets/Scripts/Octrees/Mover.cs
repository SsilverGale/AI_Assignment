using System.Collections.Generic;
using System.Linq;
using Octrees;
using UnityEngine;

public class Mover : MonoBehaviour
{
    float speed = 5f;
    float accuracy = 1f;
    float turnSpeed = 5f;
    public List<Transform> PatrolWaypoints;
    int currentWaypoint;
    OctreeNode currentNode;
    Vector3 destination;
    public GameObject player;

    public Octree_Generator octree_Generator;
    Graph graph;

    void Start()
    {
        graph = octree_Generator.waypoints;
        currentNode = GetClosestNode(transform.position);
    }

    void Update()
    {
        if (graph == null) return;

        if (graph.GetPathLength() == 0 || currentWaypoint >= graph.GetPathLength())
        {
            //currentNode = GetClosestNode(transform.position);
            //destination = GetClosestNode(player.transform.position).bounds.center;
            //SetDestination(player.transform.position);
            return;
        }

        if(Vector3.Distance(graph.GetPathNode(currentWaypoint).bounds.center, transform.position) < accuracy){
            currentWaypoint++;
            Debug.Log("Waypoint reached!");
        }

        if (currentWaypoint < graph.GetPathLength())
        {
            currentNode = graph.GetPathNode(currentWaypoint);
            destination = currentNode.bounds.center;

            Vector3 direction = destination - transform.position;
            direction.Normalize();

            //Movement
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction), turnSpeed*Time.deltaTime);
            transform.Translate(0,0,speed*Time.deltaTime);
        }

    }

    OctreeNode GetClosestNode(Vector3 position)
    {
        return octree_Generator.ot.FindClosestNode(position);
    }

    void GetRandomDestination()
    {
        OctreeNode destinationNode;
        do
        {
            destinationNode = graph.nodes.ElementAt(Random.Range(0,graph.nodes.Count)).Key;
        } while (!graph.Astar(currentNode,destinationNode));
        currentWaypoint = 0;
    }

public void SetDestination(Vector3 position)
{
    OctreeNode targetNode = GetClosestNode(position);

    // Recalculate path to player
    if (graph.Astar(currentNode, targetNode))
    {
        currentWaypoint = 0;
    }
}



    void DrawGizmos()
    {
        Gizmos.color = Color.purple;       
    }
}
