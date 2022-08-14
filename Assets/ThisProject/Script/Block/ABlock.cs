using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABlock 
{
    // Field ownerFIeld;
    /// <summary>
    /// 当たり判定持ち？
    /// </summary>
    public bool IsHaveColision { get; protected set; } = false;
    /// <summary>
    /// 消せる？
    /// </summary>
    public bool IsCanLineAlign { get; protected set; } = false;
    /// <summary>
    /// ブロック種類
    /// 変わることはない もし変わるなら、そもそもブロックインスタンス自体変える必要がある
    /// </summary>
    public readonly CommonDefines.EBlockType BLOCK_TYPE = CommonDefines.EBlockType.BLOCK_NONE;

    /// <summary>
    /// 使用画像
    /// </summary>
    public readonly Sprite selfImage = null;

    public ABlock(Sprite image, CommonDefines.EBlockType selfBlockType)
    {
        selfImage = image;
        BLOCK_TYPE = selfBlockType;
    }

    /// <summary>
    /// ブロック消去
    /// </summary>
    public virtual void Erace() 
    {
        //Debug.LogWarningFormat("消去時の動作が定義されていません。 仕様であれば当警告は無視してください。:{0}", BLOCK_TYPE.ToString());
    }

}
