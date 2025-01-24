using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    private List<Vector3> _vector = new List<Vector3>();
    [SerializeField] private List<GameObject> _listObject;
    [SerializeField] private AudioSource _audioSource;

    private RaycastHit _hit;
    private bool _explode = false;

    private void Awake()
    {
        _vector.Add(Vector3.forward);
        _vector.Add(Vector3.back);
        _vector.Add(Vector3.left);
        _vector.Add(Vector3.right);
    }

    private void OnEnable()
    {
        SpawnPointObjectBomb.Instance.ReplaceBomb(gameObject);
        RebakeSurface.Instance.OnRebake();

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);

        StartCoroutine(OnCheckCase());
    }

    IEnumerator OnCheckCase()
    {
        if (_explode) yield break;

        _explode = true;
        _audioSource.Play();
        _explosion.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(transform.position, _vector[i], out _hit, 2))
            {
                if (_hit.collider.name == "BreakableWallFalse(Clone)" || _hit.collider.tag == "Player" || _hit.collider.tag == "Bomb" ||
                    _hit.collider.tag == "Explosion" || _hit.collider.tag == "IA" || _hit.collider.tag == "ObjectBomb")
                {
                    _listObject[i].SetActive(true);
                }
            }
            else
            {
                _listObject[i].SetActive(true);
            }
        }
        RebakeSurface.Instance.OnRebake();

        yield return new WaitForSeconds(3);

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
        _explode = false;
        RebakeSurface.Instance.OnRebake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            StartCoroutine(OnCheckCase());
        }
    }
}
