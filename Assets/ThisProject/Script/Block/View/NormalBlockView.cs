using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CommonDefines;

public class NormalBlockView : ABlockView
{
    static readonly Dictionary<EShapeType, Color> COLOR_MAP = new Dictionary<EShapeType, Color>()
    {
        { EShapeType.SHAPE_NONE, Color.white },
        { EShapeType.SHAPE_S, Color.green },
        { EShapeType.SHAPE_Z, Color.red },
        { EShapeType.SHAPE_J, Color.blue },
        { EShapeType.SHAPE_L, new Color( 1.0f, 0.65f, 0.0f ) }, // オレンジ　の定義がない
        { EShapeType.SHAPE_O, Color.yellow },
        { EShapeType.SHAPE_T, Color.magenta },
        { EShapeType.SHAPE_I, Color.cyan },
        { EShapeType.SHAPE_SPECIAL, Color.white },
        { EShapeType.SHAPE_MAX, Color.white },
    };

    [SerializeField]
    Image blockImage;

    public NormalBlockView() : base( EBlockType.BLOCK_NORMAL )
    {
    }

    protected override void InitilaizeFromAwake()
    {
        // TODO: テクスチャ設定

        // ブロックによって設定色が変わるので
        // ここでは色設定を行わない
    }

    protected override bool InitializeImpl()
    {
        NormalBlock block = GetSelfBlockOfCasted< NormalBlock >();
        blockImage.color = COLOR_MAP[ block.ShapeType ];

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
