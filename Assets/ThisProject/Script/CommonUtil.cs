using System.Collections;
using System.Collections.Generic;

public static class CommonUtil
{
    /// <summary>
    /// checkValueが最大・最小を超えていた場合、
    /// その反対側へループするように値を返します。
    /// 
    /// 例１）checkValue 12 min 0 max 25 の場合
    /// -> 最大・最小共に超えてない為checkValueのまま返却（checkValueが0・25の場合も同様）
    /// 
    /// 例２）checkValue 36 min 0 max 25 の場合
    /// -> 最大25 を超えた分、最小から数えなおすように返す。つまり、
    /// 25,26,27... -> 25,0,1... と数えるので
    /// 返ってくる値は 10 ( 最小を超えた場合も同じ考え方になります )
    /// 
    /// </summary>
    /// <param name="checkValue">確認する値</param>
    /// <param name="min">最小</param>
    /// <param name="max">最大</param>
    /// <returns>最小・最大を　超えていない：checkValueそのまま 超えている：（上記参照）</returns>
    public static int LoopValue(int checkValue, int min, int max )
    {
        int retValue = checkValue;

        // TODO: 最大25に対して checkValue 51 というような2ループ以上を考慮する必要があるが、
        //       今はめんどいので後で

        if( retValue < min )
        {
            // +1 は 最大値位置の分
            retValue =  max - (min - retValue - 1);
        }
        else if ( retValue > max )
        {
            // -1 は 最小値位置の分
            retValue = (retValue - max - 1) + min;
        }

        return retValue;
    }
}
