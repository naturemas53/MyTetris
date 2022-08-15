using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APieceBlockGenerator
{
    /// <summary>
    /// ブロックの生成を行います
    /// </summary>
    /// <param name="sendGeneratedBlocks">生成したブロックの出力先</param>
    /// <returns> 生成成功でTrue　それ以外はFalseを返します </returns>
    public abstract bool GenerateBlocks( ref List<ABlock> sendGeneratedBlocks );

    protected APieceBlockGenerator()
    {

    }
}
