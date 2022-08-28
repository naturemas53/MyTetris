using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class BlockField : MonoBehaviour
{
    /// <summary>
    /// ����^�C�v
    /// </summary>
    public enum EControllType
    {
        CONTROLL_NONE,

        CONTROLL_SELECT,
        CONTROLL_DISSELECT,

        CONTROLL_DISABLE_CONTROLL,
    }

    /// <summary>
    /// �s�[�X�̃T�C�Y
    /// </summary>
    readonly Vector2Int SIZE = new Vector2Int(10, 10);
    /// <summary>
    /// ���݂̑����
    /// </summary>
    public EControllType CurrentControll { get; private set; } = EControllType.CONTROLL_DISABLE_CONTROLL;
    /// <summary>
    /// �u���b�N�̃I�t�Z�b�g
    /// </summary>
    public Dictionary<EPieceRotate, List<Vector2Int>> BlockOffsetMap { get; private set; } = null;
    /// <summary>
    /// �Ֆʂ̃u���b�N
    /// </summary>
    PiecePartsBlock[] blocks;
    /// <summary>
    /// ���݂̉�]���
    /// </summary>
    EPieceRotate currentRotate = EPieceRotate.None;

    // Start is called before the first frame update
    void Start()
    {
        BlockOffsetMap = new Dictionary<EPieceRotate, List<Vector2Int>>();
        for( int i = 0; i < (int)EPieceRotate.MAX; ++i )
        {
            BlockOffsetMap.Add( (EPieceRotate)i, new List<Vector2Int>() );
        }

        InitializeToAllRotatePiece();

        blocks = GetComponentsInChildren<PiecePartsBlock>();
        InitializeBlock();
    }

    /// <summary>
    /// �u���b�N�̏�����
    /// </summary>
    void InitializeBlock()
    {
        Vector2Int axisPos = Vector2Int.zero;
        for( int i = 0; i < blocks.Length; ++i )
        {
            if( blocks[i].IsAxisPos )
            {
                axisPos = ConvVector2IntFromIdx( i );
                break;
            }
        }

        for (int i = 0; i < blocks.Length; ++i)
        {
            Vector2Int blockPos = ConvVector2IntFromIdx( i );
            blocks[i].selfOffestPos = blockPos - axisPos;
            blocks[i].owner = this;
        }
    }

    /// <summary>
    /// ���ׂẲ�]�ɂ��ď�����
    /// </summary>
    void InitializeToAllRotatePiece()
    {
        foreach( var offsets in BlockOffsetMap )
        {
            offsets.Value.Clear();
        }
    }

    public void OnReleaseMouseButton()
    {
        CurrentControll = EControllType.CONTROLL_DISABLE_CONTROLL;
    }

    private void OnMouseUp()
    {
        OnReleaseMouseButton();
    }

    /// <summary>
    /// �u���b�N���N���b�N���ꂽ�Ƃ��̃R�[���o�b�N
    /// </summary>
    public void OnClickedBlock( PiecePartsBlock clickBlock )
    {
        bool isSelect = clickBlock.IsSelect;
        CurrentControll = (isSelect) ? EControllType.CONTROLL_DISSELECT : EControllType.CONTROLL_SELECT;
    }

    /// <summary>
    /// �㕔�^�u���I�����ꂽ�Ƃ��̃R�[���o�b�N
    /// </summary>
    /// <param name="nextRotate"></param>
    public void OnChangePieceRotate(EPieceRotate nextRotate)
    {
        CacheCurrentParam();
        SetBlocksFromRotate( nextRotate );
        currentRotate = nextRotate;
    }

    /// <summary>
    /// ���݂̒l��ێ����܂�
    /// </summary>
    void CacheCurrentParam()
    {
        if (currentRotate == EPieceRotate.None) return;

        List<Vector2Int> blockOffsets = new List<Vector2Int>();
        blockOffsets.Clear();

        foreach( var block in blocks )
        {
            if( block.IsSelect )
            {
                blockOffsets.Add( block.selfOffestPos );
            }
        }

        BlockOffsetMap[currentRotate] = blockOffsets;
    }

    /// <summary>
    /// �w���]�̃u���b�N��ݒ�
    /// </summary>
    /// <param name="fetchRotate"></param>
    void SetBlocksFromRotate(EPieceRotate fetchRotate)
    {
        foreach( var block in blocks )
        {
            bool select = BlockOffsetMap[fetchRotate].Contains(block.selfOffestPos);
            block.SetSelectFlag(select);
        }
    }

    /// <summary>
    /// �C���f�b�N�X����Vectro2Int�����߂܂�.
    /// </summary>
    Vector2Int ConvVector2IntFromIdx( int idx )
    {
        int xPos = idx % SIZE.x;
        int yPos = idx / SIZE.y;

        return new Vector2Int( xPos, yPos );
    }
}
