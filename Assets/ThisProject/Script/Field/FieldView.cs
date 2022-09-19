using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    Field refField = null;

    ABlockView[] allViews = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �t�B�[���h�r���[�̏�����
    /// </summary>
    /// <param name="field"></param>
    public void Initalize( Field field )
    {
        refField = field;
        int blockNum = refField.SIZE.x * refField.SIZE.y;
        allViews = new ABlockView[ blockNum ];

        SettingSelfSize();
        RefreshAllBlocks();
    }

    /// <summary>
    /// ���g��UI�̑傫����ݒ�
    /// </summary>
    void SettingSelfSize()
    {
        Vector2 blockSize = ABlockView.SIZE;
        Vector2 selfSize = new Vector2();
        selfSize.x = (float)refField.SIZE.x * blockSize.x;
        selfSize.y = (float)refField.SIZE.y * blockSize.y;

        RectTransform selfTransform = (RectTransform)transform;
        selfTransform.sizeDelta = selfSize;
    }

    /// <summary>
    /// �S�u���b�N�̕\�������t���b�V�����܂�
    /// </summary>
    void RefreshAllBlocks()
    {
        for( int i = 0; i < allViews.Length; ++i )
        {
            Vector2Int blockPos = refField.ConvIndexToVector2Int(i);
            AddBlockViewToSelf( blockPos );
        }
    }

    /// <summary>
    /// �ύX�����������̃u���b�N�����ւ��܂�
    /// </summary>
    void RefreshBlockFromPositions( List<Vector2Int> changeBlockPosies )
    {
        foreach( Vector2Int blockPos in changeBlockPosies )
        {
            RecycleBlockViewFromSelf( blockPos );
            AddBlockViewToSelf( blockPos );
        }
    }

    /// <summary>
    /// UI��ł̃u���b�N�\���ʒu���擾
    /// </summary>
    /// <returns></returns>
    Vector2 GetBlockPosToUI( Vector2Int blockPos )
    {
        Vector2 size = ABlockView.SIZE;
        return new Vector2( blockPos.x * size.x, -blockPos.y * size.y ); 
    }

    /// <summary>
    /// BlockView��ǉ����܂�
    /// </summary>
    void AddBlockViewToSelf( Vector2Int blockPos )
    {
        int targetIdx = refField.ConvVector2IntToIndex(blockPos);
        ABlock block = refField.GetBlock(blockPos);
        allViews[targetIdx] = BlockViewFlyweight.Instance.GetView( block.BLOCK_TYPE );
        bool isInitialized = allViews[targetIdx].Initialize(block);
        if (!isInitialized)
        {
            Debug.LogError("BlockView�̏������Ɏ��s���܂���");
            RecycleBlockViewFromSelf(blockPos);
            return;
        }

        // �u���b�N�̐e�q�t���@���@�z�u
        RectTransform blockTransform = (RectTransform)allViews[targetIdx].transform;

        blockTransform.SetParent( transform );
        blockTransform.localPosition = GetBlockPosToUI( blockPos );
        blockTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// ���g�������Ă���BlockView��ԋp���܂�
    /// </summary>
    void RecycleBlockViewFromSelf(Vector2Int blockPos)
    {
        int targetIdx = refField.ConvVector2IntToIndex(blockPos);
        if(  allViews[targetIdx] == null )
        {
            Debug.LogError("�����Ȃ����ԋp���悤�Ƃ��Ă��܂�...");
            return;
        }

        BlockViewFlyweight.Instance.CacheView(allViews[targetIdx]);
        allViews[targetIdx] = null;
    }
}
