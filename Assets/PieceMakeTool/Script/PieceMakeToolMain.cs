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
    /// �����x�点�ď�����
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
    /// �f�[�^�o��
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
    /// �f�[�^�ǂݍ���
    /// </summary>
    public void Inport()
    {
        Debug.Log("�H����...");

        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Title = "�����ς݂̃s�[�X�p�����[�^�A�Z�b�g��I�����Ă�������";
        ofd.ShowDialog();

        Debug.LogFormat( "SelectedPath:{0}", ofd.FileName );
    }

    void OnChangedRotate( EPieceRotate nextRotate )
    {
        kickBackParams.OnChangePieceRotate( nextRotate );
        blocks.OnChangePieceRotate( nextRotate );
    }
}
