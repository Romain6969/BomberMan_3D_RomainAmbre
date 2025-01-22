using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAAttack : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private SearchState _searchState;
    public IAMovement IaMovement;
    private NavMeshAgent _agent;
    public int _numBomb;
    private bool _canAttack = false;
    private float _offset = 1;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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
                if (IaMovement.NumberBomb > 0)
                {
                    Bomb();
                    if (IaMovement.NumberBomb <= 0)
                    {
                        IAStateMachine.Instance.OnTransition(_searchState);
                        _canAttack = false;
                    }
                    StartCoroutine(Wait());
                }
                else
                {
                    IAStateMachine.Instance.OnTransition(_searchState);
                    _canAttack = false;
                }
            }
        }
    }

    public void Bomb()
    {

        GameObject Bomb = ObjectPoolBomb.Instance.GetPooledObject();

        if (Bomb != null)
        {
            Bomb.transform.position = transform.position;
            Bomb.SetActive(true);
        }
        IaMovement.NumberBomb--;
    }


        IEnumerator Wait()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1);
        _canAttack = true;
    }
}
