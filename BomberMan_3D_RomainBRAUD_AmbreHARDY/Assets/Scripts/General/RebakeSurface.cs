using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class RebakeSurface : MonoBehaviour
{
    public static RebakeSurface Instance;

    [SerializeField] private NavMeshSurface _navMeshSurface;

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

    public void OnRebake()
    {
        _navMeshSurface.BuildNavMesh();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        _navMeshSurface.BuildNavMesh();
    }
}
