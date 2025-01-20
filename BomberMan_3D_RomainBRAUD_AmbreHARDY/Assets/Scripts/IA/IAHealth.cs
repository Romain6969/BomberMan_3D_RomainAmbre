using UnityEngine;

public class IAHealth : MonoBehaviour
{
    public GameObject PanelWin;
    public Movement PlayerMovement;
    [SerializeField] private DeadState _deadState;
    public int HP = 3;

    private void Update()
    {
        if (HP <= 0)
        {
            IAStateMachine.Instance.OnTransition(_deadState);
            HP = 3;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            HP--;
        }
    }

}
