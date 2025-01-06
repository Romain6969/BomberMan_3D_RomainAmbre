using System.Collections.Generic;
using UnityEngine;

public class SpawnPointObjectBomb : MonoBehaviour
{
    public static SpawnPointObjectBomb Instance;

    [field: SerializeField] public List<Transform> SpawnPoints {  get; private set; }

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
}
