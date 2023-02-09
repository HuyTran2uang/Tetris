using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance { get; private set; }
    Grid Grid => Grid.Instance;

    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        if (startNode.coordinate == targetNode.coordinate) return new List<Node>();

        //create open list and closed list;
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        //add start node to open list
        open.Add(startNode);
        //while
        while (open.Count > 0)
        {
            Node currentNode = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < currentNode.fCost || open[i].fCost == currentNode.fCost)
                {
                    if (open[i].hCost < currentNode.hCost)
                    {
                        if (open[i].hCost == 0)
                            open[i].state = targetNode.state;
                        currentNode = open[i];
                    }
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode == targetNode)
            {
                this.SetPathForNode(startNode, targetNode);
                return Grid.GetNodeFromPos(targetNode.coordinate).path;
            }

            foreach (Node neighbour in Grid.GetNeighbourNode(currentNode))
            {
                if (!neighbour.MoveAble(currentNode) || closed.Contains(neighbour))
                    continue;

                int new_gCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (new_gCost < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = new_gCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!open.Contains(neighbour))
                        open.Add(neighbour);
                }
            }
        }
        return null;
    }

    private void SetPathForNode(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode.coordinate != startNode.coordinate)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        endNode.path = path;
        // Node targetNode = Grid.GetNodeFromPos(path[path.Count - 1].coordinate);
        // targetNode.state = path[path.Count - 1].state;
        // targetNode.path = path;
        // Debug.Log("targetNode " + targetNode.state);
        Grid.SetNodeFromPos(endNode.coordinate, endNode);
    }


    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.coordinate.x - nodeB.coordinate.x);
        int dstY = Mathf.Abs(nodeA.coordinate.y - nodeB.coordinate.y);

        if (dstX > dstY)
            return 10 * (dstX - dstY);
        return 10 * (dstY - dstX);
    }

    private void Awake()
    {
        Instance = this;
    }
}
