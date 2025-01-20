using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAFlee : MonoBehaviour
{
    private float _distanceBetweenBombIA = 4f;
    private NavMeshAgent _agent;
    private GameObject _wichBomb = null;
    [SerializeField] private FleeState _fleeState;
    private bool _checkBombs = false;
    public virtual BaseIAState PreviousState { get; set; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
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
            if (_checkBombs == false)
            {
                PreviousState = IAStateMachine.Instance.CurrentState;
            }
            IAStateMachine.Instance.OnTransition(_fleeState);
        }
        if (_checkBombs == true)
        {
            if (shortestDistance > _distanceBetweenBombIA && _wichBomb.activeInHierarchy == true)
            {
                IAStateMachine.Instance.OnTransition(PreviousState);
                _checkBombs = false;
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
