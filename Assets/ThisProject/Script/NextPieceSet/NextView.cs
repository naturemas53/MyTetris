using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextView : MonoBehaviour
{
    /// <summary>
    /// �s�[�X�Ԃ��ǂꂭ�炢�󂯂邩
    /// </summary>
    static readonly float PIECE_SPACER = 10.0f;

    /// <summary>
    /// �s�[�X1������̍�����`
    /// MEMO : �Ƃ肠�����A�ʏ�~�m�ɍ��킹�ău���b�N2���Ƃ��Ă����܂�
    /// </summary>
    static readonly float PIECE_HEIGHT = ABlockView.SIZE.y * 2;

    /// <summary>
    /// �l�N�X�g1�ڂ̑傫��
    /// </summary>
    static readonly float PIECE_SCALE_TO_TOP_NEXT = 0.5f;

    /// <summary>
    /// �l�N�X�g��2�ڈȍ~�̑傫��
    /// </summary>
    static readonly float PIECE_SCALE_TO_SECONDRY_NEXT = 0.25f;

    /// <summary>
    /// �Q�Ƃ���s�[�X�����@
    /// </summary>
    ANextPieceSet dispNextPieces = null;

    /// <summary>
    /// �s�[�X�\���̌��{
    /// </summary>
    [SerializeField]
    PieceView originPieceView;

    /// <summary>
    /// �l�N�X�g�̕\�����s���e
    /// </summary>
    [SerializeField]
    Transform nextPieceParent;

    /// <summary>
    /// �\������Next��
    /// </summary>
    uint dispNextNum = 1;

    /// <summary>
    /// ��LNext���̃v���p�e�Bs
    /// </summary>
    public uint DispNum
    {
        get
        {
            return dispNextNum;
        }

        set
        {
            dispNextNum = value;

            if( dispNextPieces == null )
            {
                // �l�N�W�F�l���Ȃ����͂ǂ����悤���Ȃ��̂ň�U�p�X
                return;
            }

            SetUpNextPieceViews();
            RefreshNextPieceViews();
        }
    }

    /// <summary>
    /// �����ς݂�PieceView
    /// </summary>
    List<PieceView> pieceViewInstance = new List<PieceView>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �\������NEXT��ݒ肵�܂�.
    /// </summary>
    /// <param name="nextPieces"></param>
    public void SetDispNext( ANextPieceSet nextPieces )
    {
        if( dispNextPieces != null )
        {
            dispNextPieces.OnUpdateNext.RemoveListener( RefreshNextPieceViews );
        }

        this.dispNextPieces = nextPieces;
        dispNextPieces.OnUpdateNext.AddListener( RefreshNextPieceViews );

        SetUpNextPieceViews();
        RefreshNextPieceViews();
    }

    /// <summary>
    /// �l�N�X�g�\���̃Z�b�g�A�b�v���s���܂�.
    /// </summary>
    void SetUpNextPieceViews()
    {
        // �܂��͕Еt������
        nextPieceParent.DetachChildren();
        foreach( PieceView view in pieceViewInstance )
        {
            Destroy( view.gameObject );
        }
        pieceViewInstance.Clear();

        // �����Ă�Next�\�� �Ȃ񂾂���

        if (dispNextNum <= 0)
        {
            // �\�����Ȃ��ݒ�Ȃ炱���܂�...
            // (NEXT�擾���ɔz��R�s�[�����Ă���̂ŁA���̌`��)
            return;
        }

        uint nextNum = dispNextPieces.CommonSetting.generateNum;

        // ��]�\�������A�������̕������Ȃ���΁A������̐��ɍ��킹��
        if( nextNum < dispNextNum )
        {
            Debug.LogWarning( "NEXT����������]�\������菭�Ȃ����߁A�������ɍ��킹�ĕ\�����s���܂��B" );
            dispNextNum = nextNum;
        }

        Vector3 createPos = Vector3.zero;
        // ���ʒu�Ƃ��̊֌W�ł�����ƍ��≺�ɂ��炷
        createPos.x += (ABlockView.SIZE.x * PIECE_SCALE_TO_TOP_NEXT);
        createPos.y += -(ABlockView.SIZE.y * PIECE_SCALE_TO_TOP_NEXT);

        for ( int i = 0; i < dispNextNum; ++i )
        {
            Vector3 scale = Vector3.one * PIECE_SCALE_TO_TOP_NEXT;
            float pieceHeight = PIECE_HEIGHT * PIECE_SCALE_TO_TOP_NEXT;
            if( i > 0 )
            {
                scale = Vector3.one * PIECE_SCALE_TO_SECONDRY_NEXT;
                pieceHeight = PIECE_HEIGHT * PIECE_SCALE_TO_SECONDRY_NEXT;
            }

            PieceView createView = Instantiate( originPieceView, nextPieceParent );
            createView.transform.localPosition = createPos;
            createView.transform.localScale = scale;
            pieceViewInstance.Add( createView );

            createPos.y += ( pieceHeight + PIECE_SPACER ) * -1;
        }
    }

    /// <summary>
    /// �ePieceView�̍X�V���s���܂�.
    /// </summary>
    public void RefreshNextPieceViews()
    {
        if( pieceViewInstance.Count <= 0 )
        {
            // NEXT�͕\�����Ȃ�
            return;
        }

        List<Piece> pieces = dispNextPieces.NextPieces;

        for( int i = 0; i < pieceViewInstance.Count; ++i )
        {
            pieceViewInstance[i].SetViewPiece( pieces[i] );
        }
    }    
}
