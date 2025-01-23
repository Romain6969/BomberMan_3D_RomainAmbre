using Unity.Mathematics;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    private quaternion _quaternion;

    private void Start()
    {
        _quaternion = transform.rotation;
    }

    void Update()
    {
        transform.rotation = _quaternion;
    }
}
