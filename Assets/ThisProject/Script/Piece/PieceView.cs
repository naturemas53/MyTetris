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
    /// 表示するピースを設定します
    /// </summary>
    public void SetViewPiece( Piece piece )
    {
        // 前のピースが残っているなら片付け処理
        if( targetPiece != null )
        {
            targetPiece.OnRotateSelf.RemoveListener( UpdateBlockPosOfRotated );
            RecycleBlockViews();
            targetPiece = null;
        }

        if( piece == null ) 
        {
            // 表示したいピースが無いということなので、片付けのみで終了.
            return;
        }

        targetPiece = piece;
        piece.OnRotateSelf.AddListener( UpdateBlockPosOfRotated );
        InitDispBlocks();
    }

    /// <summary>
    /// ブロックViewを全て返却します.
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
    /// ブロックの表示の初期化を行います。
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
    /// ブロック位置を更新します（主に回転時に使用）
    /// </summary>
    void UpdateBlockPosOfRotated()
    {
        Vector2 blockSize  = ABlockView.SIZE;
        // 上下逆に動いてしまうのでこれで対処...
        blockSize.y *= -1;

        List<Vector2Int> blockOffsets = new List<Vector2Int>( targetPiece.BlockOffsets );
        for( int i = 0; i < blockOffsets.Count; ++i  )
        {
            useViews[i].transform.localPosition = blockSize * blockOffsets[i];
        }
    }
}
