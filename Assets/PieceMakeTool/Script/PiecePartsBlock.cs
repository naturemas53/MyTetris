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
    /// <summary>
    /// �u���b�N���L��
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
    /// �I���t���O��ݒ肵�܂�.
    /// </summary>
    public void SetSelectFlag( bool value )
    {
        isSelect = value;
        UpdateRadioButtonColor();
    }

    /// <summary>
    /// ����󋵂���I���󋵂�ύX���܂�
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
    /// ���W�I�{�^���i�I�ȓz�j�̐F��ς��܂�
    /// </summary>
    void UpdateRadioButtonColor()
    {
        Color color = (isSelect) ? Color.black : Color.white;
        radioButton.color = color;
    }
}
