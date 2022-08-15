using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

public class PieceDataColumns : ScriptableObject
{
    // ���PieceParam�Ɠ���
    // �������A
    // 1. �u���b�N�͐������ł���Ă��炤�̂Ŗ���
    // 2. ����ɋ�ʗp�̃f�[�^���@�I�Ȃ̂�����

    /// <summary>
    /// �f�[�^��
    /// </summary>
    public string selfDataName = "";
    /// <summary>
    /// ���ʒu����̃u���b�N�ʒu
    /// </summary>
    public Dictionary<EPieceRotate, List<Vector2Int>> blockOffSets;
    /// <summary>
    /// �s�[�X�̃L�b�N�o�b�N�f�[�^
    /// </summary>
    public Dictionary<EPieceRotate, KickBackSet> kickBacks;
    /// <summary>
    /// �����ʒu
    /// </summary>
    public Vector2Int initPos = Vector2Int.zero;
    /// <summary>
    /// �`��
    /// </summary>
    public EShapeType shapeType = EShapeType.SHAPE_NONE;

    public PieceDataColumns()
    {
        blockOffSets = new Dictionary<EPieceRotate, List<Vector2Int>>();
        kickBacks    = new Dictionary<EPieceRotate, KickBackSet>();

        for (int i = 0; i < PIECE_ROTATE_NUM; ++i)
        {
            blockOffSets.Add((EPieceRotate)i, new List<Vector2Int>());
            kickBacks.Add((EPieceRotate)i, new KickBackSet());
        }
    }
}
