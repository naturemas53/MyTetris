using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CommonDefines;

public class PieceControll
{
    /// <summary>
    /// 所持ピース
    /// </summary>
    public Piece HavePiece { get; private set; } = null;
    /// <summary>
    /// ピース位置
    /// </summary>
    public Vector2Int PiecePos { get; private set; } = Vector2Int.zero;
    /// <summary>
    /// 接着済みか
    /// </summary>
    public bool IsLocked
    {
        get
        {
            return optionRemain.lockDownTime <= 0.0f; 
        }
    }

    /// <summary>
    /// ピース変更時のコールバック
    /// </summary>
    public UnityEvent OnChangePiece { get; private set; } = new UnityEvent();

    /// <summary>
    /// ロックダウン時間などの現状のオプション
    /// </summary>
    public CommonDefines.PieceControllerOption CurrentOption { get; private set; }
    /// <summary>
    /// ロックダウン時間など　の各数値の「残り値」（めんどくさかったので使いまわし）
    /// </summary>
    CommonDefines.PieceControllerOption optionRemain;
    /// <summary>
    /// ピースを持っているフィールド
    /// </summary>
    readonly Field OWN_FIELD = null;

    public PieceControll( Field field )
    {
        OWN_FIELD = field;
    }

    /// <summary>
    /// インフィニティ等のパラメータをリセットします
    /// </summary>
    void InitParams()
    {
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// 初期位置を出現位置に設定します
    /// </summary>
    void SetPosToAppear()
    {
        Vector2Int basePos = OWN_FIELD.GetLeftTopFromPlayArea();
        PiecePos = basePos + HavePiece.GetInitPos();
    }

    /// <summary>
    /// 次に出現するピースを設定します
    /// </summary>
    public void SetPieceOfAppear( Piece piece )
    {
        HavePiece = piece;
        InitParams();
        SetPosToAppear();

        OnChangePiece.Invoke();
    }

    /// <summary>
    /// インフィニティ等のオプションを設定します
    /// </summary>
    public void SetControllOption( PieceControllerOption newOption )
    {
        CurrentOption = newOption;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="deltaTime">経過時間</param>
    public void Update( float deltaTime, float dropMultiPlayer = 1.0f )
    {
        optionRemain.dropTime -= (deltaTime * dropMultiPlayer) ;

        if ( optionRemain.dropTime < 0.0f )
        {
            // この時のdownは空中にある前提なので必ず成功する　はず
            TryMove( Vector2Int.down );
        }

        //optionRemain.lockDownTime -= deltaTime;
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
