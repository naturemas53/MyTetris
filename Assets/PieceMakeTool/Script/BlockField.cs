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
    Dictionary<EPieceRotate, List<Vector2Int>> blockOffsets;
    /// <summary>
    /// �Ֆʂ̃u���b�N
    /// </summary>
    PiecePartsBlock[] blocks;

    // Start is called before the first frame update
    void Start()
    {
        blockOffsets = new Dictionary<EPieceRotate, List<Vector2Int>>();
        for( int i = 0; i < (int)EPieceRotate.MAX; ++i )
        {
            blockOffsets.Add( (EPieceRotate)i, new List<Vector2Int>() );
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
        foreach( var offsets in blockOffsets )
        {
            offsets.Value.Clear();
        }
    }

    public void OnReleaseMouseButton()
    {
        Debug.Log( "�}�E�X�グ�����̂���" );
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
    /// �C���f�b�N�X����Vectro2Int�����߂܂�.
    /// </summary>
    Vector2Int ConvVector2IntFromIdx( int idx )
    {
        int xPos = idx % SIZE.x;
        int yPos = idx / SIZE.y;

        return new Vector2Int( xPos, yPos );
    }
}
