using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayContext : MonoBehaviour
{
    AState state = null;

    public Field Field { get; private set; } = null;
    public PieceControll PieceControll { get; private set; } = null;


    // Start is called before the first frame update
    void Start()
    {
        Field = new Field();

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
}
