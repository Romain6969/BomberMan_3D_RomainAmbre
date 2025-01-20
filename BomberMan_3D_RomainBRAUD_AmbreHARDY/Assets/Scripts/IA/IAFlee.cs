using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAFlee : MonoBehaviour
{
    private float _distanceBetweenBombIA = 4f;
    [SerializeField] private List<GameObject> _bombs = new List<GameObject>();
    private NavMeshAgent _agent;
    private GameObject _wichBomb = null;
    [SerializeField] private FleeState _fleeState;
    private bool _checkBombs = true;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        float shortestDistance = 50f;

        

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
            IAStateMachine.Instance.OnTransition(_fleeState);
        }
    }

    public void RunAway()
    {
        Vector3 dirToAI = transform.position - _wichBomb.transform.position;

        Vector3 newPos = transform.position + dirToAI;

        _agent.SetDestination(newPos);
    }
}
