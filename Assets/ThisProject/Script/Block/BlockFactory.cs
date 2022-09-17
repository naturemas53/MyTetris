using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : SingletonMonoBehaviour<BlockFactory>
{


    protected override void Initialize()
    {
        // �u���b�N�����p�̃N���X��������ɂȂ�...?
    }

    /// <summary>
    /// �u���b�N�𐶐����ĕԂ��܂�.
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
                Debug.LogError( "�m��Ȃ��u���b�N�^�C�v or NONE���n����܂���. null��ԋp���܂��B" );
                break;
        }

        return retBlock;
    }

    /// <summary>
    /// �u���b�N�𐶐����ĕԂ��܂�(�������͎w��̌^�Ő������Ă����Łj.
    /// ... ��������Ȃ炻���������̊֐�����Ȃ��̂ł�...?
    /// </summary>
    /// <returns></returns>
    public T CreateBlock<T>() where T : ABlock, new()
    {
        return new T();
    }
}
