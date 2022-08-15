using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBlock : ABlock
{
    public SpaceBlock() : base ( CommonDefines.EBlockType.BLOCK_EMPTY)
    {
        IsCanLineAlign = false;
        IsHaveColision = false;
    }

}
