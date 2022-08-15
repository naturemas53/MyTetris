using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : SingletonMonoBehaviour<BlockFactory>
{
    protected override void Initialize()
    {
        // ブロック生成用のクラスだから特にない...?
    }

    /// <summary>
    /// ブロックを生成して返します.
    /// </summary>
    /// <returns></returns>
    public ABlock CreateBlock( CommonDefines.EBlockType needBlock )
    {
        ABlock retBlock = null;

        switch ( needBlock )
        {
            case CommonDefines.EBlockType.BLOCK_EMPTY:  retBlock = new SpaceBlock();  break;
            case CommonDefines.EBlockType.BLOCK_WALL:   retBlock = new WallBlock();   break;
            case CommonDefines.EBlockType.BLOCK_NORMAL: retBlock = new NormalBlock(); break;
            
            default:
                Debug.LogError( "知らないブロックタイプ or NONEが渡されました. nullを返却します。" );
                break;
        }

        return retBlock;
    }

    /// <summary>
    /// ブロックを生成して返します(こっちはキャストまでしてくれる版）.
    /// </summary>
    /// <returns></returns>
    public T CreateBlock<T>(CommonDefines.EBlockType needBlock) where T : ABlock
    {
        ABlock createdBlock = CreateBlock( needBlock );

        if( createdBlock == null )
        {
            return null;
        }

        return createdBlock as T;
    }
}
