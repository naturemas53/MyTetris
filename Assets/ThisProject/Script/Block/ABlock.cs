using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABlock
{
    /// <summary>
    /// ブロックを所有しているフィールド.
    /// </summary>
    Field ownerField = null;
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

    public ABlock(CommonDefines.EBlockType selfBlockType)
    {
        BLOCK_TYPE = selfBlockType;
    }

    /// <summary>
    /// ラインが揃った時の動作
    /// </summary>
    /// <param name="selfBlockPos"> 自ブロックの位置 </param>
    public virtual void ExecuteLineAligned( Vector2Int selfBlockPos ) 
    {
        //Debug.LogWarningFormat("消去時の動作が定義されていません。 仕様であれば当警告は無視してください。:{0}", BLOCK_TYPE.ToString());
    }

    /// <summary>
    /// このブロックを所持します（Field必須）.
    /// </summary>
    public void OwnedSelf(Field ownField)
    {
        ownerField = ownField;
    }

    /// <summary>
    /// 当ブロックを手放します.
    /// </summary>
    public void LetGoSelf()
    {
        ownerField = null;
    }

}
