using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateControllState : AState
{
    public UpdateControllState( GamePlayContext owner ) : base( owner )
    {

    }

    public override void Initialize()
    {
        
    }

    public override AState Update()
    {
        float deltaTime = Time.deltaTime;
        owner.PieceControll.Update( deltaTime );

        AState nextState = null;
        if( owner.PieceControll.IsLocked )
        {
            nextState = new PieceLockedState( owner );
        }

        return nextState;
    }
}
