using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactoryInstances : SingletonMonoBehaviour<PieceFactoryInstances>
{
    Dictionary<System.Type, APieceFactory> createFactorys = null;
    protected override void Initialize()
    {
        createFactorys = new Dictionary<System.Type, APieceFactory>();

    }

    public AFactory GetFactory< AFactory > () where AFactory : APieceFactory, new()
    {
        AFactory retFactory = null;

        if( !createFactorys.ContainsKey( typeof(AFactory) ) )
        {
            createFactorys.Add( typeof(AFactory), new AFactory() );
        }

        retFactory = (AFactory)createFactorys[ typeof(AFactory) ];

        return retFactory;
    }
}
