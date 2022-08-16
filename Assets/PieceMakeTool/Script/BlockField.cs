using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class BlockField : MonoBehaviour
{
    /// <summary>
    /// 操作タイプ
    /// </summary>
    public enum EControllType
    {
        CONTROLL_NONE,

        CONTROLL_SELECT,
        CONTROLL_DISSELECT,

        CONTROLL_DISABLE_CONTROLL,
    }

    /// <summary>
    /// ピースのサイズ
    /// </summary>
    readonly Vector2Int SIZE = new Vector2Int(10, 10);
    /// <summary>
    /// 現在の操作状況
    /// </summary>
    public EControllType CurrentControll { get; private set; } = EControllType.CONTROLL_DISABLE_CONTROLL;
    /// <summary>
    /// ブロックのオフセット
    /// </summary>
    Dictionary<EPieceRotate, List<Vector2Int>> blockOffsets;
    /// <summary>
    /// 盤面のブロック
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
    /// ブロックの初期化
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
    /// すべての回転について初期化
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
        Debug.Log( "マウス上げた時のそれ" );
        CurrentControll = EControllType.CONTROLL_DISABLE_CONTROLL;
    }

    private void OnMouseUp()
    {
        OnReleaseMouseButton();
    }

    /// <summary>
    /// ブロックがクリックされたときのコールバック
    /// </summary>
    public void OnClickedBlock( PiecePartsBlock clickBlock )
    {
        bool isSelect = clickBlock.IsSelect;
        CurrentControll = (isSelect) ? EControllType.CONTROLL_DISSELECT : EControllType.CONTROLL_SELECT;
    }

    /// <summary>
    /// インデックスからVectro2Intを求めます.
    /// </summary>
    Vector2Int ConvVector2IntFromIdx( int idx )
    {
        int xPos = idx % SIZE.x;
        int yPos = idx / SIZE.y;

        return new Vector2Int( xPos, yPos );
    }
}
