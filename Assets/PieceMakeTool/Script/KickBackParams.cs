using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class KickBackParams : MonoBehaviour
{
    /// <summary>
    /// �L�b�N�o�b�N�m�F���s����.
    /// </summary>
    readonly int KICKBACK_COUNT = 4;
    /// <summary>
    /// ���x�����t�H�[�}�b�g
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
    /// �������1�t�����点�ď�����
    /// </summary>
    /// <returns></returns>
    IEnumerator CoLateStart()
    {
        yield return new WaitForEndOfFrame();

        paramInputer = GetComponentsInChildren<Vector2IntInputer>();
        int needCount = KICKBACK_COUNT * (int)ERotateDirection.MAX;

        if (paramInputer.Length < needCount)
        {
            Debug.LogError("�L�b�N�o�b�N�̃p�����[�^��������܂���");
            yield break;
        }

        SetParamLabels();
    }

    /// <summary>
    /// �eVector2Int�̃��x����ݒ�
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
    /// �㕔�^�u���I�����ꂽ�Ƃ��̃R�[���o�b�N
    /// </summary>
    /// <param name="prevRotate"></param>
    /// <param name="nextRotate"></param>
    public void OnChangePieceRotate( EPieceRotate prevRotate, EPieceRotate nextRotate )
    {

    }
}
