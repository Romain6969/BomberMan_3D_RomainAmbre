using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int HP {  get; set; }
    [SerializeField] private GameObject _panel;
    [SerializeField] private Movement _movement;
    [SerializeField] private UseBomb _useBomb;
    [SerializeField] private bool _invincibility;
    [SerializeField] private Image _healthImage;
    [SerializeField] private AnimationCurve _curve;

    private void Update()
    {
        if (HP <= 0)
        {
            _panel.SetActive(true);
            GameManager.Instance.End();
        }
    }

    public void Paralyze()
    {
        _movement.IsMoving = false;
        _useBomb.CanBomb = false;
        _movement.CurrentMovement = new Vector2(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion" && !_invincibility)
        {
            HP--;
            float targetFillAmount = Mathf.InverseLerp(0, 3, HP);
            _healthImage.DOFillAmount(targetFillAmount, 0.5f).SetEase(_curve);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        _invincibility = true;
        yield return new WaitForSeconds(3);
        _invincibility = false;
    }
}
