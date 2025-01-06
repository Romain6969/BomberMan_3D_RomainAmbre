using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int HP {  get; set; }
    [SerializeField] private GameObject _panel;
    [SerializeField] private Movement _movement;
    [SerializeField] private UseBomb _useBomb;

    private void Update()
    {
        if (HP <= 0)
        {
            _panel.SetActive(true);
            GameManager.Instance.End();
        }
    }

    public void Paralyze()
    {
        _movement.IsMoving = false;
        _useBomb.CanBomb = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            HP--;
        }
    }
}
