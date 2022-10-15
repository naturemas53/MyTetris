using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextView : MonoBehaviour
{
    /// <summary>
    /// ピース間をどれくらい空けるか
    /// </summary>
    static readonly float PIECE_SPACER = 10.0f;

    /// <summary>
    /// ピース1つ当たりの高さ定義
    /// MEMO : とりあえず、通常ミノに合わせてブロック2つ分としておきます
    /// </summary>
    static readonly float PIECE_HEIGHT = ABlockView.SIZE.y * 2;

    /// <summary>
    /// ネクスト1つ目の大きさ
    /// </summary>
    static readonly float PIECE_SCALE_TO_TOP_NEXT = 0.5f;

    /// <summary>
    /// ネクストの2つ目以降の大きさ
    /// </summary>
    static readonly float PIECE_SCALE_TO_SECONDRY_NEXT = 0.25f;

    /// <summary>
    /// 参照するピース生成機
    /// </summary>
    ANextPieceSet dispNextPieces = null;

    /// <summary>
    /// ピース表示の原本
    /// </summary>
    [SerializeField]
    PieceView originPieceView;

    /// <summary>
    /// ネクストの表示を行う親
    /// </summary>
    [SerializeField]
    Transform nextPieceParent;

    /// <summary>
    /// 表示するNext数
    /// </summary>
    uint dispNextNum = 1;

    /// <summary>
    /// 上記Next数のプロパティs
    /// </summary>
    public uint DispNum
    {
        get
        {
            return dispNextNum;
        }

        set
        {
            dispNextNum = value;

            if( dispNextPieces == null )
            {
                // ネクジェネがない内はどうしようもないので一旦パス
                return;
            }

            SetUpNextPieceViews();
            RefreshNextPieceViews();
        }
    }

    /// <summary>
    /// 生成済みのPieceView
    /// </summary>
    List<PieceView> pieceViewInstance = new List<PieceView>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 表示するNEXTを設定します.
    /// </summary>
    /// <param name="nextPieces"></param>
    public void SetDispNext( ANextPieceSet nextPieces )
    {
        if( dispNextPieces != null )
        {
            dispNextPieces.OnUpdateNext.RemoveListener( RefreshNextPieceViews );
        }

        this.dispNextPieces = nextPieces;
        dispNextPieces.OnUpdateNext.AddListener( RefreshNextPieceViews );

        SetUpNextPieceViews();
        RefreshNextPieceViews();
    }

    /// <summary>
    /// ネクスト表示のセットアップを行います.
    /// </summary>
    void SetUpNextPieceViews()
    {
        // まずは片付けから
        nextPieceParent.DetachChildren();
        foreach( PieceView view in pieceViewInstance )
        {
            Destroy( view.gameObject );
        }
        pieceViewInstance.Clear();

        // 続いてはNext表示 なんだけど

        if (dispNextNum <= 0)
        {
            // 表示しない設定ならここまで...
            // (NEXT取得時に配列コピーをしているので、この形で)
            return;
        }

        uint nextNum = dispNextPieces.CommonSetting.generateNum;

        // 希望表示数より、生成数の方が少なければ、そちらの数に合わせる
        if( nextNum < dispNextNum )
        {
            Debug.LogWarning( "NEXT生成数が希望表示数より少ないため、生成数に合わせて表示を行います。" );
            dispNextNum = nextNum;
        }

        Vector3 createPos = Vector3.zero;
        // 軸位置とかの関係でちょっと左や下にずらす
        createPos.x += (ABlockView.SIZE.x * PIECE_SCALE_TO_TOP_NEXT);
        createPos.y += -(ABlockView.SIZE.y * PIECE_SCALE_TO_TOP_NEXT);

        for ( int i = 0; i < dispNextNum; ++i )
        {
            Vector3 scale = Vector3.one * PIECE_SCALE_TO_TOP_NEXT;
            float pieceHeight = PIECE_HEIGHT * PIECE_SCALE_TO_TOP_NEXT;
            if( i > 0 )
            {
                scale = Vector3.one * PIECE_SCALE_TO_SECONDRY_NEXT;
                pieceHeight = PIECE_HEIGHT * PIECE_SCALE_TO_SECONDRY_NEXT;
            }

            PieceView createView = Instantiate( originPieceView, nextPieceParent );
            createView.transform.localPosition = createPos;
            createView.transform.localScale = scale;
            pieceViewInstance.Add( createView );

            createPos.y += ( pieceHeight + PIECE_SPACER ) * -1;
        }
    }

    /// <summary>
    /// 各PieceViewの更新を行います.
    /// </summary>
    public void RefreshNextPieceViews()
    {
        if( pieceViewInstance.Count <= 0 )
        {
            // NEXTは表示しない
            return;
        }

        List<Piece> pieces = dispNextPieces.NextPieces;

        for( int i = 0; i < pieceViewInstance.Count; ++i )
        {
            pieceViewInstance[i].SetViewPiece( pieces[i] );
        }
    }    
}
