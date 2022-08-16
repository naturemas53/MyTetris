using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecePartsBlock : MonoBehaviour
{
    /// <summary>
    /// ���ʒu�H
    /// </summary>
    [SerializeField]
    bool isAxisPos = false;
    
    [SerializeField]
    Image backGround;
    [SerializeField]
    Image radioButton;

    /// <summary>
    /// �I�𒆁H
    /// </summary>
    bool isSelect = false;

    /// <summary>
    /// ���ʒu����̃I�t�Z�b�g�ʒu
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
    /// �I���t���O��ݒ肵�܂�.
    /// </summary>
    public void SetSelectFlag( bool value )
    {
        isSelect = value;
        UpdateRadioButtonColor();
    }

    public void OnEnterMousePointer()
    {
        // TODO: ���L�҂Ƀ}�E�X�̑���󋵕����āA�K�v�ɉ����ăt���O�ς��Ƃ�
        SetSelectFlag( true );
    }

    /// <summary>
    /// ���W�I�{�^���i�I�ȓz�j�̐F��ς��܂�
    /// </summary>
    void UpdateRadioButtonColor()
    {
        Color color = (isSelect) ? Color.black : Color.white;
        radioButton.color = color;
    }
}
