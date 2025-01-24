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
