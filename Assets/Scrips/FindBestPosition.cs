using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBestPosition : MonoBehaviour
{
    public static FindBestPosition Instance { get; private set; }
    public Vector2Int Size => Map.Instance.Size;
    public Dictionary<Vector2Int, Square> D_Grid => Map.Instance.Grid;
    public List<Square> Squares => Map.Instance.Squares;
    public List<Block> Blocks => Map.Instance.Blocks;

    private bool CheckPosValid(Vector2Int position)
    {
        if (position.x < 0 || position.y < 0 || position.x >= Size.x || position.y >= Size.y) return false;
        return true;
    }

    private bool CheckPosNearEdge(Vector2Int position)
    {
        if (position.x == -1 || position.x == Size.x || position.y == Size.y) return true;
        return false;
    }
    //check near edge or near has value
    private bool IsNeighbour(Vector2Int position)
    {
        if (this.CheckPosValid(position))
            return D_Grid[position].hasValue;
        return this.CheckPosNearEdge(position);
    }
    //check block in map
    public bool CheckBlockValid(List<Vector2Int> listPos)
    {
        foreach (var pos in listPos)
        {
            if (!this.CheckPosValid(pos)) return false;
            if (D_Grid[pos].hasValue) return false;
        }
        return true;
    }
    //return list position of neighbour with state from position
    private List<Vector2Int> GetListPosNeighbour(Block block, BlockState state, Vector2Int position)
    {
        List<Vector2Int> listPosOfBlock = block.StatesFromPos(position)[state];
        if (!this.CheckBlockValid(listPosOfBlock)) return null;
        List<Vector2Int> listPosition = new List<Vector2Int>();
        foreach (var pos in listPosOfBlock)
        {
            if (this.IsNeighbour(pos + Vector2Int.up) && !listPosOfBlock.Contains(pos + Vector2Int.up))
                listPosition.Add(pos + Vector2Int.up);
            if (this.IsNeighbour(pos + Vector2Int.down) && !listPosOfBlock.Contains(pos + Vector2Int.down))
                listPosition.Add(pos + Vector2Int.down);
            if (this.IsNeighbour(pos + Vector2Int.left) && !listPosOfBlock.Contains(pos + Vector2Int.left))
                listPosition.Add(pos + Vector2Int.left);
            if (this.IsNeighbour(pos + Vector2Int.right) && !listPosOfBlock.Contains(pos + Vector2Int.right))
                listPosition.Add(pos + Vector2Int.right);
        }
        return listPosition;
    }
    //check block pressing
    private bool Check2BlockPressing(List<Vector2Int> blockA, List<Vector2Int> blockB)
    {
        foreach (var pos in blockA)
            if (blockB.Contains(pos)) return true;
        return false;
    }
    //check block pressing someone in list node
    private bool CheckBlockPressingListNode(List<Vector2Int> block, List<Node> nodes)
    {
        foreach (var node in nodes)
            foreach (var pos in node.GetBlock())
                if (block.Contains(pos)) return true;
        return false;
    }
    //return nodes have max neighbour with all states
    private List<Node> GetListNodeBestNeighbourOfStates(Block block, Vector2Int position)
    {
        List<Node> listNode = new List<Node>();
        List<BlockState> states = block.States;
        int maxNeighbourOfState = 0;
        foreach (var state in states)
        {
            List<Vector2Int> listPosNeighbour = this.GetListPosNeighbour(block, state, position);
            if (listPosNeighbour == null) continue;
            int countNeighbour = listPosNeighbour.Count;
            if (countNeighbour > maxNeighbourOfState)
            {
                maxNeighbourOfState = countNeighbour;
                listNode = new List<Node>();
                Node node = new Node();
                node.block = block;
                node.state = state;
                node.coordinate = position;
                node.listPosNeighbour = listPosNeighbour;
                listNode.Add(node);
            }
            else if (countNeighbour == maxNeighbourOfState)
            {
                Node node = new Node();
                node.block = block;
                node.state = state;
                node.coordinate = position;
                node.listPosNeighbour = listPosNeighbour;
                if (this.CheckBlockPressingListNode(node.GetBlock(), listNode)) continue;
                listNode.Add(node);
            }
        }
        return listNode;
    }
    //
    List<Vector2Int> listStartPos = new List<Vector2Int>();
    private void SetListStartPos()
    {
        for (int i = 0; i < Size.x; i++)
        {
            listStartPos.Add(new Vector2Int(i, 0));
            listStartPos.Add(new Vector2Int(i, 1));
        }
    }
    //check path found
    private bool CheckPathFound(Node targetNode)
    {
        foreach (var startPos in listStartPos)
        {
            List<Node> listNode = PathFinding.Instance.FindPath(Grid.Instance.GetNodeFromPos(startPos), targetNode);
            if (listNode != null)
                return true;
        }
        return false;
    }
    //return nodes are best max neighbour with all position
    private List<Node> ListBestNode(Block block)
    {
        List<Node> listNode = new List<Node>();
        //pos have a lot of neighbour most
        int maxNeighbour = 0;
        foreach (var pos in D_Grid.Keys)
        {
            //check all start pos can spawn
            List<Node> listNodeOfPos = this.GetListNodeBestNeighbourOfStates(block, pos);
            foreach (var node in listNodeOfPos)
            {
                Grid.Instance.SetNodeFromPos(node.coordinate, node);
                if (!this.CheckPathFound(node))
                    continue;
                if (node.listPosNeighbour.Count > maxNeighbour)
                {
                    listNode = new List<Node>();
                    maxNeighbour = node.listPosNeighbour.Count;
                    listNode.Add(node);
                }
                else if (node.listPosNeighbour.Count == maxNeighbour)
                {
                    if (this.CheckBlockPressingListNode(node.GetBlock(), listNode)) continue;
                    listNode.Add(node);
                }
                else
                    continue;
            }
        }
        return listNode;
    }

    private void CreateBlock(Node node)
    {
        List<Vector2Int> block = node.GetBlock();
        foreach (var pos in block)
        {
            D_Grid[pos].SetColor(node.block.Color);
            D_Grid[pos].hasValue = true;
        }
    }

    private void DeleteCurrentBlock(Node node)
    {
        List<Vector2Int> block = node.GetBlock();
        foreach (var pos in block)
        {
            D_Grid[pos].SetColor(ListColor.Instance.Null);
            D_Grid[pos].hasValue = false;
        }
    }

    private void RemoveSetButton(List<Square> squares)
    {
        foreach (var square in squares)
        {
            square.SetColor(ListColor.Instance.Null);
            square.button.onClick.RemoveAllListeners();
        }
    }

    private void SetButton(List<Node> nodes)
    {
        List<Square> squares = new List<Square>();
        foreach (var node in nodes)
        {
            foreach (var pos in node.GetBlock())
                squares.Add(D_Grid[pos]);
        }

        foreach (var node in nodes)
        {
            foreach (var pos in node.GetBlock())
            {
                D_Grid[pos].SetColor(ListColor.Instance.BestPos);
                List<System.Action> actions = new List<System.Action> {
                    () => StartCoroutine(this.MoveFollowPathFound(Grid.Instance.GetNodeFromPos(node.coordinate))),
                    () => this.RemoveSetButton(squares),
                };
                D_Grid[pos].SetListAction(actions);
            }
        }
    }

    private bool IsGameOver()
    {
        for (int i = 0; i < Size.x; i++)
            if (D_Grid[new Vector2Int(i, 0)].hasValue) return true;
        return false;
    }

    private void StopMoveFollowStep()
    {
        this.StopAllCoroutines();
        isMoving = false;
        Invoke(nameof(this.CheckLine), 1);
    }
    bool isMoving;
    private IEnumerator MoveFollowPathFound(Node node, int step = 0)
    {
        if (step >= node.path.Count)
            this.StopMoveFollowStep();
        yield return new WaitForSeconds(0.1f);
        if (node.path == null) yield return null;
        if (step < node.path.Count)
        {
            isMoving = true;
            if (step != 0)
                this.DeleteCurrentBlock(node.path[step - 1]);
            this.CreateBlock(node.path[step]);
            step++;
        }
        StartCoroutine(this.MoveFollowPathFound(node, step));
    }

    private Block GetRandomBlock()
    {
        return Blocks[Random.Range(0, Blocks.Count)];
    }

    List<Node> nodes;

    private bool HasLine(int iRow)
    {
        for (int i = 0; i < Size.x; i++)
            if (!D_Grid[new Vector2Int(i, iRow)].hasValue) return false;
        return true;
    }

    private void DeleteLine(int iRow)
    {
        for (int i = 0; i < Size.x; i++)
        {
            D_Grid[new Vector2Int(i, iRow)].SetColor(ListColor.Instance.Null);
            D_Grid[new Vector2Int(i, iRow)].hasValue = false;
        }
    }

    private void RowDown(int iRow)
    {
        for (int i = iRow; i > 0; i--)
        {
            for (int j = 0; j < Size.x; j++)
            {
                D_Grid[new Vector2Int(j, i)].SetColor(D_Grid[new Vector2Int(j, i - 1)].image.sprite);
                D_Grid[new Vector2Int(j, i)].hasValue = D_Grid[new Vector2Int(j, i - 1)].hasValue;
            }
        }
    }

    bool isChecking;
    bool isWait;

    private void CheckLine()
    {
        isChecking = true;
        int count = 0;
        for (int i = Size.y - 1; i > 0; i--)
        {
            if (this.HasLine(i))
            {
                count++;
                this.DeleteLine(i);
                Score.Instance.IncreaseScore(50);
                AudioSystem.Instance.PlaySoundOnce(ListSound.Instance.EatItem);
                this.RowDown(i);
                Invoke(nameof(this.CheckLine), 0.5f);
            }
        }
        if (count <= 0)
        {
            isWait = false;
            isChecking = false;
            if (this.IsGameOver())
            {
                GameController.Instance.GameOver();
                return;
            }
            this.OnPlay();
        }
    }

    private void OnPlay()
    {
        SpawnBlock.Instance.SpawnRandomNextBlock();
        Block block = SpawnBlock.Instance.BlockUsing;
        Grid.Instance.SetGrid(block);
        nodes = this.ListBestNode(block);
        if (nodes.Count <= 0 || nodes == null)
        {
            GameController.Instance.GameOver();
            return;
        }
        this.SetButton(nodes);
        isWait = true;
        Debug.Log(isWait);
    }

    public void StartGame()
    {
        this.SetListStartPos();
        //
        Block block = SpawnBlock.Instance.BlockUsing;
        Grid.Instance.SetGrid(block);
        nodes = this.ListBestNode(block);
        this.SetButton(nodes);
        isWait = true;
    }

    private void Awake()
    {
        Instance = this;
    }
}
