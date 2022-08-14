using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBlock : ABlock
{
    public SpaceBlock( Sprite image ) : base ( image, CommonDefines.EBlockType.BLOCK_EMPTY)
    {
        IsCanLineAlign = false;
        IsHaveColision = false;
    }

}
