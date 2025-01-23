using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class IAAttack : MonoBehaviour
{
    [Header("Position du joueur")]
    [SerializeField] private GameObject _player;

    [Header("Les Etats")]
    [SerializeField] private SearchState _searchState;
    public IAMovement IaMovement;

    [Header("Bomb UI")]
    [SerializeField] public TMP_Text _textBomb;

    private bool _canAttack = false;
    private float _offset = 2;
    private NavMeshAgent _agent;

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
        _textBomb.text = $"{IaMovement.NumberBomb}";
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
                    IaMovement.NumberBomb = Bomb(IaMovement.NumberBomb);
                    if (Bomb(IaMovement.NumberBomb) <= 0)
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

    public int Bomb(int bombs)
    {

        GameObject Bomb = ObjectPoolBomb.Instance.GetPooledObject();

        if (Bomb != null)
        {
            Bomb.transform.position = transform.position;
            Bomb.SetActive(true);
        }
        return bombs -=1;
    }


        IEnumerator Wait()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1);
        _canAttack = true;
    }
}
