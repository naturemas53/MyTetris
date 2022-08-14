using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece 
{
    List<ABlock> blocks = null;
    CommonDefines.PieceParam param = null;
    CommonDefines.EPieceRotate currentRotate = 0;

    public readonly CommonDefines.EShapeType SHAPE_TYPE = CommonDefines.EShapeType.SHAPE_NONE;

    public Piece( CommonDefines.EShapeType selfShapeType, CommonDefines.PieceParam selfParam )
    {
        SHAPE_TYPE = selfShapeType;
        currentRotate = CommonDefines.EPieceRotate.ZERO_O_CLOCK; // 最初は0度から
        param = selfParam;
    }

    /// <summary>
    /// 生成時の初期位置取得
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetInitPos()
    {
        return param.initPos;
    }

    /// <summary>
    /// ピース回転
    /// </summary>
    /// <param name="rotateDirection">回転方向</param>
    /// <param name="checkKickBackList">回転出来なかった際のキックバックリスト</param>
    public void Rotate( CommonDefines.ERotateDirection rotateDirection, out List<Vector2Int> checkKickBackList )
    {
        int nextRotate = (int)currentRotate + (int)rotateDirection;
        currentRotate = (CommonDefines.EPieceRotate)CommonUtil.LoopValue( nextRotate, 0, (int)CommonDefines.EPieceRotate.MAX - 1);

        checkKickBackList = param.kickBackChecks[(int)currentRotate].kickBackDelta[rotateDirection];
    }

}
