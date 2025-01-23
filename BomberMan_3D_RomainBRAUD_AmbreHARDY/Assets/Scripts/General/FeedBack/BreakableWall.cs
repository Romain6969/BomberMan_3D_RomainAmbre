using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listRigidBody;
    [SerializeField] private GameObject _ciment;

    [SerializeField] private Vector3 _radiusDetection;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private LayerMask _affectedLayers;

    private RaycastHit _hit;

    private void OnEnable()
    {
        OnExplosion();
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 2))
        {
            if (_hit.collider.tag == "SpawnPoints" && !SpawnPointObjectBomb.Instance.SpawnPoints.Contains(_hit.collider.gameObject.transform))
            {
                SpawnPointObjectBomb.Instance.SpawnPoints.Add(_hit.collider.gameObject.transform);
            }
        }
    }

    public void OnExplosion()
    {
        _ciment.SetActive(false);

        Collider[] colliders = Physics.OverlapBox(transform.position, _radiusDetection, Quaternion.identity, _affectedLayers);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;
            collider.gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                collider.transform.parent = null;
                rb.AddExplosionForce(_force, transform.position, _radius);
            }
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        RebakeSurface.Instance.OnRebake();
        Destroy(gameObject);
    }
}
