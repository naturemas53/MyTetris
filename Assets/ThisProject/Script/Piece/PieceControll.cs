using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControll
{
    /// <summary>
    /// 所持ピース
    /// </summary>
    Piece havePiece = null;
    
    /// <summary>
    /// ピース位置
    /// </summary>
    Vector2Int piecePos = Vector2Int.zero;

    /// <summary>
    /// ロックダウン時間などの現状のオプション
    /// </summary>
    public CommonDefines.PieceControllerOption CurrentOption { get; private set; }
    /// <summary>
    /// ロックダウン時間など　の各数値の「残り値」（めんどくさかったので使いまわし）
    /// </summary>
    CommonDefines.PieceControllerOption optionRemain;

    public PieceControll( Piece firstPiece, CommonDefines.PieceControllerOption firstOption )
    {
        havePiece = firstPiece;
        CurrentOption = firstOption;
        InitParams();
    }

    /// <summary>
    /// インフィニティ等のパラメータをリセットします
    /// </summary>
    void InitParams()
    {
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="deltaTime">経過時間</param>
    public void Update( float deltaTime )
    {
        optionRemain.lockDownTime -= deltaTime;
    }

    /// <summary>
    /// ピース移動を行います(失敗の可能性アリ）
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>移動に　成功：True 失敗：False</returns>
    public bool TryMove( Vector2Int moveDirection )
    {
        return true;
    }

    /// <summary>
    /// 回転を行います(失敗の可能性アリ）
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>回転に　成功：True 失敗：False</returns>
    public bool TryRotate(int rotateDirection)
    {
        return true;
    }
}
