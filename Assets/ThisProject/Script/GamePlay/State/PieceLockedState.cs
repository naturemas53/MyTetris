using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLockedState : AState
{
    public PieceLockedState( GamePlayContext owner ) : base( owner )
    {

    }

    public override void Initialize()
    {
        owner.PieceControll.ApplyPieceToField();

        if( owner.Field.BookedAlignLineIdxs.Count > 0 )
        {
            owner.Field.ExecuteAlignLine();
            owner.Field.PackField();
        }
    }

    public override AState Update()
    {
        return new PieceAppearState(owner);
    }
}
