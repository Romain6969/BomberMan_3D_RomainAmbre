using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAAttack : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private UseBomb _createBomb;
    [SerializeField] private SearchState _searchState;
    private NavMeshAgent _agent;
    public int _numBomb;
    private bool _canAttack = false;
    private float _offset = 1;
    
    private void Start()
    {
        
        _agent = GetComponent<NavMeshAgent>();
        foreach (GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "Player")
            {
                _player = i;
                break;
            }
        }
    }

    public void Attacking()
    {
        _canAttack = true;
    }

    private void FixedUpdate()
    {
        if (_canAttack)
        {
            _agent.destination = _player.transform.position;

            if (transform.position.x <= _player.transform.position.x + _offset &&
                transform.position.x >= _player.transform.position.x - _offset &&
                transform.position.z <= _player.transform.position.z + _offset &&
                transform.position.z >= _player.transform.position.z - _offset)
            {
                if (_numBomb > 0)
                {
                    _createBomb.Bomb();
                }
                else
                {
                    IAStateMachine.Instance.OnTransition(_searchState);
                }
            }
        }
    }
}
