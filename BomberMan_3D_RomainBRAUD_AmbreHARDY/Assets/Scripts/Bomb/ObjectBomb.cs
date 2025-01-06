using UnityEngine;

public class ObjectBomb : MonoBehaviour
{
    private void OnEnable()
    {
        int index = Random.Range(0, SpawnPointObjectBomb.Instance.SpawnPoints.Count);
        transform.position = SpawnPointObjectBomb.Instance.SpawnPoints[index].position;
    }
}