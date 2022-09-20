using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class NormalPieceFactory : APieceFactory
{
    /// <summary>
    /// 必要なブロックの数
    /// </summary>
    readonly int NEED_BLOCK_NUM = 4;

    Dictionary<EShapeType, PieceDataColumns> pieceParamMap = null;

    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    Option optionValue = null;

    public NormalPieceFactory()
    {
        pieceParamMap = new Dictionary<EShapeType, PieceDataColumns>();
        SetParamDataMap();
    }

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

        param.pieceDatas = pieceParamMap[ optionValue.shapeType ];

        BlockFactory factory = BlockFactory.Instance;
        // 普通のピースなので、4つブロックを生成しておきます.
        for( int i = 0; i < NEED_BLOCK_NUM; ++i  )
        {
            NormalBlock block = factory.CreateBlock<NormalBlock>();
            
            NormalBlock.Option option = new NormalBlock.Option();
            option.shapeType = optionValue.shapeType;
            block.SetOption( option );

            param.blocks.Add( block );
        }

        return new Piece(param);
    }

    /// <summary>
    /// ピースパラメータを取得します
    /// </summary>
    void SetParamDataMap(  )
    {
        string basePath = FILE_INPUT_ENTRY + "PieceParam/" + "NORMAL_{0}.json";

        for( EShapeType shape = EShapeType.SHAPE_Z; shape < EShapeType.SHAPE_SPECIAL; ++shape )
        {
            string jsonPath = string.Format( basePath, shape.ToString() );
            PieceDataColumns pieceData = PieceDataColumsIO.Import( jsonPath );

            pieceParamMap.Add( shape, pieceData );
        }
    }
}
