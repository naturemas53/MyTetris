using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAppearState : AState
{
    /// <summary>
    /// ピースの出現に失敗したか
    /// </summary>
    bool isFailAppear = false;
    /// <summary>
    /// Holdを使用するか
    /// </summary>
    bool isUseHold = false;

    AState nextState = null;

    public PieceAppearState(GamePlayContext owner, bool isUseHold = false)  : base(owner)
    {
        this.isUseHold = isUseHold;
    }

    public override void Initialize()
    {
        PieceControll controll = owner.PieceControll;
        ANextPieceSet nextPieceSet = owner.NextPieceSet;
        HoldCue hold = owner.HoldCue;

        Piece appearPiece = null;

        if (isUseHold)
        {
            hold.ExecuteHold( controll.HavePiece, out appearPiece );
        }

        if( appearPiece == null )
        {
            appearPiece = nextPieceSet.GetTopPiece( true );
        }

        isFailAppear = ! (controll.TryAppearPiece(appearPiece));
        nextState = ( isFailAppear ) ? null : new UpdateControllState(owner);


        if (!isUseHold)
        {
            owner.HoldCue.ResetAllowHold();
        }
    }

    public override AState Update()
    {
        return nextState;
    }
}
