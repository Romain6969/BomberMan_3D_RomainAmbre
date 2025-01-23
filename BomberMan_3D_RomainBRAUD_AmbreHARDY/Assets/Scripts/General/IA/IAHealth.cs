using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IAHealth : MonoBehaviour
{
    [Header("UI")]
    public GameObject PanelWin;
    [SerializeField] private Image _healthImage;
    [SerializeField] private AnimationCurve _curve;

    [Header("Fin du jeu")]
    public Movement PlayerMovement;
    [SerializeField] private DeadState _deadState;

    [Header("Points de vie de l'IA")]
    public int HP = 3;

    [Header("Invincibilité de l'IA")]
    [SerializeField] private bool _invincibility;
    [SerializeField] private MeshRenderer _renderer;
    

    private void Update()
    {
        if (HP <= 0)
        {
            IAStateMachine.Instance.OnTransition(_deadState);
            HP = 3;
        }
    }

    public void IsDead()
    {
        PanelWin.SetActive(true);
        foreach (GameObject i in FindObjectsOfType<GameObject>())
        {
            if (i.tag == "ObjetcBomb" || i.tag == "Bomb" || i.tag == "Explosion")
            {
                Destroy(i);
            }
        }
        PlayerMovement.CanMove = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion" && !_invincibility)
        {
            HP--;
            float targetFillAmount = Mathf.InverseLerp(0, 3, HP);
            _healthImage.DOFillAmount(targetFillAmount, 0.5f).SetEase(_curve);
            StartCoroutine(Invicibility());
        }
    }

    IEnumerator Invicibility()
    {
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
    }

}
