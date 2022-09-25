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
    }

    public override AState Update()
    {
        return new PieceAppearState(owner);
    }
}
