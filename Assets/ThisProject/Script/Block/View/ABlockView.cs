using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public abstract class ABlockView : MonoBehaviour
{
    /// <summary>
    /// 親のキャンバスサイズ
    /// </summary>
    static readonly Vector2 PARENT_SIZE = new Vector2(100.0f,100.0f);
    /// <summary>
    /// このクラス（≒派生先）で取り扱えるブロック種類
    /// </summary>
    public readonly EBlockType USABLE_TYPE = EBlockType.BLOCK_NONE;

    /// <summary>
    /// 参照中のブロック
    /// </summary>
    protected ABlock refBlock = null;

    public ABlockView( EBlockType blockType )
    {
        USABLE_TYPE = blockType;
    }

    /// <summary>
    /// 生成時の初期化
    /// </summary>
    private void Awake()
    {
        InitilaizeFromAwake();
    }

    /// <summary>
    /// Awake時の初期化を行います.
    /// </summary>
    protected abstract void InitilaizeFromAwake();

    /// <summary>
    /// 初期化
    /// </summary>
    public bool Initialize( ABlock block ) 
    {
        if( !IsUsableType( block ) )
        {
            Debug.LogError("想定してないブロックが渡されたので、表示できません。");
            return false;
        }

        refBlock = block;

        return InitializeImpl();
    }

    /// <summary>
    /// 初期化（派生先用）
    /// </summary>
    /// <returns></returns>
    protected virtual bool InitializeImpl() { return true; }

    /// <summary>
    /// このクラス（≒派生先）で取り扱うブロックか
    /// </summary>
    /// <param name="block"></param>
    /// <returns>取り扱えるならTrue そうでなければFalse</returns>
    public bool IsUsableType(ABlock block) { return block.BLOCK_TYPE == USABLE_TYPE; }

    /// <summary>
    /// 自身が持っているブロックを、キャストしたうえで取得します
    /// </summary>
    /// <typeparam name="TBlock"></typeparam>
    /// <returns></returns>
    protected TBlock GetSelfBlockOfCasted<TBlock>() where TBlock : ABlock { return refBlock as TBlock; }
}
