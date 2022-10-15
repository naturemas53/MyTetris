using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class GameInitState : AState
{
    AState nextState = null;

    public GameInitState(GamePlayContext owner) : base (owner)
    {

    }

    public override void Initialize()
    {
        // ネクストジェネレーターの生成 & ネク順の生成
        ANextPieceSet nextPieceSet = new SevenBagNextSet();
        ANextPieceSet.CommonOption nextOption = new ANextPieceSet.CommonOption();
        nextOption.generateNum = 7;
        nextPieceSet.CommonSetting = nextOption;
        nextPieceSet.GenerateNext();
        owner.ChangeNextGenerator( nextPieceSet );

        // ピース操作の準備
        PieceControll controll = owner.PieceControll;
        PieceControllerOption commonParams = new PieceControllerOption();
        commonParams.dropTime = 1.0f;
        commonParams.lockDownTime = 1.0f;
        commonParams.moveInfinity = 128;
        commonParams.rotateInfinity = 128;
        commonParams.wildInfinity = 0;

        controll.SetControllOption( commonParams );

        AState appearState = new PieceAppearState(owner);
        appearState.Initialize();
        nextState = appearState.Update();
    }

    public override AState Update()
    {
        return nextState;
    }

}
