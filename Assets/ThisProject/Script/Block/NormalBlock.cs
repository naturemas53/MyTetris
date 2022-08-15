using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : ABlock
{
    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    Option optionValue = null;

    CommonDefines.EShapeType ShapeType 
    {
        get
        {
            if( optionValue == null ) return CommonDefines.EShapeType.SHAPE_NONE;

            return optionValue.shapeType;
        }
    }

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
    }

}
