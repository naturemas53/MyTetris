using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPieceBlockGenerator : APieceBlockGenerator
{
    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    Option optionValue = null;

    /// <summary>
    /// ÉIÉvÉVÉáÉìê›íË
    /// </summary>
    /// <param name="applyOption"></param>
    public void SetOption(Option applyOption)
    {
       optionValue = applyOption;
    }

    public override bool GenerateBlocks(ref List<ABlock> sendGeneratedBlocks)
    {
        sendGeneratedBlocks.Clear();

        return true;
    }
}
