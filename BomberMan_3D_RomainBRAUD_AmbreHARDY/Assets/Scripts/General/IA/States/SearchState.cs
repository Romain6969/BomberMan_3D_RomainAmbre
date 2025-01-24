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
        return;
    }
}
