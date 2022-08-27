using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static CommonDefines;

public class PieceCommonParams : MonoBehaviour
{
    public struct Params
    {
        public Vector2Int initPos;
        public EShapeType shapeType;
        public string dataName;
    }

    public Params CurrentParam
    {
        get
        {
            return GetCurrentParams();
        }

        set
        {
            SetCurrentParam( value );
        }
    }

    [SerializeField]
    InputField dataNameInputer;

    Vector2IntInputer initPosInputer;
    Dropdown shapeSelector;
    // Start is called before the first frame update
    void Start()
    {
        initPosInputer = GetComponentInChildren<Vector2IntInputer>();
        shapeSelector = GetComponentInChildren<Dropdown>();

        shapeSelector.ClearOptions();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Clear();
        for( int i = (int)EShapeType.SHAPE_Z; i < (int)EShapeType.SHAPE_MAX; ++i )
        {
            EShapeType shapeType = (EShapeType)i;
            
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = shapeType.ToString();
            options.Add( option );
        }
        shapeSelector.AddOptions( options );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// パラメータを設定します
    /// </summary>
    void SetCurrentParam( Params param )
    {
        initPosInputer.SetVector2Int( param.initPos );
        shapeSelector.value = (int)param.shapeType - (int)EShapeType.SHAPE_Z;
        dataNameInputer.text = param.dataName;
    }

    /// <summary>
    /// 現在のパラメータを取得します
    /// </summary>
    /// <returns></returns>
    Params GetCurrentParams()
    {
        Params ret = new Params();

        initPosInputer.TryGetVector2Int( out ret.initPos );
        ret.shapeType = (EShapeType)(shapeSelector.value + (int)EShapeType.SHAPE_Z);
        ret.dataName = dataNameInputer.text;

        return ret;
    }
}
