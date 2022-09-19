using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    Field refField = null;

    ABlockView[] allViews = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// フィールドビューの初期化
    /// </summary>
    /// <param name="field"></param>
    public void Initalize( Field field )
    {
        refField = field;
        int blockNum = refField.SIZE.x * refField.SIZE.y;
        allViews = new ABlockView[ blockNum ];

        SettingSelfSize();
        RefreshAllBlocks();
    }

    /// <summary>
    /// 自身のUIの大きさを設定
    /// </summary>
    void SettingSelfSize()
    {
        Vector2 blockSize = ABlockView.SIZE;
        Vector2 selfSize = new Vector2();
        selfSize.x = (float)refField.SIZE.x * blockSize.x;
        selfSize.y = (float)refField.SIZE.y * blockSize.y;

        RectTransform selfTransform = (RectTransform)transform;
        selfTransform.sizeDelta = selfSize;
    }

    /// <summary>
    /// 全ブロックの表示をリフレッシュします
    /// </summary>
    void RefreshAllBlocks()
    {
        for( int i = 0; i < allViews.Length; ++i )
        {
            Vector2Int blockPos = refField.ConvIndexToVector2Int(i);
            AddBlockViewToSelf( blockPos );
        }
    }

    /// <summary>
    /// 変更があった個所のブロックを入れ替えます
    /// </summary>
    void RefreshBlockFromPositions( List<Vector2Int> changeBlockPosies )
    {
        foreach( Vector2Int blockPos in changeBlockPosies )
        {
            RecycleBlockViewFromSelf( blockPos );
            AddBlockViewToSelf( blockPos );
        }
    }

    /// <summary>
    /// UI上でのブロック表示位置を取得
    /// </summary>
    /// <returns></returns>
    Vector2 GetBlockPosToUI( Vector2Int blockPos )
    {
        Vector2 size = ABlockView.SIZE;
        return new Vector2( blockPos.x * size.x, -blockPos.y * size.y ); 
    }

    /// <summary>
    /// BlockViewを追加します
    /// </summary>
    void AddBlockViewToSelf( Vector2Int blockPos )
    {
        int targetIdx = refField.ConvVector2IntToIndex(blockPos);
        ABlock block = refField.GetBlock(blockPos);
        allViews[targetIdx] = BlockViewFlyweight.Instance.GetView( block.BLOCK_TYPE );
        bool isInitialized = allViews[targetIdx].Initialize(block);
        if (!isInitialized)
        {
            Debug.LogError("BlockViewの初期化に失敗しました");
            RecycleBlockViewFromSelf(blockPos);
            return;
        }

        // ブロックの親子付け　＆　配置
        RectTransform blockTransform = (RectTransform)allViews[targetIdx].transform;

        blockTransform.SetParent( transform );
        blockTransform.localPosition = GetBlockPosToUI( blockPos );
        blockTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// 自身が持っているBlockViewを返却します
    /// </summary>
    void RecycleBlockViewFromSelf(Vector2Int blockPos)
    {
        int targetIdx = refField.ConvVector2IntToIndex(blockPos);
        if(  allViews[targetIdx] == null )
        {
            Debug.LogError("何もないやつを返却しようとしています...");
            return;
        }

        BlockViewFlyweight.Instance.CacheView(allViews[targetIdx]);
        allViews[targetIdx] = null;
    }
}
