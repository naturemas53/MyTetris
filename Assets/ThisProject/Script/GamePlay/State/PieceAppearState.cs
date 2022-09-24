using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAppearState : AState
{
    /// <summary>
    /// ピースの出現に失敗したか
    /// </summary>
    bool isFailAppear = false;

    public PieceAppearState(GamePlayContext owner)  : base(owner)
    {
    }

    public override void Initialize()
    {
        PieceControll controll = owner.PieceControll;
        ANextPieceSet nextPieceSet = owner.NextPieceSet;

        isFailAppear = controll.TryAppearPiece(nextPieceSet.GetTopPiece(true));
    }

    public override AState Update()
    {
        return null;
    }
}
