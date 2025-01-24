using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private UseBomb _useBombe;
    [SerializeField] private AudioSource _audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectBomb" && _useBombe.NumberBomb < 3)
        {
            _audioSource.Play();
            _useBombe.NumberBomb++;
            _useBombe.OnActualiseBomb();
            other.gameObject.SetActive(false);
        }
    }
}
