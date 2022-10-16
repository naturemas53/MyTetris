using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldCue
{
    /// <summary>
    /// Hold実行時のイベント
    /// </summary>
    public UnityEvent OnExecutedHold = new UnityEvent();

    /// <summary>
    /// 保持中のピース
    /// </summary>
    public Piece HoldPiece { get; private set; } = null;

    /// <summary>
    /// ホールドできる？
    /// </summary>
    public bool IsCanHold { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ホールド実行
    /// </summary>
    public void ExecuteHold( Piece newestHoldPiece, out Piece holdedPiece )
    {
        holdedPiece = null;

        if( !IsCanHold )
        {
            Debug.LogWarning("ホールド不可の時に呼ばれています...");
            return;
        }

        // Hold入れ替え
        holdedPiece = HoldPiece;
        HoldPiece = newestHoldPiece;

        // 向きは保存せずにリセット.
        HoldPiece.SetRotateForce( CommonDefines.EPieceRotate.ZERO_O_CLOCK );

        OnExecutedHold.Invoke();

        IsCanHold = false;
    }

    /// <summary>
    /// ホールドの実行状態をリセット
    /// </summary>
    public void ResetAllowHold()
    {
        IsCanHold = true;
    }
}
