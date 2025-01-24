using UnityEngine;

public class AttackState : BaseIAState
{
    [SerializeField] private IAAttack _iAAttack;
    
    public override void OnEnter()
    {
        _iAAttack.Attacking();
    }

    public override void OnExit()
    {

    }
}
