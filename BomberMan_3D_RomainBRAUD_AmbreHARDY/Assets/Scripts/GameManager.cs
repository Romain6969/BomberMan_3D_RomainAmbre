using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public List<Health> _listHealth;

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

    public void End()
    {
        for (int i = 0; i < _listHealth.Count; i++)
        {
            _listHealth[i].Paralyze();
        }
    }
}
