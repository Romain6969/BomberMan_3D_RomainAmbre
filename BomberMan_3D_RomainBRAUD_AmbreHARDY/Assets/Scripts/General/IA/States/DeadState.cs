using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseIAState
{
    [SerializeField] private IAHealth iAHealth;

    public override void OnEnter()
    {
        iAHealth.IsDead();
    }

    public override void OnExit()
    {

    }
}
