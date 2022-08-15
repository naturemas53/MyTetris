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

        // TODO: コンストラクト時のimage指定は後に廃止予定.
        switch ( needBlock )
        {
            case CommonDefines.EBlockType.BLOCK_EMPTY:  retBlock = new SpaceBlock(null);  break;
            case CommonDefines.EBlockType.BLOCK_WALL:   retBlock = new WallBlock(null);   break;
            case CommonDefines.EBlockType.BLOCK_NORMAL: retBlock = new NormalBlock(null); break;
            
            default:
                Debug.LogError( "知らないブロックタイプ or NONEが渡されました. nullを返却します。" );
                break;
        }

        return retBlock;
    }
}
