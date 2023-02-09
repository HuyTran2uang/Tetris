using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public static SpawnBlock Instance { get; private set; }
    private List<Block> _blocks = new List<Block>()
    {
        new TBlock(),
        new IBlock(),
        new OBlock(),
        new JBlock(),
        new LBlock(),
        new ZBlock(),
        new SBlock()
    };

    public Block BlockUsing { get; private set; }
    List<Block> blocksPrepare = new List<Block>();

    public Block SpawnRandomBlock()
    {
        return _blocks[Random.Range(0, _blocks.Count)];
    }

    public void SpawnRandomNextBlock()
    {
        BlockUsing = blocksPrepare[0];
        UISetBlock.Instance.SetBlockUsing(BlockUsing);
        blocksPrepare.RemoveAt(0);
        blocksPrepare.Add(this.SpawnRandomBlock());
        UISetBlock.Instance.SetBlocksPrepare(blocksPrepare);
    }

    public void StartGame()
    {
        BlockUsing = this.SpawnRandomBlock();
        UISetBlock.Instance.SetBlockUsing(BlockUsing);
        //random 3 block to prepare
        blocksPrepare = new List<Block>();
        blocksPrepare.Add(this.SpawnRandomBlock());
        blocksPrepare.Add(this.SpawnRandomBlock());
        blocksPrepare.Add(this.SpawnRandomBlock());
        UISetBlock.Instance.SetBlocksPrepare(blocksPrepare);
    }

    private void Awake()
    {
        Instance = this;
    }
}
