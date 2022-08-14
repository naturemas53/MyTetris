using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceBlockGenerator< T > where T : class
{
    protected T option = null;

    /// <summary>
    /// ブロックの生成を行います
    /// </summary>
    /// <param name="sendGeneratedBlocks">生成したブロックの出力先</param>
    /// <returns> 生成成功でTrue　それ以外はFalseを返します </returns>
    public abstract bool GenerateBlocks( ref List<ABlock> sendGeneratedBlocks );

    /// <summary>
    /// 生成時のオプションを設定
    /// </summary>
    /// <param name="applyOption"></param>
    public virtual void SetOption ( T applyOption )
    {
        //Debug.LogWarningFormat("設定値がないジェネレータの為、");
    }

    protected APieceBlockGenerator()
    {

    }
}
