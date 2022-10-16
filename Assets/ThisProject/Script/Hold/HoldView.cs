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
    [SerializeField]
    GameObject disablePanel;

    HoldCue refHold = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (refHold == null) return;

        bool isDisablePiece = !refHold.IsCanHold;

        if ( isDisablePiece != IsDispDisalblePanel() )
        {
            SwitchDispDisablePanel();
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="hold"></param>
    public void Initialize( HoldCue hold )
    {
        refHold = hold;
        refHold.OnExecutedHold.AddListener( RefreshHoldPiece );

        // ���ʒu����\�������Ă�A���Ȃ̂ŁA�ʒu���Y�����Ȃ��Ƃ�΂�
        // �i�{����PieceView�Œ������ׂ��ȋC������j
        Vector3 localPos = holdPieceView.transform.localPosition;
        //localPos.x -= ABlockView.SIZE.x * SCALE;
        localPos.y += ABlockView.SIZE.y * -SCALE;

        holdPieceView.transform.localPosition = localPos;
        holdPieceView.transform.localScale = Vector3.one * SCALE;

        // Hold�����@�̃p�l���ʒu�ݒ�
        localPos.x -= ABlockView.SIZE.x * SCALE;
        localPos.y -= ABlockView.SIZE.y * -SCALE;
        Vector2 size = ABlockView.SIZE * SCALE;
        size.x *= 4;
        size.y *= 2;

        disablePanel.transform.localPosition = localPos;
        RectTransform rectTransform = (RectTransform)disablePanel.transform;
        rectTransform.sizeDelta = size;

        // HACK: �߂�ǂ��̂�Update��Hold�̃O���[��Ԃ��X�V
        this.Update();
    }

    /// <summary>
    /// �z�[���h�s�[�X�̕\���X�V
    /// </summary>
    public void RefreshHoldPiece()
    {
        holdPieceView.SetViewPiece( refHold.HoldPiece );
    }

    /// <summary>
    /// Hold���Â�����p�l�����A�N�e�B�u�ɂ��Ă��邩
    /// </summary>
    /// <returns></returns>
    bool IsDispDisalblePanel()
    {
        return disablePanel.activeSelf;
    }

    /// <summary>
    /// �Â�����p�l���̃A�N�e�B�u�؂�ւ�
    /// </summary>
    void SwitchDispDisablePanel()
    {
        disablePanel.SetActive(!IsDispDisalblePanel());
    }
}
