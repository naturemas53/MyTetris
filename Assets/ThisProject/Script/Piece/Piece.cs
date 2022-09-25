using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Piece 
{
    readonly CommonDefines.PieceParam param = null;
    CommonDefines.EPieceRotate currentRotate = 0;

    /// <summary>
    /// 自身が回転したときのコールバック
    /// </summary>
    public UnityEvent OnRotateSelf { get; private set; } = new UnityEvent();

    /// <summary>
    /// 構成するブロック
    /// </summary>
    public List<ABlock> Blocks
    {
        get { return param.blocks; }
    }

    /// <summary>
    /// 軸からのブロック位置
    /// </summary>
    public List<Vector2Int> BlockOffsets
    {
        get { return param.pieceDatas.blockOffSets[ currentRotate ]; }
    }

    public Piece( CommonDefines.PieceParam selfParam, CommonDefines.EPieceRotate startRotate = CommonDefines.EPieceRotate.ZERO_O_CLOCK )
    {
        currentRotate = startRotate;
        param = selfParam;
    }

    /// <summary>
    /// 生成時の初期位置取得
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetInitPos()
    {
        return param.pieceDatas.initPos;
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

        checkKickBackList = param.pieceDatas.kickBacks[currentRotate].data[rotateDirection];

        OnRotateSelf.Invoke();
    }

    /// <summary>
    /// 自身の複製を返します。
    /// </summary>
    /// <returns></returns>
    public Piece CloneSelf()
    {
        Piece retPiece = new Piece( param, currentRotate );
        return retPiece;
    }

}
