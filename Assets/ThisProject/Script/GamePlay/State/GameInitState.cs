using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitState : AState
{
    public GameInitState(GamePlayContext owner) : base (owner)
    {

    }

    public override void Initialize()
    {
        // TODO: ゲーム初期化処理
    }

    public override AState Update()
    {
        return null;
    }

}
