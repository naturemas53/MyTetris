using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : SingletonMonoBehaviour< GameInput >
{
    /// <summary>
    /// ������
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
    /// �L�[���s�[�g�Ɋւ�����.
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
    /// Enum��InputAction�̕R�Â�
    /// </summary>
    Dictionary<EAction, InputAction> inputActions = new Dictionary<EAction, InputAction>();
    /// <summary>
    /// �e�A�N�V�����̃L�[���s�[�g���.
    /// </summary>
    Dictionary<EAction, KeyRepeatData> repeatDataMap = new Dictionary<EAction, KeyRepeatData>();

    /// <summary>
    /// �L�[���s�[�g�̊Ԋu
    /// </summary>
    public float RepeatInterval = 1.0f;
    /// <summary>
    /// ���s�[�g�J�n�܂ł̑ҋ@����.
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
    /// inputActions�̏�����.
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
    /// repeatDataMap�̏�����.
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
    /// �A�N�V�������Ȃ񂩋N�������ɍ��킹�ăC�x���g���o�C���h
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
    /// �w��A�N�V���������s���ꑱ���Ă��邩
    /// </summary>
    /// <param name="targetAction"></param>
    /// <returns></returns>
    public bool IsDown( EAction targetAction )
    {
        // HACK: ���s�[�g��񂩂���͈̂Ⴄ�C�����邯�ǁ@��U�͂����...
        return repeatDataMap[targetAction].isDown;
    }

    /// <summary>
    /// �w��A�N�V���������s���ꂽ�u�Ԃ��i�L�[���s�[�g�L���I���j
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
    /// �L�[�����J�n���󂯎��܂�
    /// </summary>
    void ReceiveBeginDownKey( EAction targetAction )
    {
        KeyRepeatData data = repeatDataMap[ targetAction ];
        data.isDown = true;
        data.remainRepeatTime = WaitInterbal; // �ŏ��͑҂��̈Ӗ������Ƃ��Ē����������Ă���
        repeatDataMap[targetAction] = data;

        //Debug.Log( targetAction.ToString() + "��������܂���" );

    }

    /// <summary>
    /// �������̃L�[�����X�V���܂�
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
    /// �L�[�����I�����󂯎��܂�
    /// </summary>
    void ReceiveEndDownKey( EAction targetAction )
    {
        KeyRepeatData data = repeatDataMap[targetAction];
        data.isDown = false;
        data.remainRepeatTime = WaitInterbal; // 0�ȉ��ŃL�[���s�[�g�����Ƃ݂Ȃ���邽�߁A�l�����Ă���.
        repeatDataMap[targetAction] = data;

        //Debug.Log(targetAction.ToString() + "��������܂���");

    }
}
