using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseIAState
{
    [SerializeField] private IAMovement _movementIA;

    public override void OnEnter()
    {
        _movementIA.WhereToGo();
    }

    public override void OnExit()
    {

    }
}
