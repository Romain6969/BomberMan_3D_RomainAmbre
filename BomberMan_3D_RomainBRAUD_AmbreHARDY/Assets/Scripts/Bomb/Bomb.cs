using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    private void OnEnable()
    {
        SpawnPointObjectBomb.Instance.ReplaceBomb(gameObject);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        _explosion.SetActive(true);
        yield return new WaitForSeconds(1);

        GameObject objectBomb = ObjectPoolObjectBomb.Instance.GetPooledObject();

        if (objectBomb != null)
        {
            objectBomb.SetActive(true);
        }

        _explosion.SetActive(false);
        gameObject.SetActive(false);
    }
}
