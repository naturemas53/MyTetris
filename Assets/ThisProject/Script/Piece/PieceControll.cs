using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControll
{
    /// <summary>
    /// �����s�[�X
    /// </summary>
    Piece havePiece = null;
    
    /// <summary>
    /// �s�[�X�ʒu
    /// </summary>
    Vector2Int piecePos = Vector2Int.zero;

    /// <summary>
    /// ���b�N�_�E�����ԂȂǂ̌���̃I�v�V����
    /// </summary>
    public CommonDefines.PieceControllerOption CurrentOption { get; private set; }
    /// <summary>
    /// ���b�N�_�E�����ԂȂǁ@�̊e���l�́u�c��l�v�i�߂�ǂ����������̂Ŏg���܂킵�j
    /// </summary>
    CommonDefines.PieceControllerOption optionRemain;

    public PieceControll( Piece firstPiece, CommonDefines.PieceControllerOption firstOption )
    {
        havePiece = firstPiece;
        CurrentOption = firstOption;
        InitParams();
    }

    /// <summary>
    /// �C���t�B�j�e�B���̃p�����[�^�����Z�b�g���܂�
    /// </summary>
    void InitParams()
    {
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    /// <param name="deltaTime">�o�ߎ���</param>
    public void Update( float deltaTime )
    {
        optionRemain.lockDownTime -= deltaTime;
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
