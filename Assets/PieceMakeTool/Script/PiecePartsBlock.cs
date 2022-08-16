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

    public bool IsAxisPos
    {
        get { return isAxisPos; }
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

    public void OnEnterMousePointer()
    {
        // TODO: 所有者にマウスの操作状況聞いて、必要に応じてフラグ変えとく
        SetSelectFlag( true );
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
