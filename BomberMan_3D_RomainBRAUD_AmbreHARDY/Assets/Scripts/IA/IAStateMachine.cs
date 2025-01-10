using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStateMachine : MonoBehaviour
{
    public static IAStateMachine Instance;
    public virtual BaseIAState CurrentState { get; set; }
    public virtual BaseIAState InitialSate { get; set; }

    [Header("Les états de l'IA")]
    public SearchState Search;
    public DeadState Dead;
    public AttackState Attack;
    public FleeState Flee;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        InitialSate = Search;
    }


    public virtual void OnTransition(BaseIAState newState)
    {
        CurrentState = newState;
        InitialSate.OnExit();
        CurrentState.OnEnter();
        InitialSate = newState;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case SearchState:

                break;
            case DeadState:

                break;
            case AttackState:

                break;
            case FleeState:

                break;
        }

        /*
        if (CurrentState == Search)
        {

        }
        else if (CurrentState == Dead) 
        {

        }
        else if (CurrentState == Attack) 
        {

        }
        else if (CurrentState == Flee)
        {

        }
        */
    }
}
