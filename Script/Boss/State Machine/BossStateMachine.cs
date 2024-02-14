using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public BossState CurrentBossState;

    public void Initialize(BossState startingState)
    {
        CurrentBossState = startingState;
        CurrentBossState.EnterState();
    }

    public void ChangeState(BossState newState)
    {
        CurrentBossState.ExitState();
        CurrentBossState = newState;
        CurrentBossState.EnterState();
    }
}
