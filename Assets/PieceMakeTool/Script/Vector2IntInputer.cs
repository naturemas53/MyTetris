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

        // nullチェック
        if( xPosInput == null || yPosInput == null )
        {
            Debug.LogError( "InputFieldが正常に取得できていません。" );
        }

        // おまけ　クラスの意図に反してる関係上、本来はあまりNG
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
    /// Vector2Intの取得を行います。
    /// </summary>
    /// <param name="inputedValue"> 正常に取得出来た場合はその値 そうでない場合は固定でzeroが入ります。 </param>
    /// <returns> 取得に成功でtrue 失敗でfalseを返します </returns>
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
    /// Vector2Intを設定します
    /// </summary>
    /// <param name="setValue"></param>
    public void SetVector2Int( Vector2Int setValue )
    {
        xPosInput.text = setValue.x.ToString();
        yPosInput.text = setValue.y.ToString();
    }

    /// <summary>
    /// ラベル名設定
    /// </summary>
    public void SetLabel( string labelString )
    {
        if (labelText == null) return;

        labelText.text = labelString;
    }
}
