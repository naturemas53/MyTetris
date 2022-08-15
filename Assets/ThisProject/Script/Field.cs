using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    /// <summary>
    /// �v���C�G���A�̃T�C�Y�i���s�[�X��������͈́j
    /// </summary>
    public readonly Vector2Int PLAY_AREA_SIZE = new Vector2Int( 10, 20 );
    /// <summary>
    /// �㕔�̃o�b�t�@����
    /// </summary>
    public readonly int TOP_BUFFER_SPACE = 10;

    //---�ȉ��A�R���X�g���N�g���ɐݒ�---
    /// <summary>
    /// �o�b�t�@�⍶�E���̕ǂ��܂߂��S�̃T�C�Y�i�R���X�g���N�^���Ɋm��j
    /// </summary>
    public readonly Vector2Int SIZE;
    /// <summary>
    /// �v���C�G���A�̃��N�g�i�����ʒu�Q�Ɠ��ɂǂ����j
    /// </summary>
    public readonly RectInt PLAY_AREA_IDX_RECT;

    /// <summary>
    /// �t�B�[���h���̃u���b�N
    /// </summary>
    ABlock[] fieldBlocks = null;

    public Field()
    { 
        // �萔�l�@�Z��
        SIZE = CalcTotalSize();
        PLAY_AREA_IDX_RECT = CalcPlayAreaIdxRect();

        fieldBlocks = new ABlock[ SIZE.x * SIZE.y ];

        Initialize();
    }

    /// <summary>
    /// �t�B�[���h�̏��������s���܂�.
    /// </summary>
    public void Initialize()
    {
        Array.Clear( fieldBlocks, 0, fieldBlocks.Length );

        // TODO: �ǂƃX�y�[�X�z�u����
    }

    /// <summary>
    /// �ǂ�o�b�t�@�����܂߂��A�S�̂̃T�C�Y�����߂܂�
    /// </summary>
    /// <returns></returns>
    Vector2Int CalcTotalSize()
    {
        Vector2Int totalSize = PLAY_AREA_SIZE;
        // ���E�̕� ��.
        totalSize.x += 2;
        // ���̕ǁ@��.
        totalSize.y += 1;
        // �㕔�o�b�t�@ ��.
        totalSize.y += TOP_BUFFER_SPACE;

        return totalSize;
    }

    /// <summary>
    /// �v���C�G���A�̃��N�g��Ԃ��܂�.(���_�͍�����ۂ�)
    /// </summary>
    /// <returns></returns>
    RectInt CalcPlayAreaIdxRect()
    {
        RectInt playAreaIdxRect = new RectInt();

        playAreaIdxRect.size = PLAY_AREA_SIZE;
        playAreaIdxRect.x = 1; // ���̕ǂ̕����炵��
        playAreaIdxRect.y = TOP_BUFFER_SPACE; // �㕔�o�b�t�@�����炵��

        return playAreaIdxRect;
    }

}
