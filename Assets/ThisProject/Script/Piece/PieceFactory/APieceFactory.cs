using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceFactory
{
    /// <summary>
    /// �s�[�X����
    /// </summary>
    /// <returns>����������True�@����ȊO��False</returns>
    public abstract Piece CreatePiece();

    protected APieceFactory()
    {

    }
}
