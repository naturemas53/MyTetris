using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class BlockViewFlyweight : SingletonMonoBehaviour<BlockViewFlyweight>
{
    [System.Serializable]
    class OriginalObjects
    {
        public EBlockType blockType = EBlockType.BLOCK_NONE;
        public ABlockView originalObject = null;
    }

    [SerializeField]
    List<OriginalObjects> originalViews;

    Dictionary<EBlockType, List<ABlockView> > nonUseView;

    protected override void Initialize()
    {
        nonUseView = new Dictionary<EBlockType, List<ABlockView> >();
    }

    /// <summary>
    /// �u���b�NUI���擾���܂�
    /// </summary>
    /// <returns></returns>
    public ABlockView GetView( EBlockType needType )
    {
        ABlockView retView = null;

        if( !nonUseView.ContainsKey( needType ) ) CreateNewBlockViewCaches( needType );

        if( nonUseView[needType].Count > 0 )
        {
            retView = nonUseView[needType][0];
            nonUseView[needType].RemoveAt(0);

            retView.gameObject.SetActive( true );
        }
        else
        {
            // �������Ȃ�A�V��������ĕԂ�.
            retView = CreateView( needType );
        }

        return retView;
    }

    /// <summary>
    /// �g���I�����View��ێ����܂�
    /// </summary>
    public void CacheView( ABlockView view )
    {
        view.gameObject.SetActive( false );
        // �ۊǗp�̏ꏊ�Ɉꉞ�ŕR�Â�����
        view.transform.SetParent( transform );

        if (!nonUseView.ContainsKey(view.USABLE_TYPE)) CreateNewBlockViewCaches(view.USABLE_TYPE);

        nonUseView[view.USABLE_TYPE].Add( view );
    }

    /// <summary>
    /// View�L���b�V����V���ɍ��܂�
    /// </summary>
    void CreateNewBlockViewCaches( EBlockType type )
    {
        nonUseView.Add( type, new List<ABlockView>() );
        nonUseView[type].Clear();
    }

    /// <summary>
    /// View�I�u�W�F�N�g����
    /// </summary>
    ABlockView CreateView( EBlockType targetType )
    {
        ABlockView origin = originalViews.Find( data => data.blockType == targetType ).originalObject;
        return Instantiate( origin.gameObject ).GetComponent<ABlockView>();
    }
}
