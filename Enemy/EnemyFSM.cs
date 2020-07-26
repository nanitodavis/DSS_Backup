using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM
{
    public Stack<EnumsScript.Enemy_State> states;
    // Start is called before the first frame update
    public EnemyFSM()
    {
        states = new Stack<EnumsScript.Enemy_State>();
        states.Push(EnumsScript.Enemy_State.INITIAL);
    }

    public EnumsScript.Enemy_State PopState()
    {
        if (states.Count > 0)
            return states.Pop();
        return EnumsScript.Enemy_State.NONE;
    }

    public void PushState(EnumsScript.Enemy_State newState)
    {
        states.Push(newState);
    }

    public EnumsScript.Enemy_State GetCurrentState()
    {
        if (states.Count > 0)
        {
            return states.Peek();
        }
        return EnumsScript.Enemy_State.NONE;
    }

    public void PopAllStates()
    {
        states = new Stack<EnumsScript.Enemy_State>();
        states.Push(EnumsScript.Enemy_State.DEATH);
    }
}