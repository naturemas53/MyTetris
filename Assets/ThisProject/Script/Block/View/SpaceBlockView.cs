using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class SpaceBlockView : ABlockView
{
    public SpaceBlockView() : base( EBlockType.BLOCK_EMPTY )
    {
    }

    protected override void InitilaizeFromAwake()
    {
        // �󔒂�\���i�H�j����̂œ��ɂȂ�...
    }

    protected override bool InitializeImpl()
    {
        // �ނ���ǂ����悤�ˁi�H�j
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
