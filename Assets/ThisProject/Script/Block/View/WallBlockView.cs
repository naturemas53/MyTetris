using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CommonDefines;

public class WallBlockView : ABlockView
{
    [SerializeField]
    Image blockImage;

    /// <summary>
    /// 壁の色
    /// </summary>
    static readonly Color WALL_COLOR = Color.gray;

    public WallBlockView() : base( EBlockType.BLOCK_WALL )
    {
    }

    protected override void InitilaizeFromAwake()
    {
        // TODO : テクスチャ設定

        blockImage.color = WALL_COLOR;
    }

    protected override bool InitializeImpl()
    {
        // 特に動きはない
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
