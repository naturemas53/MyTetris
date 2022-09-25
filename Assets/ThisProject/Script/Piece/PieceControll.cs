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
    bool IsValidPiecePos(Vector2Int checkBasePos, Piece checkPiece = null)
    {
        bool isSuccess = true;

        Piece targetPiece = checkPiece;
        if (targetPiece == null) targetPiece = HavePiece;

        for( int i = 0; i < targetPiece.Blocks.Count; ++i )
        {
            Vector2Int checkPos = checkBasePos + targetPiece.BlockOffsets[i];
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
    /// �s�[�X���t�B�[���h���ɔz�u���܂�.
    /// </summary>
    public void ApplyPieceToField()
    {
        for (int i = 0; i < HavePiece.Blocks.Count; ++i)
        {
            Vector2Int blockPos = PiecePos + HavePiece.BlockOffsets[i];
            OWN_FIELD.SetBlock(HavePiece.Blocks[i], blockPos);
        }

        HavePiece = null;
        OnChangePiece.Invoke();
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
                float tmpOverTime = optionRemain.dropTime;
                // ���̎���down�͋󒆂ɂ���O��Ȃ̂ŕK����������@�͂�
                TryMove( new Vector2Int( 0, 1 ) );

                // ���������͂����ň���.
                optionRemain.dropTime += tmpOverTime;
            }
        }
    }

    /// <summary>
    /// �n�[�h�h���b�v�i���܂ŋ}�]���� �� �ڒ��j���s���܂�
    /// </summary>
    public void HardDrop()
    {
        // ���܂ŉ��낵�؂���
        while( IsValidPiecePos( PiecePos + new Vector2Int(0,1) ) )
        {
            PiecePos = PiecePos + new Vector2Int(0, 1);
        }

        // �ڒ�(�ꉞ�Őڒn��Ԃɂ����Ă���)
        IsGround = true;
        optionRemain.lockDownTime = 0.0f;
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
        else if ( prevGround && moveDirection.x != 0 )
        {
            // ���ɓ������Ƃ��̓C���t�B�j�e�B�`�F�b�N
            UseInfinity( true );
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
        ERotateDirection rotDir = (ERotateDirection)rotateDirection;
        Piece clonePiece = HavePiece.CloneSelf();

        List<Vector2Int> kickbacks = null;
        clonePiece.Rotate( rotDir, out kickbacks );

        Vector2Int kickbackAdjustPos = Vector2Int.zero;

        bool isSuccess = true;
        if( !IsValidPiecePos( PiecePos, clonePiece ) )
        {
            for( int i = 0; i < kickbacks.Count; ++i )
            {
                if( IsValidPiecePos( PiecePos + kickbacks[i], clonePiece ) )
                {
                    kickbackAdjustPos = kickbacks[i];
                    break;
                }

                if( i == kickbacks.Count - 1)
                {
                    isSuccess = false;
                }
            }
        }
        
        // ��]�ł��܂���ł���
        if( !isSuccess )
        {
            return false;
        }

        bool prevIsGround = IsGround;

        // �����܂ŗ��ď��߂ĉ�]�m�F����ꂽ�̂ŉ�]
        // �K�v�ɉ����ăL�b�N�o�b�N�̈ړ����s��
        HavePiece.Rotate( rotDir, out kickbacks );
        PiecePos = PiecePos + kickbackAdjustPos;

        if ( prevIsGround )
        {
            UseInfinity(false);
        }
        CheckGroundOfSelfPiece();

        return true;
    }

    /// <summary>
    /// �C���t�B�j�e�B�g�p
    /// </summary>
    void UseInfinity( bool isMove )
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
        optionRemain.dropTime = CurrentOption.dropTime;

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
