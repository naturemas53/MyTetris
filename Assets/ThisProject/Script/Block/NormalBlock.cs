using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : ABlock
{
    public NormalBlock( Sprite image ) : base ( image, CommonDefines.EBlockType.BLOCK_NORMAL)
    {
        IsCanLineAlign = true;
        IsHaveColision = true;
    }

    public override void Erace()
    {
        // TODO:消去時の処理
    }

}
