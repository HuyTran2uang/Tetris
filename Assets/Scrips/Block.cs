using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    R0,
    R90,
    R180,
    RN90
}

public abstract class Block
{
    public virtual string Name { get; }
    public virtual Sprite Color { get; }
    public virtual List<BlockState> States { get; }
    public virtual Dictionary<BlockState, List<Vector2Int>> StatesFromPos(Vector2Int pos)
    {
        Dictionary<BlockState, List<Vector2Int>> states = new Dictionary<BlockState, List<Vector2Int>>();
        if (this.State1FromPos(pos) != null)
            states.Add(BlockState.R0, this.State1FromPos(pos));
        if (this.State2FromPos(pos) != null)
            states.Add(BlockState.R90, this.State2FromPos(pos));
        if (this.State3FromPos(pos) != null)
            states.Add(BlockState.R180, this.State3FromPos(pos));
        if (this.State4FromPos(pos) != null)
            states.Add(BlockState.RN90, this.State4FromPos(pos));
        return states;
    }
    protected abstract List<Vector2Int> State1FromPos(Vector2Int pos);
    protected abstract List<Vector2Int> State2FromPos(Vector2Int pos);
    protected abstract List<Vector2Int> State3FromPos(Vector2Int pos);
    protected abstract List<Vector2Int> State4FromPos(Vector2Int pos);
}

public class TBlock : Block
{
    public override string Name => "TBlock";
    public override Sprite Color => ListColor.Instance.Red;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90, BlockState.R180, BlockState.RN90 };
    /*
        0 0 0
          0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0
        0 0
        0
    */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
          0
        0 0 0
    */
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y - 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
          0
        0 0
          0
    */
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x - 1, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
}

public class IBlock : Block
{
    public override string Name => "IBlock";
    public override Sprite Color => ListColor.Instance.Green;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90 };
    /*
        0
        0
        0
        0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 2);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /* 0 0 0 0 */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x + 2, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    //
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        return null;
    }
    //
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        return null;
    }
}

public class OBlock : Block
{
    public override string Name => "OBlock";
    public override Sprite Color => ListColor.Instance.Yellow;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0 };
    /*
        0 0
        0 0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    //
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        return null;
    }
    //
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        return null;
    }
    //
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        return null;
    }
}

public class JBlock : Block
{
    public override string Name => "JBlock";
    public override Sprite Color => ListColor.Instance.Navy;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90, BlockState.R180, BlockState.RN90 };
    /*
          0
          0
        0 0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y - 2);
        Vector2Int pos4 = new Vector2Int(pos.x - 1, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0
        0 0 0
    */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x + 2, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0 0
        0
        0
    */
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 2);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0 0 0
            0
    */
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x - 2, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
}

public class LBlock : Block
{
    public override string Name => "LBlock";
    public override Sprite Color => ListColor.Instance.Pink;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90, BlockState.R180, BlockState.RN90 };
    /*
        0
        0
        0 0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y - 2);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
            0
        0 0 0
    */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x - 2, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y - 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0 0
          0
          0
    */
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x, pos.y + 2);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0 0 0
        0
    */
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x - 1, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
}

public class ZBlock : Block
{
    public override string Name => "ZBlock";
    public override Sprite Color => ListColor.Instance.Blue;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90 };
    /*
        0 0
          0 0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x - 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
          0
        0 0
        0
    */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y - 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    //
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        return null;
    }
    //
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        return null;
    }
}

public class SBlock : Block
{
    public override string Name => "SBlock";
    public override Sprite Color => ListColor.Instance.Orange;
    public override List<BlockState> States => new List<BlockState>() { BlockState.R0, BlockState.R90 };
    /*
          0 0
        0 0
    */
    protected override List<Vector2Int> State1FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos3 = new Vector2Int(pos.x, pos.y + 1);
        Vector2Int pos4 = new Vector2Int(pos.x - 1, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    /*
        0
        0 0
          0
    */
    protected override List<Vector2Int> State2FromPos(Vector2Int pos)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();

        Vector2Int pos1 = pos;
        Vector2Int pos2 = new Vector2Int(pos.x, pos.y - 1);
        Vector2Int pos3 = new Vector2Int(pos.x + 1, pos.y);
        Vector2Int pos4 = new Vector2Int(pos.x + 1, pos.y + 1);

        listPos.Add(pos1);
        listPos.Add(pos2);
        listPos.Add(pos3);
        listPos.Add(pos4);
        return listPos;
    }
    //
    protected override List<Vector2Int> State3FromPos(Vector2Int pos)
    {
        return null;
    }
    //
    protected override List<Vector2Int> State4FromPos(Vector2Int pos)
    {
        return null;
    }
}
