using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    private List<Vector3> _vector = new List<Vector3>();
    [SerializeField] private List<GameObject> _listObject;

    private RaycastHit _hit;

    private void OnEnable()
    {
        SpawnPointObjectBomb.Instance.ReplaceBomb(gameObject);

        _vector.Add(Vector3.forward);
        _vector.Add(Vector3.back);
        _vector.Add(Vector3.left);
        _vector.Add(Vector3.right);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);

        _explosion.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(transform.position, _vector[i], out _hit, 2))
            {
                if (_hit.collider.name == "BreakableWallFalse(Clone)"|| _hit.collider.name == "Player")
                {
                    _listObject[i].SetActive(true);
                }
            }
            else
            {
                _listObject[i].SetActive(true);
            }
        }

        yield return new WaitForSeconds(2);

        GameObject objectBomb = ObjectPoolObjectBomb.Instance.GetPooledObject();

        if (objectBomb != null)
        {
            objectBomb.SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            if (_listObject[i])
            {
                _listObject[i].SetActive(false);
            }
        }

        _explosion.SetActive(false);
        gameObject.SetActive(false);
    }
}
