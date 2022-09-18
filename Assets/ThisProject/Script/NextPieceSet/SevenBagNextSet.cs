using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class SevenBagNextSet : ANextPieceSet
{
    List<EShapeType> inBagPieces = null;

    public SevenBagNextSet()
    {
        inBagPieces = new List<EShapeType>();
        ReloadBag();
    }

    protected override void AddNextPieceImpl()
    {
        int pieceIdx = inBagPieces.Count;
        EShapeType createShape = inBagPieces[ pieceIdx ];

        // TODO: �s�[�X���� & �ǉ�����

        inBagPieces.RemoveAt( pieceIdx );

        if( inBagPieces.Count <= 0 )
        {
            ReloadBag();
        }
    }

    /// <summary>
    /// �o�b�O���[���܂��B
    /// </summary>
    void ReloadBag()
    {
        for( EShapeType shape = EShapeType.SHAPE_Z; shape < EShapeType.SHAPE_SPECIAL; ++shape )
        {
            inBagPieces.Add( shape );
        }
    }

}
