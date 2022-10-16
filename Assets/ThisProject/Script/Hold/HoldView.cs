using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldView : MonoBehaviour
{
    /// <summary>
    /// �z�[���h�r���[�̑傫��
    /// </summary>
    static readonly float SCALE = 0.7f;

    [SerializeField]
    PieceView holdPieceView;

    HoldCue refHold = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize( HoldCue hold )
    {
        refHold = hold;
        refHold.OnExecutedHold.AddListener( RefreshHoldPiece );

        // ���ʒu����\�������Ă�A���Ȃ̂ŁA�ʒu���Y�����Ȃ��Ƃ�΂�
        // �i�{����PieceView�Œ������ׂ��ȋC������j
        Vector3 localPos = holdPieceView.transform.localPosition;
        localPos.x += ABlockView.SIZE.x * SCALE;
        localPos.y += ABlockView.SIZE.y * -SCALE;

        holdPieceView.transform.localPosition = localPos;
        holdPieceView.transform.localScale = Vector3.one * SCALE;
    }

    public void RefreshHoldPiece()
    {
        holdPieceView.SetViewPiece( refHold.HoldPiece );
    }
}
