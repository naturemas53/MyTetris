using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class GameInitState : AState
{
    public GameInitState(GamePlayContext owner) : base (owner)
    {

    }

    public override void Initialize()
    {
        // ネクストジェネレーターの生成 & ネク順の生成
        owner.ChangeNextGenerator( new SevenBagNextSet() );
        ANextPieceSet nextPieceSet = owner.NextPieceSet;
        ANextPieceSet.CommonOption nextOption = new ANextPieceSet.CommonOption();
        nextOption.generateNum = 3;
        nextPieceSet.CommonSetting = nextOption;
        nextPieceSet.GenerateNext();

        // ピース操作の準備
        PieceControll controll = owner.PieceControll;
        PieceControllerOption commonParams = new PieceControllerOption();
        commonParams.dropTime = 1.0f;
        commonParams.lockDownTime = 1.0f;
        commonParams.moveInfinity = 128;
        commonParams.rotateInfinity = 128;
        commonParams.wildInfinity = 0;

        controll.SetControllOption( commonParams );
        controll.SetPieceOfAppear( nextPieceSet.GetTopPiece( true ) );
    }

    public override AState Update()
    {
        // TODO:ゲーム中のステートを返す
        return null;
    }

}
