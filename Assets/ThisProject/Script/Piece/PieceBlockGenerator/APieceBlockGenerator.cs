using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceBlockGenerator
{
    /// <summary>
    /// �u���b�N�̐������s���܂�
    /// </summary>
    /// <param name="sendGeneratedBlocks">���������u���b�N�̏o�͐�</param>
    /// <returns> ����������True�@����ȊO��False��Ԃ��܂� </returns>
    public abstract bool GenerateBlocks( ref List<ABlock> sendGeneratedBlocks );

    protected APieceBlockGenerator()
    {

    }
}
