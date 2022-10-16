using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldView : MonoBehaviour
{
    /// <summary>
    /// ホールドビューの大きさ
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
    /// 初期化
    /// </summary>
    /// <param name="hold"></param>
    public void Initialize( HoldCue hold )
    {
        refHold = hold;
        refHold.OnExecutedHold.AddListener( RefreshHoldPiece );

        // 軸位置から表示させてるアレなので、位置をズラさないとやばい
        // （本当はPieceViewで調整すべきな気がする）
        Vector3 localPos = holdPieceView.transform.localPosition;
        //localPos.x -= ABlockView.SIZE.x * SCALE;
        localPos.y += ABlockView.SIZE.y * -SCALE;

        holdPieceView.transform.localPosition = localPos;
        holdPieceView.transform.localScale = Vector3.one * SCALE;

        // Hold無効　のパネル位置設定
        localPos.x -= ABlockView.SIZE.x * SCALE;
        localPos.y -= ABlockView.SIZE.y * -SCALE;
        Vector2 size = ABlockView.SIZE * SCALE;
        size.x *= 4;
        size.y *= 2;

        disablePanel.transform.localPosition = localPos;
        RectTransform rectTransform = (RectTransform)disablePanel.transform;
        rectTransform.sizeDelta = size;

        // HACK: めんどいのでUpdateでHoldのグレー状態を更新
        this.Update();
    }

    /// <summary>
    /// ホールドピースの表示更新
    /// </summary>
    public void RefreshHoldPiece()
    {
        holdPieceView.SetViewPiece( refHold.HoldPiece );
    }

    /// <summary>
    /// Holdを暗くするパネルをアクティブにしているか
    /// </summary>
    /// <returns></returns>
    bool IsDispDisalblePanel()
    {
        return disablePanel.activeSelf;
    }

    /// <summary>
    /// 暗くするパネルのアクティブ切り替え
    /// </summary>
    void SwitchDispDisablePanel()
    {
        disablePanel.SetActive(!IsDispDisalblePanel());
    }
}
