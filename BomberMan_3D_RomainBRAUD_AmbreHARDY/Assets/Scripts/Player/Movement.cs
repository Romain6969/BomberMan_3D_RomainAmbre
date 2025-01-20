using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _maxSpeed = 6;
    private float currentSpeed;
    [SerializeField] public Vector2 CurrentMovement { get; set; }
    [SerializeField] public bool IsMoving { get; set; } = true;
    [SerializeField] public bool CanMove { get; set; } = true;


    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!IsMoving) return;

        CurrentMovement = context.ReadValue<Vector2>();

        if (context.performed)
        {
            StartCoroutine(Wait());
        }
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            Vector3 mouvement = new Vector3(CurrentMovement.x, 0, CurrentMovement.y);
            mouvement.Normalize();
            transform.Translate(currentSpeed * mouvement * Time.fixedDeltaTime, Space.World);

            if (mouvement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(mouvement, Vector2.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
            }
        }
    }

    IEnumerator Wait()
    {
        currentSpeed = _maxSpeed;
        yield return new WaitForSeconds(1);
        currentSpeed = _speed;
    }
}
