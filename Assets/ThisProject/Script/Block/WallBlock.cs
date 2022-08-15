using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock: ABlock
{
    public WallBlock() : base ( CommonDefines.EBlockType.BLOCK_WALL)
    {
        IsCanLineAlign = false;
        IsHaveColision = true;
    }
}
