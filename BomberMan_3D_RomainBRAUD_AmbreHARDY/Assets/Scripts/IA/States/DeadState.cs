using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseIAState
{
    public GameObject PanelWin;
    public Movement PlayerMovement;

    public override void OnEnter()
    {
        PanelWin.SetActive(true);
        foreach(GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "ObjetcBomb" || i.tag == "Bomb")
            {
                Destroy(i);
            }
        }
        //PlayerMovement. Something to stop the player to move (probably a bool).
    }

    public override void OnExit()
    {

    }
}
