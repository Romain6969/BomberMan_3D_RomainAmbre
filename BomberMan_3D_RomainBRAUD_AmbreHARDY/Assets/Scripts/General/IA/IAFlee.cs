using UnityEngine;
using UnityEngine.AI;

public class IAFlee : MonoBehaviour
{
    private float _distanceBetweenBombIA = 3f;
    private NavMeshAgent _agent;
    private GameObject _wichBomb = null;
    private bool _checkBombs = false;
    private float _fleeCooldown = 1.5f;
    private float _lastFleeTime = 0f;

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

        foreach (GameObject i in ObjectPoolBomb.Instance.poolObjects)
        {
            if (i.activeInHierarchy && shortestDistance > Vector3.Distance(transform.position, i.transform.position))
            {
                shortestDistance = Vector3.Distance(transform.position, i.transform.position);
                _wichBomb = i;
            }
        }

        if (shortestDistance < _distanceBetweenBombIA && _wichBomb != null && Time.time - _lastFleeTime > _fleeCooldown)
        {
            if (!_checkBombs && PreviousState != _fleeState)
            {
                PreviousState = IAStateMachine.Instance.CurrentState;
            }
            IAStateMachine.Instance.OnTransition(_fleeState);
            _lastFleeTime = Time.time;
        }
        else if (_checkBombs && (shortestDistance > _distanceBetweenBombIA + 1 || _wichBomb == null))
        {
            _checkBombs = false;
            IAStateMachine.Instance.OnTransition(PreviousState);
        }
    }

    public void RunAway()
    {
        if (_wichBomb != null)
        {
            Vector3 dirToAI = transform.position - _wichBomb.transform.position;
            Vector3 newPos = transform.position + dirToAI.normalized * _distanceBetweenBombIA;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(newPos, out hit, _distanceBetweenBombIA, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }

            _checkBombs = true;
        }
    }
}
