using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBomb : MonoBehaviour
{
    public static ObjectPoolBomb Instance;

    private List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] private int amountToPool;

    [SerializeField] private GameObject _prefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
            else
            {
                GameObject obj = Instantiate(_prefab);
                obj.SetActive(false);
                poolObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }
}
