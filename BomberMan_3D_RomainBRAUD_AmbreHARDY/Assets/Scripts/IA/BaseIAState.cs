using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseIAState : MonoBehaviour
{
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
}
