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
    /// �����x�点�ď�����
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
    /// �f�[�^�o��
    /// </summary>
    public void Export()
    {

    }

    /// <summary>
    /// �f�[�^�ǂݍ���
    /// </summary>
    public void Inport()
    {
        Debug.Log("�H����...");
    }

    void OnChangedRotate( EPieceRotate nextRotate )
    {
        kickBackParams.OnChangePieceRotate( nextRotate );
        blocks.OnChangePieceRotate( nextRotate );
    }
}
