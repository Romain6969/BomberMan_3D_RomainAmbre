using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseIAState
{
    public IAFlee IAFlee;

    public override void OnEnter()
    {
        IAFlee.RunAway();
    }

    public override void OnExit()
    {

    }
}
