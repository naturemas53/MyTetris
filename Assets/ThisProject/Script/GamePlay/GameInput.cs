using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : SingletonMonoBehaviour< GameInput >
{
    /// <summary>
    /// 操作種類
    /// </summary>
    public enum EAction
    {
        ROTATE_LEFT,
        ROTATE_RIGHT,

        UP,
        DOWN,
        LEFT,
        RIGHT,

        HOLD,

        ACTION_MAX
    }

    /// <summary>
    /// キーリピートに関する情報.
    /// </summary>
    struct KeyRepeatData
    {
        public float remainRepeatTime;
        public bool isDown;

        public KeyRepeatData(float remainTime = 1.0f, bool keyDown = false )
        {
            remainRepeatTime = remainTime;
            isDown = keyDown;
        }
    }

    /// <summary>
    /// EnumとInputActionの紐づけ
    /// </summary>
    Dictionary<EAction, InputAction> inputActions = new Dictionary<EAction, InputAction>();
    /// <summary>
    /// 各アクションのキーリピート情報.
    /// </summary>
    Dictionary<EAction, KeyRepeatData> repeatDataMap = new Dictionary<EAction, KeyRepeatData>();

    /// <summary>
    /// キーリピートの間隔
    /// </summary>
    public float RepeatInterval = 1.0f;
    /// <summary>
    /// リピート開始までの待機時間.
    /// </summary>
    public float WaitInterbal = 1.0f;

    PlayerInput playerInput = null;

    protected override void Initialize()
    {
        playerInput = GetComponent<PlayerInput>();

        InitActionMap();
        InitRepeatDataMap();
        BindEventToActionTriggered();
    }

    /// <summary>
    /// inputActionsの初期化.
    /// </summary>
    void InitActionMap()
    {
        inputActions.Clear();

        var currentMap = playerInput.currentActionMap;

        inputActions.Add( EAction.ROTATE_LEFT, currentMap["LeftRotate"] );
        inputActions.Add( EAction.ROTATE_RIGHT, currentMap["RightRotate"] );
        inputActions.Add( EAction.UP, currentMap["Up"] );
        inputActions.Add( EAction.DOWN, currentMap["Down"] );
        inputActions.Add( EAction.RIGHT, currentMap["Right"] );
        inputActions.Add( EAction.LEFT, currentMap["Left"] );
        inputActions.Add( EAction.HOLD, currentMap["Hold"] );
    }

    /// <summary>
    /// repeatDataMapの初期化.
    /// </summary>
    void InitRepeatDataMap()
    {
        repeatDataMap.Clear();
        for( EAction action = EAction.ROTATE_LEFT; action < EAction.ACTION_MAX; ++action )
        {
            repeatDataMap.Add( action, new KeyRepeatData( RepeatInterval ) );
        }
    }

    /// <summary>
    /// アクションがなんか起きた時に合わせてイベントをバインド
    /// </summary>
    void BindEventToActionTriggered()
    {
        foreach( var pair in inputActions )
        {
            InputAction action = pair.Value;
            action.started += (x) => { ReceiveBeginDownKey( pair.Key ); };
            action.canceled += (x) => { ReceiveEndDownKey( pair.Key ); };
        }
    }

    private void Update()
    {
        for (EAction action = EAction.ROTATE_LEFT; action < EAction.ACTION_MAX; ++action)
        {
            UpdateDownKey( action );
        }
    }

    /// <summary>
    /// 指定アクションが発行され続けているか
    /// </summary>
    /// <param name="targetAction"></param>
    /// <returns></returns>
    public bool IsDown( EAction targetAction )
    {
        // HACK: リピート情報から取るのは違う気がするけど　一旦はこれで...
        return repeatDataMap[targetAction].isDown;
    }

    /// <summary>
    /// 指定アクションが発行された瞬間か（キーリピート有無選択可）
    /// </summary>
    /// <param name="targetAction"></param>
    /// <returns></returns>
    public bool IsPress(EAction targetAction, bool isAllowKeyRepeat = false)
    {
        bool retPress = false;

        InputAction action = inputActions[targetAction];
        retPress = action.triggered;

        if( isAllowKeyRepeat )
        {
            KeyRepeatData keyRepeatData = repeatDataMap[targetAction];

            retPress = retPress || (keyRepeatData.remainRepeatTime <= 0.0f);
        }

        return retPress;
    }

    /// <summary>
    /// キー押下開始を受け取ります
    /// </summary>
    void ReceiveBeginDownKey( EAction targetAction )
    {
        KeyRepeatData data = repeatDataMap[ targetAction ];
        data.isDown = true;
        data.remainRepeatTime = WaitInterbal; // 最初は待ちの意味合いとして長い方を入れておく
        repeatDataMap[targetAction] = data;

        //Debug.Log( targetAction.ToString() + "が押されました" );

    }

    /// <summary>
    /// 押下中のキー情報を更新します
    /// </summary>
    void UpdateDownKey( EAction targetAction )
    {
        KeyRepeatData data = repeatDataMap[targetAction];

        if( !IsDown(targetAction) ) return;

        if( data.remainRepeatTime <= 0.0f)
        {
            data.remainRepeatTime += RepeatInterval;
        }

        data.remainRepeatTime -= Time.deltaTime;

        repeatDataMap[targetAction] = data;
    }

    /// <summary>
    /// キー押下終了を受け取ります
    /// </summary>
    void ReceiveEndDownKey( EAction targetAction )
    {
        KeyRepeatData data = repeatDataMap[targetAction];
        data.isDown = false;
        data.remainRepeatTime = WaitInterbal; // 0以下でキーリピート発生とみなされるため、値を入れておく.
        repeatDataMap[targetAction] = data;

        //Debug.Log(targetAction.ToString() + "が離されました");

    }
}
