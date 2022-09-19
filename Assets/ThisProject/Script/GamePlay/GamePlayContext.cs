using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayContext : MonoBehaviour
{
    AState state = null;
    public Field Field { get; private set; } = null;
    public PieceControll PieceControll { get; private set; } = null;
    public ANextPieceSet NextPieceSet { get; private set; } = null;

    [SerializeField]
    FieldView fieldView;

    // Start is called before the first frame update
    void Start()
    {
        Field = new Field();
        PieceControll = new PieceControll( Field );

        fieldView.Initalize( Field );

        state = new GameInitState( this );
        state.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        AState nextState = state.Update();

        if( nextState != null )
        {
            state = nextState;
            state.Initialize();
        }
    }

    /// <summary>
    /// ネク順生成を変更します
    /// </summary>
    public void ChangeNextGenerator( ANextPieceSet setGenerator )
    {
        // TODO:ネク順の引継ぎとかしなきゃ行けなさそうだけど、一旦保留
        NextPieceSet = setGenerator;
    }
}
