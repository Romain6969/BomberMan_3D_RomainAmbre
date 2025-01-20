using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.VFX;
using System.IO;
using System.Linq;

public class VFXGetter : EditorWindow
{
    public int Count = 0;

    public string[] VFXNames = null;

    public int Index = 0;
    public VisualEffect[] VFXs = null;



    [MenuItem("Window/MyWindow(Romain)/VFXGetter")]
    public static void ShowWindow()
    {
        GetWindow<VFXGetter>("VFXGetter");
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;
    }

    private void OnSelectionChanged()
    {
        Count = 0;
        Repaint();
    }

    void OnGUI()
    {
        GUILayout.Label("Choose the VFX you want to create", EditorStyles.boldLabel);
        EditorGUILayout.Space();


        EditorGUILayout.Popup("Choose VFX", Index, VFXNames);

        if (GUILayout.Button("CREATE"))
        {

        }
    }

    public VisualEffect SearchVFX(string name)
    {
        string relativePath = "VFX";
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.vfx");
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                VisualEffect vfx = AssetDatabase.LoadAssetAtPath<VisualEffect>(assetPath);

                if (vfx != null && vfx.name == name)
                {
                    Debug.Log($"VFX trouvé : {vfx.name}");
                    return vfx;
                }
            }
        }
        else
        {
            Debug.LogError("Dossier introuvable : " + fullPath);
        }

        Debug.LogError($"VFX '{name}' non trouvé.");
        return null;
    }

    public string[] SearchVFXNames()
    {
        string relativePath = "VFX";
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        string[] names = null;

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.vfx");
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                VisualEffect vfx = AssetDatabase.LoadAssetAtPath<VisualEffect>(assetPath);

                if (vfx != null)
                {
                    Debug.Log($"VFX trouvé : {vfx.name}");
                    names[0] += vfx.name;
                }
            }
        }
        else
        {
            Debug.LogError("Dossier introuvable : " + fullPath);
        }

        Debug.LogError($"VFX '{name}' non trouvé.");
        return null;
    }
}
