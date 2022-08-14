using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPieceBlockGenerator : APieceBlockGenerator< NormalPieceBlockGenerator.Option >
{
    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    public override void SetOption(Option applyOption)
    {
        this.option = applyOption;
    }

    public override bool GenerateBlocks(ref List<ABlock> sendGeneratedBlocks)
    {
        sendGeneratedBlocks.Clear();

        return true;
    }
}
