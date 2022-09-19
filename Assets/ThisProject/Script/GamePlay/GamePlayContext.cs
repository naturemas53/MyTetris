using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayContext : MonoBehaviour
{
    AState state = null;

    public Field Field { get; private set; } = null;
    public PieceControll PieceControll { get; private set; } = null;
    public ANextPieceSet NextPieceSet { get; private set; } = null;

    // Start is called before the first frame update
    void Start()
    {
        Field = new Field();
        PieceControll = new PieceControll( Field );

        state = new GameInitState( this );
        state.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        AState nextState = state.Update();

        if( state != null )
        {
            state = nextState;
            state.Initialize();
        }
    }

    /// <summary>
    /// �l�N��������ύX���܂�
    /// </summary>
    public void ChangeNextGenerator( ANextPieceSet setGenerator )
    {
        // TODO:�l�N���̈��p���Ƃ����Ȃ���s���Ȃ����������ǁA��U�ۗ�
        NextPieceSet = setGenerator;
    }
}
