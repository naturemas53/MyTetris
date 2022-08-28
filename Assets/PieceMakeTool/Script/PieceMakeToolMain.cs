using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEditor;

using static CommonDefines;

public class PieceMakeToolMain : MonoBehaviour
{
    [SerializeField]
    string outputPath = "";
    [SerializeField]
    RotateTabSet rotateTab;
    [SerializeField]
    BlockField blocks;
    [SerializeField]
    KickBackParams kickBackParams;
    [SerializeField]
    PieceCommonParams commonParams;

    [SerializeField]
    GameObject guardTop;

    // Start is called before the first frame update
    void Start()
    {
        rotateTab.OnChangedRotate.AddListener( this.OnChangedRotate );
        StartCoroutine( CoInitLate() );
    }

    /// <summary>
    /// 少し遅らせて初期化
    /// </summary>
    /// <returns></returns>
    IEnumerator CoInitLate()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        rotateTab.SetSelectRotate( EPieceRotate.ZERO_O_CLOCK );

        PieceCommonParams.Params @params = new PieceCommonParams.Params();
        @params.shapeType = EShapeType.SHAPE_Z;
        commonParams.CurrentParam = @params;

        guardTop.SetActive(false) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// データ出力
    /// </summary>
    public void Export()
    {
#if UNITY_EDITOR
        var pieceColumus = new PieceDataColumns();

        pieceColumus.blockOffSets = blocks.BlockOffsetMap;
        pieceColumus.kickBacks = kickBackParams.KickBackSetMap;

        var commonOptions = commonParams.CurrentParam;
        pieceColumus.initPos = commonOptions.initPos;
        pieceColumus.shapeType = commonOptions.shapeType;
        pieceColumus.selfDataName = commonOptions.dataName;

        string outPathTotal = outputPath + pieceColumus.selfDataName + ".asset";
        AssetDatabase.CreateAsset( pieceColumus , outPathTotal);
        AssetDatabase.Refresh();
#else
#endif

    }

    /// <summary>
    /// データ読み込み
    /// </summary>
    public void Inport()
    {
        Debug.Log("工事中...");

        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Title = "生成済みのピースパラメータアセットを選択してください";
        ofd.ShowDialog();

        Debug.LogFormat( "SelectedPath:{0}", ofd.FileName );
    }

    void OnChangedRotate( EPieceRotate nextRotate )
    {
        kickBackParams.OnChangePieceRotate( nextRotate );
        blocks.OnChangePieceRotate( nextRotate );
    }
}
