using UnityEngine;

public class IAHealth : MonoBehaviour
{
    public GameObject PanelWin;
    public Movement PlayerMovement;
    [SerializeField] private DeadState _deadState;
    public int HP = 3;

    public void IADamaged()
    {
        if (HP > 0)
        {
            HP --;
        }
        else
        {
            IAStateMachine.Instance.OnTransition(_deadState);
        }
    }

    public void IsDead()
    {
        PanelWin.SetActive(true);
        foreach (GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "ObjetcBomb" || i.tag == "Bomb" || i.tag == "Explosion")
            {
                Destroy(i);
            }
        }
        PlayerMovement.CanMove = false;
        Destroy(gameObject);
    }
}
