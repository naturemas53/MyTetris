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
        int pieceIdx = Random.Range(0, inBagPieces.Count);

        EShapeType createShape = inBagPieces[ pieceIdx ];

        NormalPieceFactory factory = PieceFactoryInstances.Instance.GetFactory< NormalPieceFactory >();
        NormalPieceFactory.Option option = new NormalPieceFactory.Option();
        option.shapeType = createShape;
        factory.SetOption( option );

        nextPieces.Add(factory.CreatePiece());

        inBagPieces.RemoveAt( pieceIdx );

        if( inBagPieces.Count <= 0 )
        {
            ReloadBag();
        }
    }

    /// <summary>
    /// バッグを補充します。
    /// </summary>
    void ReloadBag()
    {
        for( EShapeType shape = EShapeType.SHAPE_Z; shape < EShapeType.SHAPE_SPECIAL; ++shape )
        {
            inBagPieces.Add( shape );
        }
    }

}
