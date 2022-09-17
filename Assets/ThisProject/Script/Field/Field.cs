using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
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

        fieldBlocks[index] = setBlock;
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
    int ConvVector2IntToIndex( Vector2Int sourcePos )
    {
        return (sourcePos.y * SIZE.y) + sourcePos.x;
    }
}
