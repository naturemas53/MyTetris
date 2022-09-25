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
    /// 接地状態か.
    /// </summary>
    public bool IsGround { get; private set; } = false;

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
    /// <summary>
    /// どこまで下に潜ったかを保持する変数
    /// </summary>
    int mostBottomPos = 0;

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
        IsGround = false;
        mostBottomPos = 0;
    }

    /// <summary>
    /// 初期位置を出現位置に設定します
    /// </summary>
    void SetPosToAppear()
    {
        Vector2Int basePos = OWN_FIELD.GetLeftTopFromPlayArea();
        PiecePos = basePos + HavePiece.GetInitPos();
        mostBottomPos = PiecePos.y;
    }

    /// <summary>
    /// ピースを出現させます
    /// </summary>
    public bool TryAppearPiece( Piece piece )
    {
        HavePiece = piece;
        InitParams();
        SetPosToAppear();

        OnChangePiece.Invoke();

        return IsValidPiecePos( PiecePos );
    }

    /// <summary>
    /// コリジョンチェック　等
    /// </summary>
    /// <returns></returns>
    bool IsValidPiecePos(Vector2Int checkBasePos)
    {
        bool isSuccess = true;

        for( int i = 0; i < HavePiece.Blocks.Count; ++i )
        {
            Vector2Int checkPos = checkBasePos + HavePiece.BlockOffsets[i];
            ABlock block = OWN_FIELD.GetBlock( checkPos );

            if (block == null)
            {
                // ここにきちゃった場合はそもそも範囲外だけどネ
                isSuccess = false;
                break;
            }

            if(block.IsHaveColision)
            {
                isSuccess = false;
                break;
            }
        }

        return isSuccess;
    }

    /// <summary>
    /// インフィニティ等のオプションを設定します
    /// </summary>
    public void SetControllOption( PieceControllerOption newOption )
    {
        CurrentOption = newOption;
    }

    /// <summary>
    /// ピースをフィールド内に配置します.
    /// </summary>
    public void ApplyPieceToField()
    {
        for (int i = 0; i < HavePiece.Blocks.Count; ++i)
        {
            Vector2Int blockPos = PiecePos + HavePiece.BlockOffsets[i];
            OWN_FIELD.SetBlock(HavePiece.Blocks[i], blockPos);
        }

        HavePiece = null;
        OnChangePiece.Invoke();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="deltaTime">経過時間</param>
    public void Update( float deltaTime, float dropMultiPlayer = 1.0f )
    {
        if( IsGround )
        {
            // 接地状態なら接着までの時間を減らす
            optionRemain.lockDownTime -= deltaTime;
        }
        else
        {
            // 空中にいるなら、1段落ちるまでの時間を計る
            optionRemain.dropTime -= (deltaTime * dropMultiPlayer) ;
            if ( optionRemain.dropTime < 0.0f )
            {
                // この時のdownは空中にある前提なので必ず成功する　はず
                TryMove( new Vector2Int( 0, 1 ) );
            }
        }
    }

    /// <summary>
    /// ピース移動を行います(失敗の可能性アリ）
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>移動に　成功：True 失敗：False</returns>
    public bool TryMove( Vector2Int moveDirection )
    {
        if( IsGround && moveDirection.y > 0 )
        {
            // 接地済みなのに下に動かれても...　ってことで下移動を無効化
            moveDirection.y = 0;
        }

        Vector2Int checkPos = PiecePos + moveDirection;
        if(!IsValidPiecePos( checkPos ))
        {
            // 障害物とかあって動けぬ
            return false;
        }

        // 移動前、接地状態だった？
        bool prevGround = IsGround;

        // 移動と接地確認.
        PiecePos = PiecePos + moveDirection;
        CheckGroundOfSelfPiece();

        if( moveDirection.y > 0 )
        {
            ResetRemainValueFromDrop();
        }
        else if ( IsGround && moveDirection.x != 0 )
        {
            // 横に動いたときはインフィニティチェック
            ResetLockDownTime( true );
        }

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

    /// <summary>
    /// ロックダウンタイムのリセット
    /// </summary>
    void ResetLockDownTime( bool isMove )
    {
        bool haveWild = optionRemain.wildInfinity > 0;
        bool haveMove = optionRemain.moveInfinity > 0;
        bool haveRotate = optionRemain.rotateInfinity > 0;

        bool haveMoveInfinity = haveMove || haveWild;
        bool haveRotateInfinity = haveRotate || haveWild;

        bool isReset = (isMove) ? haveMoveInfinity : haveRotateInfinity;

        if( !isReset ) return;

        optionRemain.wildInfinity -= 1;

        if( isMove )
        {
            optionRemain.moveInfinity -= 1;
        }
        else
        {
            optionRemain.rotateInfinity -= 1;
        }

        optionRemain.lockDownTime = CurrentOption.lockDownTime;
    }

    /// <summary>
    /// 落下した際のパラメータリセット.
    /// </summary>
    void ResetRemainValueFromDrop()
    {
        if( PiecePos.y <= mostBottomPos ) return;

        mostBottomPos = PiecePos.y;
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// 自分のピースが接地しているか
    /// </summary>
    void CheckGroundOfSelfPiece()
    {
        IsGround = !(IsValidPiecePos( PiecePos + new Vector2Int( 0, 1 ) ));
    }
}
