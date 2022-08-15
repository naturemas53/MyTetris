using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPieceFactory : APieceFactory
{
    /// <summary>
    /// �K�v�ȃu���b�N�̐�
    /// </summary>
    readonly int NEED_BLOCK_NUM = 4;

    public class Option
    {
        public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
    }

    Option optionValue = null;

    /// <summary>
    /// �I�v�V�����ݒ�
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
        // ���ʂ̃s�[�X�Ȃ̂ŁA4�u���b�N�𐶐����Ă����܂�.
        for( int i = 0; i < NEED_BLOCK_NUM; ++i  )
        {
            NormalBlock block = factory.CreateBlock<NormalBlock>();
            param.blocks.Add( block );
        }

        return new Piece(param);
    }
}
