using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class KickBackParams : MonoBehaviour
{
    /// <summary>
    /// キックバック確認を行う回数.
    /// </summary>
    readonly int KICKBACK_COUNT = 4;
    /// <summary>
    /// ラベル名フォーマット
    /// </summary>
    readonly string LABEL_FORMAT = "Check{0}-{1}";

    public Dictionary<EPieceRotate, KickBackSet> KickBackSetMap { get; private set; } = null;
    Vector2IntInputer[] paramInputer;

    EPieceRotate currentRotate = EPieceRotate.None;

    // Start is called before the first frame update
    void Start()
    {
        InitKickbackParam();

        StartCoroutine( CoLateStart() );
    }

    /// <summary>
    /// 諸事情で1フレ送らせて初期化
    /// </summary>
    /// <returns></returns>
    IEnumerator CoLateStart()
    {
        yield return new WaitForEndOfFrame();

        paramInputer = GetComponentsInChildren<Vector2IntInputer>();
        int needCount = KICKBACK_COUNT * (int)ERotateDirection.MAX;

        if (paramInputer.Length < needCount)
        {
            Debug.LogError("キックバックのパラメータ数が足りません");
            yield break;
        }

        SetParamLabels();
    }

    /// <summary>
    /// 各Vector2Intのラベルを設定
    /// </summary>
    void SetParamLabels()
    {
        for( int i = 0; i < paramInputer.Length; ++i )
        {
            ERotateDirection rotateDirection = (i % 2 == 0) ? ERotateDirection.LEFT : ERotateDirection.RIGHT;
            int checkCount = i / 2;

            string label = string.Format( LABEL_FORMAT, checkCount, rotateDirection.ToString() );
            paramInputer[i].SetLabel(label);
        }
    }

    /// <summary>
    /// キックバックパラメータの初期化.
    /// </summary>
    void InitKickbackParam()
    {
        KickBackSetMap = new Dictionary<EPieceRotate, KickBackSet>();

        for( int i =0; i < (int)EPieceRotate.MAX; ++i )
        {
            EPieceRotate rotate = (EPieceRotate)i;

            KickBackSet kickbackSet = new KickBackSet();
            for( int j = 0; j < KICKBACK_COUNT; ++j )
            {
                kickbackSet.data[ERotateDirection.LEFT ].Add( Vector2Int.zero );
                kickbackSet.data[ERotateDirection.RIGHT].Add( Vector2Int.zero );
            }

            KickBackSetMap.Add( rotate, kickbackSet );
        }
    }

    /// <summary>
    /// キックバックパラメータを設定します.
    /// </summary>
    public void SetKickbackParams(Dictionary<EPieceRotate, KickBackSet> setKickBack)
    {
        KickBackSetMap = setKickBack;
        SetKickbackParamFromRotate( currentRotate );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 上部タブが選択されたときのコールバック
    /// </summary>
    /// <param name="nextRotate"></param>
    public void OnChangePieceRotate( EPieceRotate nextRotate )
    {
        CacheCurrentParam();
        SetKickbackParamFromRotate( nextRotate );
        currentRotate = nextRotate;
    }

    void CacheCurrentParam()
    {
        if (currentRotate == EPieceRotate.None) return;

        KickBackSet kickBackSet = new KickBackSet();

        for (int i = 0; i < KICKBACK_COUNT; ++i)
        {
            int baseIdx = i * (int)ERotateDirection.MAX;

            Vector2Int leftparam = new Vector2Int();
            Vector2Int rightparam = new Vector2Int();

            paramInputer[baseIdx]    .TryGetVector2Int( out leftparam  );
            paramInputer[baseIdx + 1].TryGetVector2Int( out rightparam );

            kickBackSet.data[ERotateDirection.LEFT] .Add( leftparam  );
            kickBackSet.data[ERotateDirection.RIGHT].Add( rightparam );
        }

        KickBackSetMap[currentRotate] = kickBackSet;
    }

    /// <summary>
    /// 指定回転のキックバックパラメータを設定
    /// </summary>
    /// <param name="fetchRotate"></param>
    void SetKickbackParamFromRotate( EPieceRotate fetchRotate )
    {
        KickBackSet kickBackSet = KickBackSetMap[fetchRotate];
        
        for( int i = 0; i  < KICKBACK_COUNT; ++i )
        {
            int baseIdx = i * (int)ERotateDirection.MAX;
            paramInputer[baseIdx]    .SetVector2Int( kickBackSet.data[ERotateDirection.LEFT] [i] );
            paramInputer[baseIdx + 1].SetVector2Int( kickBackSet.data[ERotateDirection.RIGHT][i] );
        }
    }
}
