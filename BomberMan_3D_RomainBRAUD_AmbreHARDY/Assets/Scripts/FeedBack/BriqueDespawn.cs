using UnityEngine;

public class BriqueDespawn : MonoBehaviour
{
    private bool _selfDestruct = false;

    private void OnEnable()
    {
        _selfDestruct = true;
    }

    private void Update()
    {
        if (!_selfDestruct) return;

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), 1 * Time.deltaTime);
        Destroy(gameObject, 3);
    }
}
