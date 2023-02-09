using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public Block block;
    public BlockState state;
    public List<Vector2Int> listPosNeighbour;
    public Vector2Int coordinate;
    public int fCost => gCost + hCost;
    public int gCost;
    public int hCost;
    public Node parent;
    public List<Node> path;

    public List<Vector2Int> GetBlock()
    {
        return block.StatesFromPos(coordinate)[state];
    }

    public bool MoveAble(Node node)
    {
        if (FindBestPosition.Instance.CheckBlockValid(block.StatesFromPos(coordinate)[state]))
            return true;
        foreach (var state in node.block.States)
        {
            if (FindBestPosition.Instance.CheckBlockValid(node.block.StatesFromPos(coordinate)[state]))
            {
                this.state = state;
                return true;
            }
        }
        return false;
    }
}
