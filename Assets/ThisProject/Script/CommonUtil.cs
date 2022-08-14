using System.Collections;
using System.Collections.Generic;

public static class CommonUtil
{
    /// <summary>
    /// checkValue���ő�E�ŏ��𒴂��Ă����ꍇ�A
    /// ���̔��Α��փ��[�v����悤�ɒl��Ԃ��܂��B
    /// 
    /// ��P�jcheckValue 12 min 0 max 25 �̏ꍇ
    /// -> �ő�E�ŏ����ɒ����ĂȂ���checkValue�̂܂ܕԋp�icheckValue��0�E25�̏ꍇ�����l�j
    /// 
    /// ��Q�jcheckValue 36 min 0 max 25 �̏ꍇ
    /// -> �ő�25 �𒴂������A�ŏ����琔���Ȃ����悤�ɕԂ��B�܂�A
    /// 25,26,27... -> 25,0,1... �Ɛ�����̂�
    /// �Ԃ��Ă���l�� 10 ( �ŏ��𒴂����ꍇ�������l�����ɂȂ�܂� )
    /// 
    /// </summary>
    /// <param name="checkValue">�m�F����l</param>
    /// <param name="min">�ŏ�</param>
    /// <param name="max">�ő�</param>
    /// <returns>�ŏ��E�ő���@�����Ă��Ȃ��FcheckValue���̂܂� �����Ă���F�i��L�Q�Ɓj</returns>
    public static int LoopValue(int checkValue, int min, int max )
    {
        int retValue = checkValue;

        // TODO: �ő�25�ɑ΂��� checkValue 51 �Ƃ����悤��2���[�v�ȏ���l������K�v�����邪�A
        //       ���͂߂�ǂ��̂Ō��

        if( retValue < min )
        {
            // +1 �� �ő�l�ʒu�̕�
            retValue =  max - (min - retValue - 1);
        }
        else if ( retValue > max )
        {
            // -1 �� �ŏ��l�ʒu�̕�
            retValue = (retValue - max - 1) + min;
        }

        return retValue;
    }
}
