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
    /// �ǂ̐F
    /// </summary>
    static readonly Color WALL_COLOR = Color.gray;

    public WallBlockView() : base( EBlockType.BLOCK_WALL )
    {
    }

    protected override void InitilaizeFromAwake()
    {
        // TODO : �e�N�X�`���ݒ�

        blockImage.color = WALL_COLOR;
    }

    protected override bool InitializeImpl()
    {
        // ���ɓ����͂Ȃ�
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
