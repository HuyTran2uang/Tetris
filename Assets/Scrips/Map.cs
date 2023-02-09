using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance { get; private set; }
    [SerializeField] private Vector2Int _size;//(width, height)
    [SerializeField] private GameObject _squarePrefab;
    [SerializeField] private Dictionary<Vector2Int, Square> _grid;
    [SerializeField] private List<Square> _squares;
    [SerializeField] float _time;
    float _waitTime = 0.1f;
    List<Block> _blocks = new List<Block>()
    {
        // new TBlock(),
        new IBlock(),
        new OBlock(),
        // new JBlock(),
        // new LBlock(),
        // new ZBlock(),
        // new SBlock()
    };

    public Vector2Int Size => _size;
    public Dictionary<Vector2Int, Square> Grid => _grid;
    public List<Square> Squares => _squares;
    public List<Block> Blocks => _blocks;

    private void DrawMap()
    {
        _grid = new Dictionary<Vector2Int, Square>();
        _squares = new List<Square>();
        for (int i = 0; i < _size.y; i++)
        {
            for (int j = 0; j < _size.x; j++)
            {
                GameObject obj = Instantiate(_squarePrefab, transform);
                Square square = obj.GetComponent<Square>();
                _squares.Add(square);
                _grid.Add(new Vector2Int(j, i), square);
            }
        }
    }

    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    Block _block;
    Vector2Int _spawnPos = new Vector2Int(3, 0);
    Vector2Int _currentPos;
    bool _isActing;

    private Block GetRandomBlock()
    {
        return _blocks[Random.Range(0, _blocks.Count)];
    }

    private bool CheckBlockValid(List<Vector2Int> poses)
    {
        foreach (var pos in poses)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= _size.x || pos.y >= _size.y) return false;
            if (_grid[pos].hasValue) return false;
        }
        return true;
    }

    private void CreateBlock(Block block, Vector2Int curPos)
    {
        // List<Vector2Int> poses = block.GetBlockFromPos(curPos);
        // foreach (var pos in poses)
        // {
        //     _grid[pos].SetColor(block.Color);
        //     _grid[pos].hasValue = true;
        // }
        Debug.Log("create");
    }

    private void DeleteCurrentBlock(Block block, Vector2Int curPos)
    {
        // List<Vector2Int> poses = block.GetBlockFromPos(curPos);
        // foreach (var pos in poses)
        // {
        //     _grid[pos].SetColor(ListColor.Instance.Null);
        //     _grid[pos].hasValue = false;
        // }
        Debug.Log("delete");
    }

    private void SetBlockInMap(Block block, Vector2Int curPos)
    {
        // List<Vector2Int> poses = block.GetBlockFromPos(curPos);
        // foreach (var pos in poses)
        // {
        //     _grid[pos].SetColor(block.Color);
        //     _grid[pos].hasValue = true;
        // }
        Debug.Log("Set");
    }

    private void SpawnBlock()
    {
        _currentPos = _spawnPos;
        _block = this.GetRandomBlock();
        _canSpawn = false;
        _spawned = true;
        Debug.Log("spawn");
    }

    private void BlockMoveDown(Block block, Vector2Int pos)
    {
        this.DeleteCurrentBlock(block, pos);
        this.CreateBlock(block, pos += Vector2Int.up);
    }

    private void BlockMoveLeft(Block block, Vector2Int pos)
    {
        this.DeleteCurrentBlock(block, pos);
        this.CreateBlock(block, pos += Vector2Int.left);
    }

    private void BlockMoveRight(Block block, Vector2Int pos)
    {
        this.DeleteCurrentBlock(block, pos);
        this.CreateBlock(block, pos += Vector2Int.right);
    }

    private List<Vector2Int> GetBlockBestDownLeft(Block block, Vector2Int pos)
    {
        List<Vector2Int> listStepMove = new List<Vector2Int>();
        //move down
        // if ((pos + Vector2Int.up).x >= 0 && (pos + Vector2Int.up).y >= 0 && this.CheckBlockValid(block.GetBlockFromPos(pos + Vector2Int.up)))
        // {
        //     pos += Vector2Int.up;
        //     listStepMove.Add(pos);
        //     List<Vector2Int> steps = this.GetBlockBestDownLeft(block, pos);
        //     foreach (var step in steps)
        //         listStepMove.Add(step);
        // }
        //move left
        // else if ((pos + Vector2Int.left).x >= 0 && (pos + Vector2Int.left).y >= 0 && this.CheckBlockValid(block.GetBlockFromPos(pos + Vector2Int.left)))
        // {
        //     pos += Vector2Int.left;
        //     listStepMove.Add(pos);
        //     List<Vector2Int> steps = this.GetBlockBestDownLeft(block, pos);
        //     foreach (var step in steps)
        //         listStepMove.Add(step);
        // }
        return listStepMove;
    }

    private List<Vector2Int> GetBlockBestDownRight(Block block, Vector2Int pos)
    {
        List<Vector2Int> listStepMove = new List<Vector2Int>();
        //move down
        // if ((pos + Vector2Int.up).x >= 0 && (pos + Vector2Int.up).y >= 0 && this.CheckBlockValid(block.GetBlockFromPos(pos + Vector2Int.up)))
        // {
        //     pos += Vector2Int.up;
        //     listStepMove.Add(pos);
        //     List<Vector2Int> steps = this.GetBlockBestDownRight(block, pos);
        //     foreach (var step in steps)
        //         listStepMove.Add(step);
        // }
        // //move right
        // else if ((pos + Vector2Int.right).x >= 0 && (pos + Vector2Int.right).y >= 0 && this.CheckBlockValid(block.GetBlockFromPos(pos + Vector2Int.right)))
        // {
        //     pos += Vector2Int.right;
        //     listStepMove.Add(pos);
        //     List<Vector2Int> steps = this.GetBlockBestDownRight(block, pos);
        //     foreach (var step in steps)
        //         listStepMove.Add(step);
        // }
        return listStepMove;
    }

    private IEnumerator MoveNextStep(Block block, List<Vector2Int> steps, int step)
    {
        yield return new WaitForSeconds(_waitTime);
        if (step < steps.Count)
        {
            if (step != 0)
                this.DeleteCurrentBlock(block, steps[step - 1]);
            this.CreateBlock(block, steps[step]);
            step++;
            StartCoroutine(this.MoveNextStep(block, steps, step));
        }
        else
        {
            this.StopMove();
        }
        yield return null;
    }

    private void MoveFollowSteps(Block block, List<Vector2Int> steps)
    {
        foreach (var i in _grid)
            i.Value.button.onClick.RemoveAllListeners();
        StartCoroutine(this.MoveNextStep(block, steps, 0));
    }

    private void Move()
    {
        _isMoving = true;
    }

    private void StopMove()
    {
        _isMoving = false;
        _canSpawn = true;
    }

    private void RemoveSetupSquare(List<Square> squares)
    {
        foreach (var square in squares)
        {
            square.button.onClick.RemoveAllListeners();
            square.SetColor(ListColor.Instance.Null);
        }
    }

    private void SetOptionBestPosition()
    {
        // //left
        // List<Vector2Int> steps1 = this.GetBlockBestDownLeft(_block, _spawnPos);
        // List<Vector2Int> listPos1 = _block.GetBlockFromPos(steps1[steps1.Count - 1]);
        // List<Square> squares = new List<Square>();
        // foreach (var pos in listPos1)
        //     if (!squares.Contains(_grid[pos]))
        //         squares.Add(_grid[pos]);
        // foreach (var pos in listPos1)
        // {
        //     _grid[pos].SetColor(ListColor.Instance.BestPos);
        //     List<System.Action> actions1 = new List<System.Action>();
        //     actions1.Add(() => this.MoveFollowSteps(_block, this.GetBlockBestDownLeft(_block, _spawnPos)));
        //     actions1.Add(() => this.Move());
        //     actions1.Add(() => this.RemoveSetupSquare(squares));
        //     _grid[pos].SetListAction(actions1);
        // }
        // //right
        // List<Vector2Int> steps2 = this.GetBlockBestDownRight(_block, _spawnPos);
        // List<Vector2Int> listPos2 = _block.GetBlockFromPos(steps2[steps2.Count - 1]);
        // List<Square> squares2 = new List<Square>();
        // foreach (var pos in listPos2)
        //     if (!squares.Contains(_grid[pos]))
        //         squares.Add(_grid[pos]);
        // foreach (var pos in listPos2)
        // {
        //     _grid[pos].SetColor(ListColor.Instance.BestPos);
        //     List<System.Action> actions2 = new List<System.Action>();
        //     actions2.Add(() => this.MoveFollowSteps(_block, this.GetBlockBestDownRight(_block, _spawnPos)));
        //     actions2.Add(() => this.Move());
        //     actions2.Add(() => this.RemoveSetupSquare(squares));
        //     _grid[pos].SetListAction(actions2);
        // }
    }

    bool _canSpawn;
    bool _spawned;
    bool _isMoving;
    private void Update()
    {
        //if full column is game over
        //spawn block
        if (Input.GetKeyDown(KeyCode.Space) && _canSpawn)
        {
            this.SpawnBlock();
        }
        //find best position
        // this.GetBlockBestDownLeft(_block, _spawnPos);
        if (!_isMoving && _spawned)
        {
            this.SetOptionBestPosition();
            _spawned = false;
        }
    }

    private void FixedUpdate()
    {
        if (_time > 0) _time -= Time.deltaTime;
    }

    private void Reset()
    {
        _size = new Vector2Int(7, 16);
        _squarePrefab = Resources.Load<GameObject>("UI/Square");
        this.DrawMap();
    }

    public void StartGame()
    {
        _size = new Vector2Int(7, 16);
        _squarePrefab = Resources.Load<GameObject>("UI/Square");
        this.DestroyAllChildren();
        this.DrawMap();
        _canSpawn = true;
    }

    private void Awake()
    {
        Instance = this;
    }
}
