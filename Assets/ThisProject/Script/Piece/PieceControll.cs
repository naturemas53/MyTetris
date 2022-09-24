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
    /// �ڒn��Ԃ�.
    /// </summary>
    public bool IsGround { get; private set; } = false;

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
    /// <summary>
    /// �ǂ��܂ŉ��ɐ���������ێ�����ϐ�
    /// </summary>
    int mostBottomPos = 0;

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
        IsGround = false;
        mostBottomPos = 0;
    }

    /// <summary>
    /// �����ʒu���o���ʒu�ɐݒ肵�܂�
    /// </summary>
    void SetPosToAppear()
    {
        Vector2Int basePos = OWN_FIELD.GetLeftTopFromPlayArea();
        PiecePos = basePos + HavePiece.GetInitPos();
        mostBottomPos = PiecePos.y;
    }

    /// <summary>
    /// �s�[�X���o�������܂�
    /// </summary>
    public bool TryAppearPiece( Piece piece )
    {
        HavePiece = piece;
        InitParams();
        SetPosToAppear();

        OnChangePiece.Invoke();

        return IsValidPiecePos( PiecePos );
    }

    /// <summary>
    /// �R���W�����`�F�b�N�@��
    /// </summary>
    /// <returns></returns>
    bool IsValidPiecePos(Vector2Int checkBasePos)
    {
        bool isSuccess = true;

        for( int i = 0; i < HavePiece.Blocks.Count; ++i )
        {
            Vector2Int checkPos = checkBasePos + HavePiece.BlockOffsets[i];
            ABlock block = OWN_FIELD.GetBlock( checkPos );

            if (block == null)
            {
                // �����ɂ���������ꍇ�͂��������͈͊O�����ǃl
                isSuccess = false;
                break;
            }

            if(block.IsHaveColision)
            {
                isSuccess = false;
                break;
            }
        }

        return isSuccess;
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
        if( IsGround )
        {
            // �ڒn��ԂȂ�ڒ��܂ł̎��Ԃ����炷
            optionRemain.lockDownTime -= deltaTime;
        }
        else
        {
            // �󒆂ɂ���Ȃ�A1�i������܂ł̎��Ԃ��v��
            optionRemain.dropTime -= (deltaTime * dropMultiPlayer) ;
            if ( optionRemain.dropTime < 0.0f )
            {
                // ���̎���down�͋󒆂ɂ���O��Ȃ̂ŕK����������@�͂�
                TryMove( new Vector2Int( 0, 1 ) );
            }
        }
    }

    /// <summary>
    /// �s�[�X�ړ����s���܂�(���s�̉\���A���j
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <returns>�ړ��Ɂ@�����FTrue ���s�FFalse</returns>
    public bool TryMove( Vector2Int moveDirection )
    {
        if( IsGround && moveDirection.y > 0 )
        {
            // �ڒn�ς݂Ȃ̂ɉ��ɓ�����Ă�...�@���Ă��Ƃŉ��ړ��𖳌���
            moveDirection.y = 0;
        }

        Vector2Int checkPos = PiecePos + moveDirection;
        if(!IsValidPiecePos( checkPos ))
        {
            // ��Q���Ƃ������ē�����
            return false;
        }

        // �ړ��O�A�ڒn��Ԃ������H
        bool prevGround = IsGround;

        // �ړ��Ɛڒn�m�F.
        PiecePos = PiecePos + moveDirection;
        CheckGroundOfSelfPiece();

        if( moveDirection.y > 0 )
        {
            ResetRemainValueFromDrop();
        }
        else if ( IsGround && moveDirection.x != 0 )
        {
            // ���ɓ������Ƃ��̓C���t�B�j�e�B�`�F�b�N
            ResetLockDownTime( true );
        }

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

    /// <summary>
    /// ���b�N�_�E���^�C���̃��Z�b�g
    /// </summary>
    void ResetLockDownTime( bool isMove )
    {
        bool haveWild = optionRemain.wildInfinity > 0;
        bool haveMove = optionRemain.moveInfinity > 0;
        bool haveRotate = optionRemain.rotateInfinity > 0;

        bool haveMoveInfinity = haveMove || haveWild;
        bool haveRotateInfinity = haveRotate || haveWild;

        bool isReset = (isMove) ? haveMoveInfinity : haveRotateInfinity;

        if( !isReset ) return;

        optionRemain.wildInfinity -= 1;

        if( isMove )
        {
            optionRemain.moveInfinity -= 1;
        }
        else
        {
            optionRemain.rotateInfinity -= 1;
        }

        optionRemain.lockDownTime = CurrentOption.lockDownTime;
    }

    /// <summary>
    /// ���������ۂ̃p�����[�^���Z�b�g.
    /// </summary>
    void ResetRemainValueFromDrop()
    {
        if( PiecePos.y <= mostBottomPos ) return;

        mostBottomPos = PiecePos.y;
        optionRemain = CurrentOption;
    }

    /// <summary>
    /// �����̃s�[�X���ڒn���Ă��邩
    /// </summary>
    void CheckGroundOfSelfPiece()
    {
        IsGround = !(IsValidPiecePos( PiecePos + new Vector2Int( 0, 1 ) ));
    }


}
