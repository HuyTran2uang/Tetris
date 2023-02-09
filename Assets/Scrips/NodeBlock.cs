using System.Collections.Generic;
using UnityEngine;

public class NodeBlock
{
    public Block block;
    public BlockState state;
    public Vector2Int position;
    public List<Vector2Int> listPosNeighbour;

    public List<Vector2Int> GetBlock()
    {
        return block.StatesFromPos(position)[state];
    }
}