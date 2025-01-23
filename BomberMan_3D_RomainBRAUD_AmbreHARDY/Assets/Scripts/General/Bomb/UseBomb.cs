using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseBomb : MonoBehaviour
{
    [field: SerializeField] public int NumberBomb {  get; set; }
    [field: SerializeField] public bool CanBomb { get; set; } = true;
    [SerializeField] private TMP_Text _textBomb;

    private void Update()
    {
        _textBomb.text = $"{NumberBomb}";
    }

    public void OnUseBomb(InputAction.CallbackContext context)
    {
        if (context.performed && NumberBomb > 0)
        {
            Bomb();
        }
    }

    public void Bomb()
    {
        if (!CanBomb) return;

        GameObject Bomb = ObjectPoolBomb.Instance.GetPooledObject();

        if (Bomb != null)
        {
            Bomb.transform.position = transform.position;
            Bomb.SetActive(true);
        }
        NumberBomb--;
    }
}
