using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreatePieceDataColumns")]
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
    public List<Vector2Int>[] blockOffsets = new List<Vector2Int>[CommonDefines.PIECE_ROTATE_NUM];
    /// <summary>
    /// �s�[�X�̃L�b�N�o�b�N�f�[�^
    /// </summary>
    public CommonDefines.KickBackSet[] kickBackChecks = new CommonDefines.KickBackSet[CommonDefines.PIECE_ROTATE_NUM];
    /// <summary>
    /// �����ʒu
    /// </summary>
    public Vector2Int initPos = Vector2Int.zero;
    /// <summary>
    /// �`��
    /// </summary>
    public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
}
