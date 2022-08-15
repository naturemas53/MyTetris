using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceFactory
{
    /// <summary>
    /// ピース生成
    /// </summary>
    /// <returns>生成成功でTrue　それ以外はFalse</returns>
    public abstract Piece CreatePiece();

    protected APieceFactory()
    {

    }
}
