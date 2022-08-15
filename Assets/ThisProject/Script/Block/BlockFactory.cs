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

        // TODO: �R���X�g���N�g����image�w��͌�ɔp�~�\��.
        switch ( needBlock )
        {
            case CommonDefines.EBlockType.BLOCK_EMPTY:  retBlock = new SpaceBlock(null);  break;
            case CommonDefines.EBlockType.BLOCK_WALL:   retBlock = new WallBlock(null);   break;
            case CommonDefines.EBlockType.BLOCK_NORMAL: retBlock = new NormalBlock(null); break;
            
            default:
                Debug.LogError( "�m��Ȃ��u���b�N�^�C�v or NONE���n����܂���. null��ԋp���܂��B" );
                break;
        }

        return retBlock;
    }
}
