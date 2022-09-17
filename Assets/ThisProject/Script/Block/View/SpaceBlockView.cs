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
        // 空白を表示（？）するので特にない...
    }

    protected override bool InitializeImpl()
    {
        // むしろどうしようね（？）
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
