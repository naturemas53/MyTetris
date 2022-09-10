using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static CommonDefines;

/// <summary>
/// 最初ScriptableObjectでやろうとしたけど、
/// Dictionaryが対応してないことを知って
/// 緊急策で用意したJson保存用のクラス
/// </summary>
public static class PieceDataColumsIO
{
    [System.Serializable]
    class BreakDictionary <TKey,TValue>
    {
        public List<int> elementNums;
        public List<TKey> keyArray;
        public List<TValue> allValuesArray;

            
        public BreakDictionary()
        {
            elementNums = new List<int>();
            elementNums.Clear();
            keyArray = new List<TKey>();
            keyArray.Clear();
            allValuesArray = new List<TValue>();
            allValuesArray.Clear();
        }

        /// <summary>
        /// Dictionaryデータを設定します.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        public void SetDictionary( Dictionary< TKey,List<TValue> > keyValuePairs )
        {
            keyArray = new List<TKey>( keyValuePairs.Keys );

            elementNums.Clear();
            allValuesArray.Clear();

            foreach( var pair in keyValuePairs )
            {
                elementNums.Add( pair.Value.Count );
                allValuesArray.AddRange( pair.Value );
            }
        }

        /// <summary>
        /// DIctionaryデータを取得します.
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, List<TValue>> GetDictionary()
        {
            Dictionary<TKey, List<TValue>> retValue = new Dictionary<TKey, List<TValue>>();
            retValue.Clear();

            int fetchedNum = 0;
            for(int i = 0; i < keyArray.Count; ++i)
            {
                List<TValue> buffer = allValuesArray.GetRange( fetchedNum, elementNums[i] );
                retValue.Add( keyArray[i], buffer );
                fetchedNum += elementNums[i];
            }

            return retValue;
        }
    }

    [System.Serializable]
    class PieceDataJson
    {
        public BreakDictionary<EPieceRotate, Vector2Int> blockOffsets;
        public BreakDictionary<ERotateDirection, Vector2Int>[] kickbacks;

        public Vector2Int initPos = Vector2Int.zero;
        public EShapeType shapeType = EShapeType.SHAPE_NONE;
        public string dataName = "";

        public PieceDataJson()
        {
            blockOffsets = new BreakDictionary<EPieceRotate, Vector2Int>();

            kickbacks = new BreakDictionary<ERotateDirection, Vector2Int>[ (int)EPieceRotate.MAX ];
            for( int i =0; i < (int)EPieceRotate.MAX; ++i )
            {
                kickbacks[i] = new BreakDictionary<ERotateDirection, Vector2Int>();
            }
        }
    }

    /// <summary>
    /// 出力
    /// </summary>
    /// <param name="outputPath"></param>
    /// <param name="outData"></param>
    /// <returns></returns>
    public static bool Export(string outputPath, PieceDataColumns outData )
    {
        PieceDataJson outConvData = new PieceDataJson();

        outConvData.blockOffsets.SetDictionary( outData.blockOffSets );

        for( int i = 0; i < (int)EPieceRotate.MAX; ++i )
        {
            outConvData.kickbacks[i].SetDictionary( outData.kickBacks[(EPieceRotate)i].data );
        }

        outConvData.initPos = outData.initPos;
        outConvData.shapeType = outData.shapeType;
        outConvData.dataName = outData.selfDataName;

        string outJson = JsonUtility.ToJson( outConvData );

        string path = outputPath + outData.selfDataName + ".json";
        File.WriteAllText( path, outJson );

        return true;
    }

    /// <summary>
    /// 入力（というか読み取り）
    /// </summary>
    /// <param name="inputPath"></param>
    /// <returns></returns>
    public static PieceDataColumns Import(string inputPath)
    {
        PieceDataColumns retValue = null;

        string buffer = "";
        buffer = File.ReadAllText( inputPath );
        
        PieceDataJson inputConvData = JsonUtility.FromJson<PieceDataJson>( buffer );
        if( inputConvData == null )
        {
            return retValue;
        }

        retValue = new PieceDataColumns();

        retValue.blockOffSets = inputConvData.blockOffsets.GetDictionary();
        for (int i = 0; i < (int)EPieceRotate.MAX; ++i)
        {
            KickBackSet kickback = new KickBackSet();
            kickback.data = inputConvData.kickbacks[i].GetDictionary();
            retValue.kickBacks[(EPieceRotate)i] = kickback;
        }

        retValue.initPos = inputConvData.initPos;
        retValue.shapeType = inputConvData.shapeType;
        retValue.selfDataName = inputConvData.dataName;

        return retValue;
    }
}
