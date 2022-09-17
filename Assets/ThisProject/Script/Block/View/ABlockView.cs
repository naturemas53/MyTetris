using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public abstract class ABlockView : MonoBehaviour
{
    /// <summary>
    /// �e�̃L�����o�X�T�C�Y
    /// </summary>
    static readonly Vector2 PARENT_SIZE = new Vector2(100.0f,100.0f);
    /// <summary>
    /// ���̃N���X�i���h����j�Ŏ�舵����u���b�N���
    /// </summary>
    public readonly EBlockType USABLE_TYPE = EBlockType.BLOCK_NONE;

    /// <summary>
    /// �Q�ƒ��̃u���b�N
    /// </summary>
    protected ABlock refBlock = null;

    public ABlockView( EBlockType blockType )
    {
        USABLE_TYPE = blockType;
    }

    /// <summary>
    /// �������̏�����
    /// </summary>
    private void Awake()
    {
        InitilaizeFromAwake();
    }

    /// <summary>
    /// Awake���̏��������s���܂�.
    /// </summary>
    protected abstract void InitilaizeFromAwake();

    /// <summary>
    /// ������
    /// </summary>
    public bool Initialize( ABlock block ) 
    {
        if( !IsUsableType( block ) )
        {
            Debug.LogError("�z�肵�ĂȂ��u���b�N���n���ꂽ�̂ŁA�\���ł��܂���B");
            return false;
        }

        refBlock = block;

        return InitializeImpl();
    }

    /// <summary>
    /// �������i�h����p�j
    /// </summary>
    /// <returns></returns>
    protected virtual bool InitializeImpl() { return true; }

    /// <summary>
    /// ���̃N���X�i���h����j�Ŏ�舵���u���b�N��
    /// </summary>
    /// <param name="block"></param>
    /// <returns>��舵����Ȃ�True �����łȂ����False</returns>
    public bool IsUsableType(ABlock block) { return block.BLOCK_TYPE == USABLE_TYPE; }

    /// <summary>
    /// ���g�������Ă���u���b�N���A�L���X�g���������Ŏ擾���܂�
    /// </summary>
    /// <typeparam name="TBlock"></typeparam>
    /// <returns></returns>
    protected TBlock GetSelfBlockOfCasted<TBlock>() where TBlock : ABlock { return refBlock as TBlock; }
}
