using System.Collections;
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
    [SerializeField] private AudioSource _audioSource;

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

    public int Bomb(int bombs)
    {
        GameObject Bomb = ObjectPoolBomb.Instance.GetPooledObject();

        if (Bomb != null)
        {
            Bomb.transform.position = PredictPlayerPosition();
            Bomb.SetActive(true);
            _audioSource.Play();
        }
        return bombs -= 1;
    }

    private Vector3 PredictPlayerPosition()
    {
        Vector3 playerVelocity = _player.GetComponent<Rigidbody>().velocity;
        float predictionTime = 1.5f;

        Vector3 predictedPosition = _player.transform.position + playerVelocity * predictionTime;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(predictedPosition, out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return _player.transform.position;
    }

    IEnumerator Wait()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1);
        _canAttack = true;
    }
}
