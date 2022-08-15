using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonDefines;

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
    public Dictionary<EPieceRotate, List<Vector2Int>> blockOffSets;
    /// <summary>
    /// ピースのキックバックデータ
    /// </summary>
    public Dictionary<EPieceRotate, KickBackSet> kickBacks;
    /// <summary>
    /// 初期位置
    /// </summary>
    public Vector2Int initPos = Vector2Int.zero;
    /// <summary>
    /// 形状
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
