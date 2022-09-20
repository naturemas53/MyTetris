using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    Piece targetPiece = null;

    List<ABlockView> useViews = null;

    // Start is called before the first frame update
    void Start()
    {
        useViews = new List<ABlockView>();
        useViews.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �\������s�[�X��ݒ肵�܂�
    /// </summary>
    public void SetViewPiece( Piece piece )
    {
        // �O�̃s�[�X���c���Ă���Ȃ�Еt������
        if( targetPiece != null )
        {
            targetPiece.OnRotateSelf.RemoveListener( UpdateBlockPosOfRotated );
            RecycleBlockViews();
            targetPiece = null;
        }

        if( piece == null ) 
        {
            // �\���������s�[�X�������Ƃ������ƂȂ̂ŁA�Еt���݂̂ŏI��.
            return;
        }

        targetPiece = piece;
        piece.OnRotateSelf.AddListener( UpdateBlockPosOfRotated );
        InitDispBlocks();
    }

    /// <summary>
    /// �u���b�NView��S�ĕԋp���܂�.
    /// </summary>
    void RecycleBlockViews()
    {
        foreach( ABlockView view in useViews )
        {
            BlockViewFlyweight.Instance.CacheView( view );
        }

        useViews.Clear();
    }

    /// <summary>
    /// �u���b�N�̕\���̏��������s���܂��B
    /// </summary>
    void InitDispBlocks()
    {
        for( int i = 0; i < targetPiece.Blocks.Count; ++i )
        {
            ABlockView addView = BlockViewFlyweight.Instance.GetView( targetPiece.Blocks[i].BLOCK_TYPE );
            addView.transform.SetParent(transform);
            addView.transform.localScale = Vector3.one;
            useViews.Add( addView );
        }

        UpdateBlockPosOfRotated();
    }

    /// <summary>
    /// �u���b�N�ʒu���X�V���܂��i��ɉ�]���Ɏg�p�j
    /// </summary>
    void UpdateBlockPosOfRotated()
    {
        Vector2 blockSize  = ABlockView.SIZE;
        // �㉺�t�ɓ����Ă��܂��̂ł���őΏ�...
        blockSize.y *= -1;

        List<Vector2Int> blockOffsets = new List<Vector2Int>( targetPiece.BlockOffsets );
        for( int i = 0; i < blockOffsets.Count; ++i  )
        {
            useViews[i].transform.localPosition = blockSize * blockOffsets[i];
        }
    }
}
