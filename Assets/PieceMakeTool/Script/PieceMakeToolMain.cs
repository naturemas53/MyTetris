using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    /// <summary>
    /// データ読み込み
    /// </summary>
    public void Inport()
    {
        Debug.Log("工事中...");
    }

    void OnChangedRotate( EPieceRotate nextRotate )
    {
        kickBackParams.OnChangePieceRotate( nextRotate );
        blocks.OnChangePieceRotate( nextRotate );
    }
}
