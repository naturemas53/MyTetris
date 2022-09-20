using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPieceView : MonoBehaviour
{
    [SerializeField]
    PieceView pieceView;

    PieceControll pieceControll;

    Vector2Int prevPiecePos = Vector2Int.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="setControll"></param>
    public void Initialize( PieceControll setControll )
    {
        pieceControll = setControll;
        pieceControll.OnChangePiece.AddListener( FetchCurrentPiece );
    }

    // Update is called once per frame
    void Update()
    {
        if( pieceControll == null ) return;

        if( prevPiecePos != pieceControll.PiecePos )
        {
            UpdateSelfPos();
        }
    }

    /// <summary>
    /// �\���s�[�X��ݒ肵�܂�.
    /// </summary>
    void FetchCurrentPiece()
    {
        pieceView.SetViewPiece(pieceControll.HavePiece);
        UpdateSelfPos();
    }

    /// <summary>
    /// ���g�̈ʒu���X�V���܂�
    /// </summary>
    void UpdateSelfPos()
    {
        Vector2 blockSize = ABlockView.SIZE;
        // �㉺�t�ɓ����̂Ń}�C�i�X��...
        blockSize.y *= -1;

        Vector2Int currentPos = pieceControll.PiecePos;
        transform.localPosition = currentPos * blockSize;

        prevPiecePos = pieceControll.PiecePos;
    }
}
