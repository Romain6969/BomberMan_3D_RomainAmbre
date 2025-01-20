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

    public void ReplaceBomb(GameObject obj)
    {
        float distance = 50;
        GameObject targetPosition = null;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            float distanceVerify = Vector3.Distance(SpawnPoints[i].transform.position, obj.transform.position);

            if (distanceVerify < distance)
            {
                distance = distanceVerify;
                targetPosition = SpawnPoints[i].gameObject;
            }
        }

        if (targetPosition != null)
        {
            obj.transform.position = targetPosition.transform.position;
        }
    }
}
