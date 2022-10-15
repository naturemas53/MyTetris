using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ANextPieceSet
{
    public UnityEvent OnUpdateNext { get; private set; } = new UnityEvent();

    protected List<Piece> nextPieces;

    public List<Piece> NextPieces
    {
        get
        {
            return new List<Piece>(this.nextPieces);
        }
    }

    /// <summary>
    /// �S�l�N�X�g���ʃI�v�V����
    /// </summary>
    public struct CommonOption
    {
        public uint generateNum; // ��������NEXT��
    }

    CommonOption commonSetting = new CommonOption();

    public CommonOption CommonSetting
    {
        get { return commonSetting; }
        set
        {
            if (value.generateNum <= 0)
            {
                Debug.LogWarning("������ɂ��A�K��1�ȏ�͐������Ă�������... ��������1�ɂ��܂��B");
                value.generateNum = 1;
            }

            bool isNeedReGenerate = (value.generateNum != CommonSetting.generateNum);

            commonSetting = value;

            if (isNeedReGenerate)
            {
                Debug.LogWarning("���������ύX���ꂽ���߁ANEXT�̍Đ������s���܂��B");
                GenerateNext();
            }
        }
    }

    public ANextPieceSet()
    {
        nextPieces = new List<Piece>();

        CommonOption firstOption = new CommonOption();
        firstOption.generateNum = 1;
        commonSetting = firstOption;
    }

    /// <summary>
    /// �c���𐶐����܂�.
    /// </summary>
    public void GenerateNext()
    {
        nextPieces.Clear();

        for ( int i = 0; i < CommonSetting.generateNum; ++i )
        {
            AddNextPieceImpl();
        }

        OnUpdateNext.Invoke();
    }

    /// <summary>
    /// �擪�̃s�[�X���擾���܂�
    /// </summary>
    /// <param name="isPop"> �����true�ɂ����ꍇ�A���S��Next������o���A���̃s�[�X�𐶐����܂� </param>
    /// <returns></returns>
    public Piece GetTopPiece( bool isPop = false )
    {
        Piece topPiece = nextPieces[0];

        if( isPop )
        {
            PopTopPiece();
            AddNextPieceImpl();

            OnUpdateNext.Invoke();
        }

        return topPiece;
    }

    /// <summary>
    /// �������̃s�[�X�����ׂĕԂ��܂��i�l�N��������ꂽ���Ȃ��̂ŃR�s�[�Łj
    /// </summary>
    /// <returns></returns>
    public List<Piece> GetAllPiece()
    {
        return new List<Piece>( nextPieces );
    }

    /// <summary>
    /// �擪�̃s�[�X�������A�㑱���l�߂܂�.
    /// </summary>
    void PopTopPiece()
    {
        nextPieces.RemoveAt( 0 );
    }

    /// <summary>
    /// ���s�[�X�ǉ��̏����h���p.
    /// </summary>
    protected abstract void AddNextPieceImpl();
}
