using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vector2IntInputer : MonoBehaviour
{
    InputField xPosInput = null;
    InputField yPosInput = null;

    Text labelText = null;

    // Start is called before the first frame update
    void Start()
    {
        xPosInput = GetInputField(0);
        yPosInput = GetInputField(1);

        // null�`�F�b�N
        if( xPosInput == null || yPosInput == null )
        {
            Debug.LogError( "InputField������Ɏ擾�ł��Ă��܂���B" );
        }

        // ���܂��@�N���X�̈Ӑ}�ɔ����Ă�֌W��A�{���͂��܂�NG
        Transform parent = transform.parent;
        Transform labelTransform = parent.GetChild(0);
        labelText = labelTransform.GetComponentInChildren< Text >();
    }

    InputField GetInputField(int childIdx)
    {
        Transform child = transform.GetChild( childIdx );
        return child.GetComponentInChildren<InputField>();
    }

    /// <summary>
    /// Vector2Int�̎擾���s���܂��B
    /// </summary>
    /// <param name="inputedValue"> ����Ɏ擾�o�����ꍇ�͂��̒l �����łȂ��ꍇ�͌Œ��zero������܂��B </param>
    /// <returns> �擾�ɐ�����true ���s��false��Ԃ��܂� </returns>
    public bool TryGetVector2Int( out Vector2Int inputedValue )
    {
        Vector2Int retValue = Vector2Int.zero;
        inputedValue = retValue;

        int xPos = 0;
        int yPos = 0;

        bool tryGetXPos = int.TryParse( xPosInput.text, out xPos );
        bool tryGetYPos = int.TryParse( yPosInput.text, out yPos );

        bool isSuccessGetVector = (tryGetXPos && tryGetYPos);
        if ( !isSuccessGetVector )
        {
            return false;
        }

        retValue.x = xPos;
        retValue.y = yPos;

        inputedValue = retValue;
        return true;
    }

    /// <summary>
    /// Vector2Int��ݒ肵�܂�
    /// </summary>
    /// <param name="setValue"></param>
    public void SetVector2Int( Vector2Int setValue )
    {
        xPosInput.text = setValue.x.ToString();
        yPosInput.text = setValue.y.ToString();
    }

    /// <summary>
    /// ���x�����ݒ�
    /// </summary>
    public void SetLabel( string labelString )
    {
        if (labelText == null) return;

        labelText.text = labelString;
    }
}
