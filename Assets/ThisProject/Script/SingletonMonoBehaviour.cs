using UnityEngine;
using System;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    protected static bool limitObject; // オブジェクトそのものを1つのみとするか.
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        if( CheckInstance() )
        {
            this.Initialize();
            // Initalizeの結果、オブジェクト自体の制限が設定されている場合、
            // シーン遷移による破棄が起こらないよう設定
            if (limitObject)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            // オブジェクト制限有無　はInitlaizeで設定してください.
            limitObject = false;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        // ここまで来た場合、スクリプトが重複していることを確認
        if(limitObject)
        {
            // オブジェクト自体の制限の為、ゲームオブジェクトごと消去
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        return false;
    }

    // 初期化.
    abstract protected void Initialize();
}
