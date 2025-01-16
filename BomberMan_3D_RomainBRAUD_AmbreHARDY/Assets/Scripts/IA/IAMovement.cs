using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovement : MonoBehaviour
{
    [SerializeField]private List<GameObject> _bombPoints = new List<GameObject>();
    private bool _isGoing = false;
    private int _whereToGo = 0;
    private float _offset = 0.5f;
    private NavMeshAgent _agent;
    private SearchState _searchState;

    private void Start()
    {
        _searchState = GetComponent<SearchState>();
        _agent = GetComponent<NavMeshAgent>();
        foreach (GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "SpawnPoints")
            {
                _bombPoints.Add(i);
            }
        }
        IAStateMachine.Instance.OnTransition(_searchState);
    }

    private void FixedUpdate()
    {
        if (_isGoing == true)
        {
            if (transform.position.x <= _bombPoints[_whereToGo].transform.position.x + _offset && 
                transform.position.x >= _bombPoints[_whereToGo].transform.position.x - _offset && 
                transform.position.z <= _bombPoints[_whereToGo].transform.position.z + _offset && 
                transform.position.z >= _bombPoints[_whereToGo].transform.position.z - _offset)
            {
                WhereToGo();
            }
        }
    }

    public void WhereToGo()
    {
        _whereToGo = Random.Range(0, _bombPoints.Count);
        _agent.destination = _bombPoints[_whereToGo].transform.position;
        _isGoing = true;
    }

}
