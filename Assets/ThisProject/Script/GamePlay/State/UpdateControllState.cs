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
        float dropMultiplayer = 1.0f;
        if(Input.GetKey(KeyCode.DownArrow))
        {
            dropMultiplayer = 10.0f;
        }

        float deltaTime = Time.deltaTime;
        owner.PieceControll.Update( deltaTime, dropMultiplayer );

        Vector2Int moveDir = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.LeftArrow))  moveDir.x += -1;
        if (Input.GetKeyDown(KeyCode.RightArrow)) moveDir.x += 1;

        if( moveDir != Vector2Int.zero ) owner.PieceControll.TryMove( moveDir );

        AState nextState = null;
        if( owner.PieceControll.IsLocked )
        {
            nextState = new PieceLockedState( owner );
        }

        return nextState;
    }
}
