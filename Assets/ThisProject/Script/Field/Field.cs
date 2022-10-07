using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Field
{
    public class FieldBlockChangeEvent : UnityEvent<Vector2Int>
    {

    }

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
    //---�ȏ�A�R���X�g���N�g���ɐݒ�---

    /// <summary>
    /// �t�B�[���h�u���b�N�ɕϓ����������ۂ̃C�x���g.
    /// </summary>
    public FieldBlockChangeEvent OnBlockChange { get; private set; } = new FieldBlockChangeEvent();

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

    /// <summary>
    /// ���C�������\�񒆂̗�
    /// </summary>
    public List<int> BookedAlignLineIdxs { get; private set; } = null;

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

        BookedAlignLineIdxs = new List<int>();
        BookedAlignLineIdxs.Clear();

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

        setBlock.OwnedSelf( this );
        fieldBlocks[index] = setBlock;

        // ���C���𑵂�����\��������Ȃ�`�F�b�N����
        if( setBlock.IsCanLineAlign )
        {
            CheckAlignLine( setPosition.y );
        }

        OnBlockChange.Invoke( setPosition );
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
    /// �u���b�N���������A�󔒂ɂ��܂��iEMPTY��]�v�ɐ����������Ȃ������̂Łj
    /// </summary>
    public void RemoveBlock( Vector2Int position )
    {
        SetBlock( spaceBlock, position );
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
    /// �v���C�G���A�̍����Ԃ��܂�.
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetLeftTopFromPlayArea()
    {
        // ���̕� & �㕔�o�b�t�@���@�������`
        return new Vector2Int( 1, TOP_BUFFER_SPACE );
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
    public int ConvVector2IntToIndex( Vector2Int sourcePos )
    {
        return (sourcePos.y * SIZE.x) + sourcePos.x;
    }

    /// <summary>
    /// �C���f�b�N�X���u���b�N�ʒu�ɕϊ����܂�
    /// </summary>
    /// <param name="sourceIdx"></param>
    /// <returns></returns>
    public Vector2Int ConvIndexToVector2Int(int sourceIdx)
    {
        return new Vector2Int( (sourceIdx % SIZE.x), (sourceIdx / SIZE.x) );
    }

    /// <summary>
    /// �u���b�N��1�񑵂��Ă��邩
    /// </summary>
    void CheckAlignLine(int checkLineIdx)
    {
        bool alignLine = true;

        for( int i = 0; i < SIZE.x; ++i )
        {
            ABlock block = GetBlock( new Vector2Int( i, checkLineIdx ) );

            // �ǂ́@�ǂ����Ȃ�...�i�H�j
            if( block.BLOCK_TYPE == CommonDefines.EBlockType.BLOCK_WALL )
            {
                continue;
            }

            if ( !block.IsCanLineAlign )
            {
                alignLine = false;
                break;
            }
        }

        if ( alignLine )
        {
            if (!BookedAlignLineIdxs.Contains(checkLineIdx))
            {
                BookedAlignLineIdxs.Add(checkLineIdx);
            }
        }
        else
        {
            if (BookedAlignLineIdxs.Contains(checkLineIdx))
            {
                BookedAlignLineIdxs.Remove(checkLineIdx);
            }
        }
    }

    /// <summary>
    /// ���C�������������̃A�N�V���������s���܂�
    /// </summary>
    public void ExecuteAlignLine()
    {
        if(BookedAlignLineIdxs.Count <= 0 )
        {
            return;
        }

        List<int> executeLineIdxs = new List<int>( BookedAlignLineIdxs );
        BookedAlignLineIdxs.Clear();

        for( int i = 0; i < executeLineIdxs.Count; ++i )
        {
            for( int j = 0; j < SIZE.x; ++j )
            {
                Vector2Int blockPos = new Vector2Int(j, executeLineIdxs[i]);
                ABlock block = GetBlock(blockPos);

                block.ExecuteLineAligned( blockPos );
            }
        }
    }

    /// <summary>
    /// �������C�����`�F�b�N���āA�������Ƃ���ɕʂȃ��C�����l�߂܂�
    /// </summary>
    public void PackField()
    {
        int blockExistTopLine = -1;
        // ��ԉ��͏��Ȃ̂Ń`�F�b�N���Ȃ�
        for( int i = 0; i < (SIZE.y - 1); ++i )
        {
            if( IsExistBlockFromLine( i ) )
            {
                blockExistTopLine = i;
                break;
            }
        }

        if( blockExistTopLine < 0 )
        {
            // �S�������ꂽ���A��ՓI�ɂȂ�
            return;
        }

        int eraceLineCount = 0;
        // SIZE.y -1 �͏��Ȃ̂ŁA����1���C���ォ��`�F�b�N�J�n
        for ( int i = SIZE.y - 2; i >= blockExistTopLine; --i )
        {
            if( IsExistBlockFromLine( i ) )
            {
                if( eraceLineCount > 0 )
                {
                    CopyLine( i, i + eraceLineCount );
                    ClearLine( i );
                }
            }
            else
            {
                ++eraceLineCount;
            }
        }
    }

    /// <summary>
    /// ���C���̒��Ƀu���b�N�����݂��邩
    /// </summary>
    bool IsExistBlockFromLine( int lineIdx )
    {
        bool existBlock = false;

        for (int i = 0; i < SIZE.x; ++i)
        {
            ABlock block = GetBlock(new Vector2Int(i, lineIdx));

            if ( block == null )
            {
                // �{���͂��肦�Ȃ����ǁA�ꉞ
                // ���Ȃ݂ɂȂ����̂Ƃ��Ĉ����܂�
                continue;
            }

            // �ǂ́@�ǂ����Ȃ�...�i�H�j
            if (block.BLOCK_TYPE == CommonDefines.EBlockType.BLOCK_WALL)
            {
                continue;
            }

            if (block.BLOCK_TYPE != CommonDefines.EBlockType.BLOCK_EMPTY)
            {
                existBlock = true;
                break;
            }
        }

        return existBlock;
    }

    /// <summary>
    /// ���C���̒��g���w�胉�C���ɃR�s�[���܂�
    /// </summary>
    void CopyLine( int srcLineIdx, int destLineIdx )
    {
        for (int i = 0; i < SIZE.x; ++i)
        {
            SetBlock(GetBlock(new Vector2Int(i, srcLineIdx)), new Vector2Int(i, destLineIdx));
        }
    }

    /// <summary>
    /// �w�胉�C���̒��g����ɂ��܂�
    /// �i�����C�����h���������h�̏����ɂ͎g�p���Ȃ��ŉ������I�j
    /// �i���̏����͂����܂ł��A�u���C�����������ċl�߂�v���p�̂��̂ƂȂ�܂��B�j
    /// </summary>
    void ClearLine( int lineIdx )
    {
        for (int i = 0; i < SIZE.x; ++i)
        {
            // �����̕ǂ͉󂳂Ȃ��悤��...
            if( i <= 0 || i >= (SIZE.x - 1) )
            {
                continue;
            }

            RemoveBlock( new Vector2Int(i, lineIdx) );
        }
    }
}
