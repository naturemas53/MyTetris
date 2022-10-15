using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ANextPieceSet
{
    public UnityEvent OnUpdateNext { get; private set; } = new UnityEvent();

    protected List<Piece> nextPieces;

    public List<Piece> NextPieces
    {
        get
        {
            return new List<Piece>(this.nextPieces);
        }
    }

    /// <summary>
    /// 全ネクスト共通オプション
    /// </summary>
    public struct CommonOption
    {
        public uint generateNum; // 生成するNEXT数
    }

    CommonOption commonSetting = new CommonOption();

    public CommonOption CommonSetting
    {
        get { return commonSetting; }
        set
        {
            if (value.generateNum <= 0)
            {
                Debug.LogWarning("諸事情により、必ず1以上は生成してください... 生成数を1にします。");
                value.generateNum = 1;
            }

            bool isNeedReGenerate = (value.generateNum != CommonSetting.generateNum);

            commonSetting = value;

            if (isNeedReGenerate)
            {
                Debug.LogWarning("生成数が変更されたため、NEXTの再生成を行います。");
                GenerateNext();
            }
        }
    }

    public ANextPieceSet()
    {
        nextPieces = new List<Piece>();

        CommonOption firstOption = new CommonOption();
        firstOption.generateNum = 1;
        commonSetting = firstOption;
    }

    /// <summary>
    /// ツモを生成します.
    /// </summary>
    public void GenerateNext()
    {
        nextPieces.Clear();

        for ( int i = 0; i < CommonSetting.generateNum; ++i )
        {
            AddNextPieceImpl();
        }

        OnUpdateNext.Invoke();
    }

    /// <summary>
    /// 先頭のピースを取得します
    /// </summary>
    /// <param name="isPop"> これをtrueにした場合、完全にNextから取り出し、次のピースを生成します </param>
    /// <returns></returns>
    public Piece GetTopPiece( bool isPop = false )
    {
        Piece topPiece = nextPieces[0];

        if( isPop )
        {
            PopTopPiece();
            AddNextPieceImpl();

            OnUpdateNext.Invoke();
        }

        return topPiece;
    }

    /// <summary>
    /// 生成中のピースをすべて返します（ネク順いじられたくないのでコピーで）
    /// </summary>
    /// <returns></returns>
    public List<Piece> GetAllPiece()
    {
        return new List<Piece>( nextPieces );
    }

    /// <summary>
    /// 先頭のピースを消し、後続を詰めます.
    /// </summary>
    void PopTopPiece()
    {
        nextPieces.RemoveAt( 0 );
    }

    /// <summary>
    /// 次ピース追加の処理派生用.
    /// </summary>
    protected abstract void AddNextPieceImpl();
}
