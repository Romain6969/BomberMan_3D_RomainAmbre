using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform))]
public class ObjectOnGround : Editor
{
    public float raycastDistance = 1000f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Transform selectedTransform = (Transform)target;

        if (GUILayout.Button("Placer au sol"))
        {
            PlaceOnGround(selectedTransform);
        }
    }

    private void PlaceOnGround(Transform transform)
    {
        float height = transform.localScale.y;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            transform.position = hit.point;
            transform.position += new Vector3(0, height/2, 0);
        }
        else
        {
            Debug.LogWarning("Aucune surface d�tect�e");
        }
    }
}
