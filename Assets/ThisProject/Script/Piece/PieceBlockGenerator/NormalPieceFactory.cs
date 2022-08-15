using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPieceFactory : APieceFactory
{
    /// <summary>
    /// 必要なブロックの数
    /// </summary>
    readonly int NEED_BLOCK_NUM = 4;

    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    Option optionValue = null;

    /// <summary>
    /// オプション設定
    /// </summary>
    /// <param name="applyOption"></param>
    public void SetOption(Option applyOption)
    {
       optionValue = applyOption;
    }

    public override Piece CreatePiece()
    {
        CommonDefines.PieceParam param = new CommonDefines.PieceParam();

        BlockFactory factory = BlockFactory.Instance;
        // 普通のピースなので、4つブロックを生成しておきます.
        for( int i = 0; i < NEED_BLOCK_NUM; ++i  )
        {
            NormalBlock block = factory.CreateBlock<NormalBlock>();
            param.blocks.Add( block );
        }

        return new Piece(param);
    }
}
