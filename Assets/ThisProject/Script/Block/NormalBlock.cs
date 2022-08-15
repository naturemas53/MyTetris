using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : ABlock
{
    public class Option
    {
        public Field ownerField;
    }

    Option optionValue = null;

    public NormalBlock() : base (CommonDefines.EBlockType.BLOCK_NORMAL)
    {
        IsCanLineAlign = true;
        IsHaveColision = true;
    }

    public void SetOption( Option setOption )
    {
        optionValue = setOption;
    }

    public override void ExecuteLineAligned()
    {
        // TODO:消去時の処理
        base.ExecuteLineAligned();
    }

}
