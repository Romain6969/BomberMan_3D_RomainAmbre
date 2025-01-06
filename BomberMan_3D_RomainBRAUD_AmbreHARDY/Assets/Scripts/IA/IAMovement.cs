using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovement : MonoBehaviour
{
    [SerializeField]private List<GameObject> _spawnPoints = new List<GameObject>();
    private bool _isGoing = false;
    private int _whereToGo = 0;
    private float _offset = 0.5f;

    private void Start()
    {
        WhereToGo();
    }

    private void FixedUpdate()
    {
        if (_isGoing == true)
        {
            if (transform.position.x <= _spawnPoints[_whereToGo].transform.position.x + _offset && 
                transform.position.x >= _spawnPoints[_whereToGo].transform.position.x - _offset && 
                transform.position.z <= _spawnPoints[_whereToGo].transform.position.z + _offset && 
                transform.position.z >= _spawnPoints[_whereToGo].transform.position.z - _offset)
            {
                WhereToGo();
            }
        }
    }

    public void WhereToGo()
    {
        _whereToGo = Random.Range(0, _spawnPoints.Count);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = _spawnPoints[_whereToGo].transform.position;
        _isGoing = true;
    }

}
