using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class RebakeSurface : MonoBehaviour
{
    public static RebakeSurface Instance;

    [SerializeField] private NavMeshSurface _navMeshSurface;
    private bool _reload = false;

    private void FixedUpdate()
    {
        if (_reload) return;

        StartCoroutine(Wait());
    }

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
        _reload = true;
        yield return new WaitForSeconds(10);
        _navMeshSurface.BuildNavMesh();
        _reload = false;
    }
}
