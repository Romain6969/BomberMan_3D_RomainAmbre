using UnityEngine;

public class BreakableWallFalse : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            wall.SetActive(true);
            wall.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
