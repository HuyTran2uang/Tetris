using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }
    Node[,] _grid;
    Map Map => Map.Instance;

    public void SetGrid(Block block)
    {
        _grid = new Node[Map.Size.x, Map.Size.y];

        for (int x = 0; x < Map.Size.x; x++)
        {
            for (int y = 0; y < Map.Size.y; y++)
            {
                _grid[x, y] = new Node()
                {
                    block = block,
                    coordinate = new Vector2Int(x, y),
                };
            }
        }
    }

    public Node GetNodeFromPos(Vector2Int position)
    {
        return _grid[position.x, position.y];
    }

    public void SetNodeFromPos(Vector2Int position, Node node)
    {
        _grid[position.x, position.y] = node;
    }

    public List<Node> GetNeighbourNode(Node node)
    {
        List<Node> neighborNodes = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (x != 0 && y != 0) continue;

                int xOfNeighbor = node.coordinate.x + x;
                int yOfNeighbor = node.coordinate.y + y;

                // 0 <= gridOfNeighbor < MaxSize
                if (xOfNeighbor < 0 || yOfNeighbor < 0 || xOfNeighbor >= Map.Size.x || yOfNeighbor >= Map.Size.y) continue;
                neighborNodes.Add(_grid[xOfNeighbor, yOfNeighbor]);
            }
        }

        return neighborNodes;
    }

    private void Awake()
    {
        Instance = this;
    }
}
