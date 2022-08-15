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

    Dictionary<EPieceRotate, KickBackSet> kickBackSetMap;
    Vector2IntInputer[] paramInputer;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 上部タブが選択されたときのコールバック
    /// </summary>
    /// <param name="prevRotate"></param>
    /// <param name="nextRotate"></param>
    public void OnChangePieceRotate( EPieceRotate prevRotate, EPieceRotate nextRotate )
    {

    }
}
