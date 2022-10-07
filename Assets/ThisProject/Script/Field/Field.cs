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
    /// プレイエリアのサイズ（≒ピースが動ける範囲）
    /// </summary>
    public readonly Vector2Int PLAY_AREA_SIZE = new Vector2Int( 10, 20 );
    /// <summary>
    /// 上部のバッファ部分
    /// </summary>
    public readonly int TOP_BUFFER_SPACE = 10;

    //---以下、コンストラクト時に設定---
    /// <summary>
    /// バッファや左右等の壁も含めた全体サイズ（コンストラクタ時に確定）
    /// </summary>
    public readonly Vector2Int SIZE;
    /// <summary>
    /// プレイエリアのレクト（初期位置参照等にどうぞ）
    /// </summary>
    public readonly RectInt PLAY_AREA_IDX_RECT;
    //---以上、コンストラクト時に設定---

    /// <summary>
    /// フィールドブロックに変動があった際のイベント.
    /// </summary>
    public FieldBlockChangeEvent OnBlockChange { get; private set; } = new FieldBlockChangeEvent();

    /// <summary>
    /// フィールド内のブロック
    /// </summary>
    ABlock[] fieldBlocks = null;

    /// <summary>
    /// 空白ブロック(使いまわす予定）
    /// </summary>
    ABlock spaceBlock = null;
    /// <summary>
    /// 壁ブロック(使いまわす予定)
    /// </summary>
    ABlock wallBlock = null;

    /// <summary>
    /// ライン消去予約中の列
    /// </summary>
    public List<int> BookedAlignLineIdxs { get; private set; } = null;

    public Field()
    { 
        // 定数値　算定
        SIZE = CalcTotalSize();
        PLAY_AREA_IDX_RECT = CalcPlayAreaIdxRect();

        fieldBlocks = new ABlock[ SIZE.x * SIZE.y ];

        // 通常壁・空白は使いまわしても大丈夫だと思うので、ここで生成しちゃう.
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
    /// フィールドの初期化を行います.
    /// </summary>
    public void Initialize()
    {
        Array.Clear( fieldBlocks, 0, fieldBlocks.Length );

        // 壁・空白の設定
        bool isFloor = false; // 床位置？
        bool isWall = false;  // 壁位置？
        int xPos = 0; // 今のインデックスの横位置は？（全体で）
        bool isWallBlock = false; // 壁ブロックを入れるべき？
        for( int i = 0; i < fieldBlocks.Length; ++i )
        {
            // 床チェック(一番下）
            isFloor = i >= (SIZE.x * (SIZE.y - 1));
            // 壁チェック(左右)
            xPos = i % SIZE.x;
            isWall = ( xPos == 0 ) || ( xPos == SIZE.x - 1 );

            // 場所確認後、壁か空白を入れる.
            isWallBlock = ( isFloor || isWall);
            fieldBlocks[i] = (isWallBlock) ? wallBlock : spaceBlock;
        }
    }

    /// <summary>
    /// ブロックを置きます.
    /// </summary>
    public void SetBlock( ABlock setBlock, Vector2Int setPosition )
    {
        if (!IsValidPosToField(setPosition))
        {
            Debug.LogWarning( "フィールド範囲外が指定されました" );
            return;
        }

        int index = ConvVector2IntToIndex(setPosition);

        setBlock.OwnedSelf( this );
        fieldBlocks[index] = setBlock;

        // ラインを揃えられる可能性があるならチェックする
        if( setBlock.IsCanLineAlign )
        {
            CheckAlignLine( setPosition.y );
        }

        OnBlockChange.Invoke( setPosition );
    }

    /// <summary>
    /// 指定位置のブロックを取得します.
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
    /// ブロックを消去し、空白にします（EMPTYを余計に生成したくなかったので）
    /// </summary>
    public void RemoveBlock( Vector2Int position )
    {
        SetBlock( spaceBlock, position );
    }

    /// <summary>
    /// 壁やバッファ分を含めた、全体のサイズを求めます
    /// </summary>
    /// <returns></returns>
    Vector2Int CalcTotalSize()
    {
        Vector2Int totalSize = PLAY_AREA_SIZE;
        // 左右の壁 分.
        totalSize.x += 2;
        // 下の壁　分.
        totalSize.y += 1;
        // 上部バッファ 分.
        totalSize.y += TOP_BUFFER_SPACE;

        return totalSize;
    }

    /// <summary>
    /// プレイエリアの左上を返します.
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetLeftTopFromPlayArea()
    {
        // 左の壁 & 上部バッファ分　を避ける形
        return new Vector2Int( 1, TOP_BUFFER_SPACE );
    }

    /// <summary>
    /// プレイエリアのレクトを返します.(減点は左上っぽい)
    /// </summary>
    /// <returns></returns>
    RectInt CalcPlayAreaIdxRect()
    {
        RectInt playAreaIdxRect = new RectInt();

        playAreaIdxRect.size = PLAY_AREA_SIZE;
        playAreaIdxRect.x = 1; // 左の壁の分ずらしま
        playAreaIdxRect.y = TOP_BUFFER_SPACE; // 上部バッファ分ずらしま

        return playAreaIdxRect;
    }

    /// <summary>
    /// フィールド内を指している位置か
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
    /// ブロック位置を配列インデックスに変換します
    /// </summary>
    /// <returns></returns>
    public int ConvVector2IntToIndex( Vector2Int sourcePos )
    {
        return (sourcePos.y * SIZE.x) + sourcePos.x;
    }

    /// <summary>
    /// インデックスをブロック位置に変換します
    /// </summary>
    /// <param name="sourceIdx"></param>
    /// <returns></returns>
    public Vector2Int ConvIndexToVector2Int(int sourceIdx)
    {
        return new Vector2Int( (sourceIdx % SIZE.x), (sourceIdx / SIZE.x) );
    }

    /// <summary>
    /// ブロックが1列揃っているか
    /// </summary>
    void CheckAlignLine(int checkLineIdx)
    {
        bool alignLine = true;

        for( int i = 0; i < SIZE.x; ++i )
        {
            ABlock block = GetBlock( new Vector2Int( i, checkLineIdx ) );

            // 壁は　壁だしなぁ...（？）
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
    /// ラインが揃った時のアクションを実行します
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
    /// 消去ラインをチェックして、消えたところに別なラインを詰めます
    /// </summary>
    public void PackField()
    {
        int blockExistTopLine = -1;
        // 一番下は床なのでチェックしない
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
            // 全消しされたか、奇跡的にない
            return;
        }

        int eraceLineCount = 0;
        // SIZE.y -1 は床なので、その1ライン上からチェック開始
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
    /// ラインの中にブロックが存在するか
    /// </summary>
    bool IsExistBlockFromLine( int lineIdx )
    {
        bool existBlock = false;

        for (int i = 0; i < SIZE.x; ++i)
        {
            ABlock block = GetBlock(new Vector2Int(i, lineIdx));

            if ( block == null )
            {
                // 本来はありえないけど、一応
                // ちなみにないものとして扱います
                continue;
            }

            // 壁は　壁だしなぁ...（？）
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
    /// ラインの中身を指定ラインにコピーします
    /// </summary>
    void CopyLine( int srcLineIdx, int destLineIdx )
    {
        for (int i = 0; i < SIZE.x; ++i)
        {
            SetBlock(GetBlock(new Vector2Int(i, srcLineIdx)), new Vector2Int(i, destLineIdx));
        }
    }

    /// <summary>
    /// 指定ラインの中身を空にします
    /// （※ラインが”揃った時”の処理には使用しないで下さい！）
    /// （この処理はあくまでも、「ラインを消去して詰める」時用のものとなります。）
    /// </summary>
    void ClearLine( int lineIdx )
    {
        for (int i = 0; i < SIZE.x; ++i)
        {
            // 両側の壁は壊さないように...
            if( i <= 0 || i >= (SIZE.x - 1) )
            {
                continue;
            }

            RemoveBlock( new Vector2Int(i, lineIdx) );
        }
    }
}
