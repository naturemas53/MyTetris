using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CommonDefines;

public class RotateTab : MonoBehaviour
{
    // 各回転状態　の　タブ名
    static readonly Dictionary<EPieceRotate, string> TABNAME_MAP = new Dictionary<EPieceRotate, string>()
    { 
        {EPieceRotate.ZERO_O_CLOCK,  "0°"},
        {EPieceRotate.THREE_O_CLOCK, "90°"},
        {EPieceRotate.SIX_O_CLOCK,   "180°"},
        {EPieceRotate.NINE_O_CLOCK,  "270°"},
    };

    Toggle selfToggle = null;
    /// <summary>
    /// このタブが担当している回転状態
    /// </summary>
    public EPieceRotate HaveRotate { get; private set; } = EPieceRotate.None;
    RotateTabSet owner = null;
    Image backGround = null;

    // Start is called before the first frame update
    void Start()
    {
        selfToggle = GetComponent<Toggle>();
        backGround = GetComponentInChildren< Image >();

        UpdateBackGroundColor( selfToggle.isOn );
    }

    public void Initialize( RotateTabSet tabOwner, EPieceRotate setRotate )
    {
        owner = tabOwner;
        HaveRotate = setRotate;

        GetComponentInChildren<Text>().text = TABNAME_MAP[HaveRotate];
        selfToggle.onValueChanged.AddListener( OnValueChanged );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValueChanged( bool changedValue )
    {
        UpdateBackGroundColor( changedValue );
        if ( changedValue )
        {
            owner.OnSelectedRotateTab(HaveRotate);
        }
    }
    void UpdateBackGroundColor( bool isEnable )
    {
        backGround.color = (isEnable) ? new Color( 1.0f, 0.5f, 0.0f, 1.0f ) : Color.white;
    }
}
