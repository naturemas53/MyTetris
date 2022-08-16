using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecePartsBlock : MonoBehaviour
{
    /// <summary>
    /// 軸位置？
    /// </summary>
    [SerializeField]
    bool isAxisPos = false;
    
    [SerializeField]
    Image backGround;
    [SerializeField]
    Image radioButton;

    /// <summary>
    /// 選択中？
    /// </summary>
    bool isSelect = false;

    /// <summary>
    /// 軸位置からのオフセット位置
    /// </summary>
    [System.NonSerialized]
    public Vector2Int selfOffestPos = Vector2Int.zero;
    /// <summary>
    /// ブロック所有者
    /// </summary>
    [System.NonSerialized]
    public BlockField owner = null;

    public bool IsAxisPos
    {
        get { return isAxisPos; }
    }
    
    public bool IsSelect
    {
        get { return isSelect; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Color bgColor = (isAxisPos) ? Color.yellow : Color.white;
        backGround.color = bgColor;
        UpdateRadioButtonColor();
    }

    /// <summary>
    /// 選択フラグを設定します.
    /// </summary>
    public void SetSelectFlag( bool value )
    {
        isSelect = value;
        UpdateRadioButtonColor();
    }

    /// <summary>
    /// 操作状況から選択状況を変更します
    /// </summary>
    void SetSelectFlagFromControllType( BlockField.EControllType eControllType )
    {
        if (eControllType == BlockField.EControllType.CONTROLL_DISABLE_CONTROLL) return;

        bool isSelect = (eControllType == BlockField.EControllType.CONTROLL_SELECT);
        SetSelectFlag( isSelect );
    }

    public void OnEnterMousePointer()
    {
        SetSelectFlagFromControllType( owner.CurrentControll );
    }

    public void OnBlockClick()
    {
        owner.OnClickedBlock( this );
        SetSelectFlagFromControllType(owner.CurrentControll);
    }

    private void OnMouseDown()
    {
        OnBlockClick();
    }

    private void OnMouseEnter()
    {
        OnEnterMousePointer();
    }

    public void OnMousePointerUp()
    {
        owner.OnReleaseMouseButton();
    }

    /// <summary>
    /// ラジオボタン（的な奴）の色を変えます
    /// </summary>
    void UpdateRadioButtonColor()
    {
        Color color = (isSelect) ? Color.black : Color.white;
        radioButton.color = color;
    }
}
