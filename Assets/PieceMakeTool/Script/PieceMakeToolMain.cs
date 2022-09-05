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
        return;
#if UNITY_EDITOR
        var pieceColumus = new PieceDataColumns();

        pieceColumus.blockOffSets = blocks.BlockOffsetMap;
        pieceColumus.kickBacks = kickBackParams.KickBackSetMap;

        var commonOptions = commonParams.CurrentParam;
        pieceColumus.initPos = commonOptions.initPos;
        pieceColumus.shapeType = commonOptions.shapeType;
        pieceColumus.selfDataName = commonOptions.dataName;

        string outPathTotal = outputPath + pieceColumus.selfDataName + ".asset";
#else
#endif

    }

    /// <summary>
    /// データ読み込み
    /// </summary>
    public void Inport()
    {
        Debug.Log("諸事情でもう一回調整中...");

        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "PieceParamFile (*.json)|*.json";
        ofd.Title = "生成済みのピースパラメータアセットを選択してください";
        DialogResult result =  ofd.ShowDialog();

        // それっぽい選択肢でなければPASS
        if( result != DialogResult.OK && result != DialogResult.Yes )
        {
            Debug.Log( "読み込みをキャンセルしました" );
            return;
        }

        PieceDataColumns pieceData;
        try
        {
            string selectedFilePath = ofd.FileName;
            int assetPathIdx = selectedFilePath.IndexOf("Assets\\");
            if (assetPathIdx < 0) throw new System.Exception();

            string filePathFromAssets = selectedFilePath.Substring( assetPathIdx );

            //pieceData = AssetDatabase.LoadAssetAtPath<PieceDataColumns>(filePathFromAssets);

            //if( pieceData == null )
            //{
            //    throw new System.Exception();
            //}
        }
        catch
        {
            Debug.Log("読み込みに失敗しました　関係ないファイルを選択しましたか？");
            return;
        }

        //blocks.SetBlockOffsets( pieceData.blockOffSets );
        //kickBackParams.SetKickbackParams( pieceData.kickBacks );

        //PieceCommonParams.Params @params = new PieceCommonParams.Params();
        //@params.dataName = pieceData.selfDataName;
        //@params.shapeType = pieceData.shapeType;
        //@params.initPos = pieceData.initPos;

        //commonParams.CurrentParam = @params;
    }

    void OnChangedRotate( EPieceRotate nextRotate )
    {
        kickBackParams.OnChangePieceRotate( nextRotate );
        blocks.OnChangePieceRotate( nextRotate );
    }
}
