using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioSource))]
public class AudioSourcePreview : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioSource audioSource = (AudioSource)target;

        if (GUILayout.Button("Prévisualiser le son"))
        {
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Aucun clip");
            }
        }
    }
}
