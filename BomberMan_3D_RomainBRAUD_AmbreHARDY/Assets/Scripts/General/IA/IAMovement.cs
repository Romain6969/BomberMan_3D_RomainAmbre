using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovement : MonoBehaviour
{
    [Header("Listes")]
    [Tooltip("La liste des points les plus proches de l'IA pour faire apparaitre les bombes")]
    [SerializeField] private List<GameObject> _bombPoints = new List<GameObject>();
    [SerializeField] private ObjectPoolObjectBomb _objectBomb;

    [Header("Nombre Bombes")]
    [Tooltip("Nombre de bombes que l'IA à sur lui entre 0 min et 3 max")]
    public int NumberBomb = 0;

    private bool _isGoing = false;
    private int _whereToGo = 0;
    private float _offset = 0.5f;
    private NavMeshAgent _agent;
    private SearchState _searchState;
    private AttackState _attackState;

    private Vector3 _lastPosition;
    private float _timeStuck = 0f;
    private float _maxStuckTime = 3f;

    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _searchState = GetComponent<SearchState>();
        _attackState = GetComponent<AttackState>();
        _agent = GetComponent<NavMeshAgent>();
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

        if (Vector3.Distance(transform.position, _lastPosition) < 0.1f)
        {
            _timeStuck += Time.deltaTime;
            if (_timeStuck >= _maxStuckTime)
            {
                Debug.Log("IA bloquée");
                WhereToGo();
                _timeStuck = 0f;
            }
        }
        else
        {
            _timeStuck = 0f;
        }
        _lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectBomb" && NumberBomb < 3)
        {
            NumberBomb++;
            _audioSource.Play();
            other.gameObject.SetActive(false);
            if (NumberBomb == 3)
            {
                IAStateMachine.Instance.OnTransition(_attackState);
                return;
            }
            DoesAttackPlayer();
        }
    }

    public void DoesAttackPlayer()
    {
        int chanceToGoAttack = Random.Range(0, 10);
        if (chanceToGoAttack > 7 && NumberBomb < 3)
        {
            IAStateMachine.Instance.OnTransition(_attackState);
        }
    }

    public void WhereToGo()
    {
        _bombPoints = _objectBomb.poolObjects;
        for (int index = 0; index < _bombPoints.Count; index++)
        {
            if (_bombPoints[index].activeInHierarchy == false)
            {
                _bombPoints.Remove(_bombPoints[index]);
            }
        }
        _whereToGo = Random.Range(0, _bombPoints.Count);
        _agent.destination = _bombPoints[_whereToGo].transform.position;
        _isGoing = true;
    }
}