using UnityEngine;

public class IAHealth : MonoBehaviour
{
    public GameObject PanelWin;
    public Movement PlayerMovement;

    public void IsDead()
    {
        PanelWin.SetActive(true);
        foreach (GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "ObjetcBomb" || i.tag == "Bomb")
            {
                Destroy(i);
            }
        }
        //PlayerMovement. Something to stop the player to move (probably a bool).
    }
}
