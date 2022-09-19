using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class CommonDefines
{
    /// <summary>
    /// ファイル読み込み時の入り口部分
    /// </summary>
    public const string FILE_INPUT_ENTRY = "D:\\Document_D\\tetris\\Assets\\ThisProject\\Data\\";

    /// <summary>
    /// 回転種類の数( 0°、90°、180°、270°)
    /// </summary>
    public const int PIECE_ROTATE_NUM = 4;

    /// <summary>
    /// 回転状態
    /// </summary>
    public enum EPieceRotate
    {
        None = -1, // いずれでもない（多分ありえない）

        ZERO_O_CLOCK,   //  0時方向（  0°） ...０時ってO'Clockって言うのかしら
        THREE_O_CLOCK,  //  3時方向（ 90°）
        SIX_O_CLOCK,    //  6時方向（180°）
        NINE_O_CLOCK,   //  9時方向（270°）

        MAX = PIECE_ROTATE_NUM, // 一応で定数再指定（ホントはあまりよくないカモ）
    }

    /// <summary>
    /// 回転方向
    /// </summary>
    public enum ERotateDirection
    {
        NONE,  // いずれでもない（多分ありえない）

        RIGHT = 1,   // 右回転
        LEFT  = -1,  // 左回転

        MAX = 2, // 回転の種類数
        // 1、-1としたのは回転時、上記EPieceRotateの変動に便利そうだな　と思ったから
    }

    /// <summary>
    /// ブロックの種類
    /// </summary>
    public enum EBlockType
    {
        BLOCK_NONE,   // いずれでもない（本来ありえない）
        BLOCK_EMPTY,  // 空
        BLOCK_WALL,   // 壁
        BLOCK_NORMAL, // 通常
    }

    /// <summary>
    /// 形の種類
    /// </summary>
    public enum EShapeType
    { 
        SHAPE_NONE, // いずれでもない（本来あり得ない）

        SHAPE_Z, // ここからSHAPE_Iまで　通常テトリスの形状
        SHAPE_S,
        SHAPE_J,
        SHAPE_L,
        SHAPE_O,
        SHAPE_T,
        SHAPE_I,

        SHAPE_SPECIAL, // 特集形状のピース

        SHAPE_MAX,
    }

    /// <summary>
    /// キックバックデータ（型名の変え方わからなくて、強引にクラスで包んだやべーやつ）
    /// </summary>
    public class KickBackSet
    {
        public Dictionary<ERotateDirection, List<Vector2Int>> data;

        public KickBackSet()
        {
            data = new Dictionary<ERotateDirection, List<Vector2Int>>();
            data[ ERotateDirection.LEFT  ] = new List<Vector2Int>();
            data[ ERotateDirection.LEFT  ].Clear();
            data[ ERotateDirection.RIGHT ] = new List<Vector2Int>();
            data[ ERotateDirection.RIGHT ].Clear();
        }
    }

    /// <summary>
    /// ピースのパラメータ
    /// </summary>
    public class PieceParam
    {
        /// <summary>
        /// 構成するブロック
        /// </summary>
        public List<ABlock> blocks;
        /// <summary>
        /// 当ピースに関するデータ（形状タイプ等）
        /// </summary>
        public PieceDataColumns pieceDatas;

        public PieceParam()
        {
            blocks = new List<ABlock>();
            blocks.Clear();
        }
    }

    /// <summary>
    /// ピース操作のパラメータ
    /// Q.なんでこいつ別にしとんの？ 
    /// A.なんかレベル毎にパラメータ変えるとかで、テーブルとかで使うかなって...
    /// </summary>
    public struct PieceControllerOption
    {
        public float dropTime; // 1段下がるまでの時間
        public float lockDownTime; // 接地から固まるまでの時間.

        public int moveInfinity;   // 移動インフィニティ
        public int rotateInfinity; // 回転インフィニティ
        public int wildInfinity;   // ワイルドインフィニティ(移動回転　どっちも有効）
    }
}