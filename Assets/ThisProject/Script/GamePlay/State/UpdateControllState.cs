using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

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
        GameInput input = GameInput.Instance;

        float dropMultiplayer = 1.0f;
        if(input.IsDown( GameInput.EAction.DOWN ))
        {
            dropMultiplayer = 30.0f;
        }

        float deltaTime = Time.deltaTime;
        owner.PieceControll.Update( deltaTime, dropMultiplayer );

        // ˆÚ“®
        {
            Vector2Int moveDir = Vector2Int.zero;
            if (input.IsPress(GameInput.EAction.LEFT,  true)) moveDir.x += -1;
            if (input.IsPress(GameInput.EAction.RIGHT, true)) moveDir.x += 1;

            if (moveDir != Vector2Int.zero) owner.PieceControll.TryMove(moveDir);
        }

        // ‰ñ“]
        {
            ERotateDirection rotDir = ERotateDirection.NONE;
            if (input.IsPress(GameInput.EAction.ROTATE_LEFT))  rotDir = ERotateDirection.LEFT;
            if (input.IsPress(GameInput.EAction.ROTATE_RIGHT)) rotDir = ERotateDirection.RIGHT;

            if( rotDir != ERotateDirection.NONE ) owner.PieceControll.TryRotate( (int) rotDir );
        }

        AState nextState = null;
        if( owner.PieceControll.IsLocked )
        {
            nextState = new PieceLockedState( owner );
        }

        return nextState;
    }
}
