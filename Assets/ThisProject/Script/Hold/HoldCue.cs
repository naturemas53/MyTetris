using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldCue
{
    /// <summary>
    /// Hold���s���̃C�x���g
    /// </summary>
    public UnityEvent OnExecutedHold = new UnityEvent();

    /// <summary>
    /// �ێ����̃s�[�X
    /// </summary>
    public Piece HoldPiece { get; private set; } = null;

    /// <summary>
    /// �z�[���h�ł���H
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
    /// �z�[���h���s
    /// </summary>
    public void ExecuteHold( Piece newestHoldPiece, out Piece holdedPiece )
    {
        holdedPiece = null;

        if( !IsCanHold )
        {
            Debug.LogWarning("�z�[���h�s�̎��ɌĂ΂�Ă��܂�...");
            return;
        }

        // Hold����ւ�
        holdedPiece = HoldPiece;
        HoldPiece = newestHoldPiece;

        // �����͕ۑ������Ƀ��Z�b�g.
        HoldPiece.SetRotateForce( CommonDefines.EPieceRotate.ZERO_O_CLOCK );

        OnExecutedHold.Invoke();

        IsCanHold = false;
    }

    /// <summary>
    /// �z�[���h�̎��s��Ԃ����Z�b�g
    /// </summary>
    public void ResetAllowHold()
    {
        IsCanHold = true;
    }
}
