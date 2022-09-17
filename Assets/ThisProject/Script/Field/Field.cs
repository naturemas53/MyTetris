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

    /// <summary>
    /// �󔒃u���b�N(�g���܂킷�\��j
    /// </summary>
    ABlock spaceBlock = null;
    /// <summary>
    /// �ǃu���b�N(�g���܂킷�\��)
    /// </summary>
    ABlock wallBlock = null;

    public Field()
    { 
        // �萔�l�@�Z��
        SIZE = CalcTotalSize();
        PLAY_AREA_IDX_RECT = CalcPlayAreaIdxRect();

        fieldBlocks = new ABlock[ SIZE.x * SIZE.y ];

        // �ʏ�ǁE�󔒂͎g���܂킵�Ă����v���Ǝv���̂ŁA�����Ő��������Ⴄ.
        BlockFactory blockFactory = BlockFactory.Instance;
        spaceBlock = blockFactory.CreateBlock( CommonDefines.EBlockType.BLOCK_EMPTY );
        spaceBlock.OwnedSelf( this );
        wallBlock  = blockFactory.CreateBlock( CommonDefines.EBlockType.BLOCK_WALL );
        wallBlock .OwnedSelf( this );

        Initialize();
    }

    /// <summary>
    /// �t�B�[���h�̏��������s���܂�.
    /// </summary>
    public void Initialize()
    {
        Array.Clear( fieldBlocks, 0, fieldBlocks.Length );

        // �ǁE�󔒂̐ݒ�
        bool isFloor = false; // ���ʒu�H
        bool isWall = false;  // �ǈʒu�H
        int xPos = 0; // ���̃C���f�b�N�X�̉��ʒu�́H�i�S�̂Łj
        bool isWallBlock = false; // �ǃu���b�N������ׂ��H
        for( int i = 0; i < fieldBlocks.Length; ++i )
        {
            // ���`�F�b�N(��ԉ��j
            isFloor = i >= (SIZE.x * (SIZE.y - 1));
            // �ǃ`�F�b�N(���E)
            xPos = i % SIZE.x;
            isWall = ( xPos == 0 ) || ( xPos == SIZE.x - 1 );

            // �ꏊ�m�F��A�ǂ��󔒂�����.
            isWallBlock = ( isFloor || isWall);
            fieldBlocks[i] = (isWallBlock) ? wallBlock : spaceBlock;
        }
    }

    /// <summary>
    /// �u���b�N��u���܂�.
    /// </summary>
    public void SetBlock( ABlock setBlock, Vector2Int setPosition )
    {
        if (!IsValidPosToField(setPosition))
        {
            Debug.LogWarning( "�t�B�[���h�͈͊O���w�肳��܂���" );
            return;
        }

        int index = ConvVector2IntToIndex(setPosition);

        fieldBlocks[index] = setBlock;
    }

    /// <summary>
    /// �w��ʒu�̃u���b�N���擾���܂�.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public ABlock GetBlock( Vector2Int position )
    {
        ABlock retBlock = null;
        if (!IsValidPosToField(position)) return retBlock;

        int index = ConvVector2IntToIndex( position );

        return fieldBlocks[ index ];
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

    /// <summary>
    /// �t�B�[���h�����w���Ă���ʒu��
    /// </summary>
    /// <returns></returns>
    bool IsValidPosToField( Vector2Int checkPos )
    {
        if( checkPos.x >= SIZE.x ) return false;
        if( checkPos.y >= SIZE.y ) return false;
        if( checkPos.x < 0 ) return false;
        if( checkPos.y < 0 ) return false;

        return true;
    }

    /// <summary>
    /// �u���b�N�ʒu��z��C���f�b�N�X�ɕϊ����܂�
    /// </summary>
    /// <returns></returns>
    int ConvVector2IntToIndex( Vector2Int sourcePos )
    {
        return (sourcePos.y * SIZE.y) + sourcePos.x;
    }
}
