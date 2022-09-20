using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CommonDefines;

public class PieceControll
{
    /// <summary>
    /// �����s�[�X
    /// </summary>
    public Piece HavePiece { get; private set; } = null;
    /// <summary>
    /// �s�[�X�ʒu
    /// </summary>
    public Vector2Int PiecePos { get; private set; } = Vector2Int.zero;
    /// <summary>
    /// �ڒ��ς݂�
    /// </summary>
    public bool IsLocked
    {
        get
        {
            return optionRemain.lockDownTime <= 0.0f; 
        }
    }

    /// <summary>
    /// �s�[�X�ύX���̃R�[���o�b�N
    /// </summary>
    public UnityEvent OnChangePiece { get; private set; } = new UnityEvent();

    /// <summary>
    /// ���b�N�_�E�����ԂȂǂ̌���̃I�v�V����
    /// </summary>
    public CommonDefines.PieceControllerOption CurrentOption { get; private set; }
    /// <summary>
    /// ���b�N�_�E�����ԂȂǁ@�̊e���l�́u�c��l�v�i�߂�ǂ����������̂Ŏg���܂킵�j
    /// </summary>
    CommonDefines.PieceControllerOption optionRemain;
    /// <summary>
    /// �s�[�X�������Ă���t�B�[���h
    /// </summary>
    readonly Field OWN_FIELD = null;

    public PieceControll( Field field )
    {
        OWN_FIELD = field;
    }

    /// <summary>
    /// �C���t�B�j�e�B���̃p�����[�^�����Z�b�g���܂�
    /// </summary>
    void InitParams()
    {
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// �����ʒu���o���ʒu�ɐݒ肵�܂�
    /// </summary>
    void SetPosToAppear()
    {
        Vector2Int basePos = OWN_FIELD.GetLeftTopFromPlayArea();
        PiecePos = basePos + HavePiece.GetInitPos();
    }

    /// <summary>
    /// ���ɏo������s�[�X��ݒ肵�܂�
    /// </summary>
    public void SetPieceOfAppear( Piece piece )
    {
        HavePiece = piece;
        InitParams();
        SetPosToAppear();

        OnChangePiece.Invoke();
    }

    /// <summary>
    /// �C���t�B�j�e�B���̃I�v�V������ݒ肵�܂�
    /// </summary>
    public void SetControllOption( PieceControllerOption newOption )
    {
        CurrentOption = newOption;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    /// <param name="deltaTime">�o�ߎ���</param>
    public void Update( float deltaTime, float dropMultiPlayer = 1.0f )
    {
        optionRemain.dropTime -= (deltaTime * dropMultiPlayer) ;

        if ( optionRemain.dropTime < 0.0f )
        {
            // ���̎���down�͋󒆂ɂ���O��Ȃ̂ŕK����������@�͂�
            TryMove( Vector2Int.down );
        }

        //optionRemain.lockDownTime -= deltaTime;
    }

    /// <summary>
    /// �s�[�X�ړ����s���܂�(���s�̉\���A���j
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>�ړ��Ɂ@�����FTrue ���s�FFalse</returns>
    public bool TryMove( Vector2Int moveDirection )
    {
        return true;
    }

    /// <summary>
    /// ��]���s���܂�(���s�̉\���A���j
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>��]�Ɂ@�����FTrue ���s�FFalse</returns>
    public bool TryRotate(int rotateDirection)
    {
        return true;
    }
}
