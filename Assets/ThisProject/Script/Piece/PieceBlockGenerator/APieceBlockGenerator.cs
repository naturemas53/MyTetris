using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceBlockGenerator< T > where T : class
{
    protected T option = null;

    /// <summary>
    /// �u���b�N�̐������s���܂�
    /// </summary>
    /// <param name="sendGeneratedBlocks">���������u���b�N�̏o�͐�</param>
    /// <returns> ����������True�@����ȊO��False��Ԃ��܂� </returns>
    public abstract bool GenerateBlocks( ref List<ABlock> sendGeneratedBlocks );

    /// <summary>
    /// �������̃I�v�V������ݒ�
    /// </summary>
    /// <param name="applyOption"></param>
    public virtual void SetOption ( T applyOption )
    {
        //Debug.LogWarningFormat("�ݒ�l���Ȃ��W�F�l���[�^�ׁ̈A");
    }

    protected APieceBlockGenerator()
    {

    }
}
