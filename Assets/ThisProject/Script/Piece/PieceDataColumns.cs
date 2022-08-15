using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreatePieceDataColumns")]
public class PieceDataColumns : ScriptableObject
{
    // 大体PieceParamと同じ
    // ただし、
    // 1. ブロックは生成側でやってもらうので無し
    // 2. 代わりに区別用のデータ名　的なのがある

    /// <summary>
    /// データ名
    /// </summary>
    public string selfDataName = "";
    /// <summary>
    /// 軸位置からのブロック位置
    /// </summary>
    public List<Vector2Int>[] blockOffsets = new List<Vector2Int>[CommonDefines.PIECE_ROTATE_NUM];
    /// <summary>
    /// ピースのキックバックデータ
    /// </summary>
    public CommonDefines.KickBackSet[] kickBackChecks = new CommonDefines.KickBackSet[CommonDefines.PIECE_ROTATE_NUM];
    /// <summary>
    /// 初期位置
    /// </summary>
    public Vector2Int initPos = Vector2Int.zero;
    /// <summary>
    /// 形状
    /// </summary>
    public CommonDefines.EShapeType shapeType = CommonDefines.EShapeType.SHAPE_NONE;
}
