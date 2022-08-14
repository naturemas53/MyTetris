using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState 
{
    /// <summary>
    /// 当ステートの所有者(必要に応じてこっからフィールドやら取ってください）
    /// </summary>
    protected GamePlayContext owner;

    protected AState( GamePlayContext selfOwner )
    {
        owner = selfOwner;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public abstract void Initialize();
    
    /// <summary>
    /// State更新
    /// </summary>
    /// <returns> 次に流れるStateがある場合は更新 維持の場合はnull </returns>
    public abstract AState Update();
}
