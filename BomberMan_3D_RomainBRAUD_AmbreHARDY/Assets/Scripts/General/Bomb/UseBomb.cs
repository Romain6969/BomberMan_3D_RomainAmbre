using UnityEngine;
using UnityEngine.InputSystem;

public class UseBomb : MonoBehaviour
{
    [field: SerializeField] public int NumberBomb {  get; set; }
    [field: SerializeField] public bool CanBomb { get; set; } = true;

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
