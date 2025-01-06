using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private UseBomb _useBombe;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectBomb" && _useBombe.NumberBomb <= 3)
        {
            _useBombe.NumberBomb++;
            other.gameObject.SetActive(false);
        }
    }
}
