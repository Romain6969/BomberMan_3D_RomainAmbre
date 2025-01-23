using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAFlee : MonoBehaviour
{
    private float _distanceBetweenBombIA = 4f;
    private NavMeshAgent _agent;
    private GameObject _wichBomb = null;
    private bool _checkBombs = false;

    [Header("Etats")]
    [SerializeField] private FleeState _fleeState;
    public virtual BaseIAState PreviousState { get; set; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float shortestDistance = 50f;

        Debug.Log(_checkBombs);

        foreach (GameObject i in ObjectPoolBomb.Instance.poolObjects)
        {
            if (shortestDistance > Vector3.Distance(transform.position, i.transform.position))
            {
                shortestDistance = Vector3.Distance(transform.position, i.transform.position);
                _wichBomb = i;
            }
        }

        if (shortestDistance < _distanceBetweenBombIA && _wichBomb.activeInHierarchy == true)
        {
            if (_checkBombs == false && PreviousState != _fleeState)
            {
                PreviousState = IAStateMachine.Instance.CurrentState;
            }
            IAStateMachine.Instance.OnTransition(_fleeState);
        }
        if (_checkBombs == true)
        {
            if (shortestDistance > _distanceBetweenBombIA + 1)
            {
                _checkBombs = false;
                IAStateMachine.Instance.OnTransition(PreviousState);
            }
        }
    }

    public void RunAway()
    {
        Vector3 dirToAI = transform.position - _wichBomb.transform.position;

        Vector3 newPos = transform.position + dirToAI;

        _agent.SetDestination(newPos);

        _checkBombs = true;
    }
}
