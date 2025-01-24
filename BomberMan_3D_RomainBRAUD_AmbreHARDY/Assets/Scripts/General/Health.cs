using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int HP {  get; set; }
    [SerializeField] private GameObject _panel;
    [SerializeField] private Movement _movement;
    [SerializeField] private UseBomb _useBomb;
    [SerializeField] private bool _invincibility;
    [SerializeField] private Image _healthImage;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private List<GameObject> _faceList;
    [SerializeField] private AudioSource _audioSource;

    public void Update()
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
        _invincibility = true;
        _faceList[0].SetActive(false);
        _faceList[1].SetActive(false);
        _faceList[2].SetActive(true);
        _useBomb.CanBomb = false;
        _movement.CurrentMovement = new Vector2(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion" && !_invincibility)
        {
            HP--;
            _audioSource.Play();
            float targetFillAmount = Mathf.InverseLerp(0, 3, HP);
            _healthImage.DOFillAmount(targetFillAmount, 0.5f).SetEase(_curve);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        _faceList[1].SetActive(true);
        _faceList[0].SetActive(false);
        _invincibility = true;
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _renderer.enabled = true;
        _invincibility = false;
        _faceList[1].SetActive(false);
        _faceList[0].SetActive(true);
    }
}
