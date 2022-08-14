using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState 
{
    /// <summary>
    /// ���X�e�[�g�̏��L��(�K�v�ɉ����Ă�������t�B�[���h������Ă��������j
    /// </summary>
    protected GamePlayContext owner;

    protected AState( GamePlayContext selfOwner )
    {
        owner = selfOwner;
    }

    /// <summary>
    /// ������
    /// </summary>
    public abstract void Initialize();
    
    /// <summary>
    /// State�X�V
    /// </summary>
    /// <returns> ���ɗ����State������ꍇ�͍X�V �ێ��̏ꍇ��null </returns>
    public abstract AState Update();
}
