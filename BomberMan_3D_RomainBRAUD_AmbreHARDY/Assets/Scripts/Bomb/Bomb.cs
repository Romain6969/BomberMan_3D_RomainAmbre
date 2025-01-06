using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);

        GameObject objectBomb = ObjectPoolObjectBomb.Instance.GetPooledObject();

        if (objectBomb != null)
        {
            objectBomb.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
