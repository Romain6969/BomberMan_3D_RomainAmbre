using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.VFX;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class VFXGetter : EditorWindow
{
    public int Count = 0;

    public string[] VFXNames = new string[10];
    public string[] Names = new string[2];

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
        VFXNames = SearchVFXNames();
        GUILayout.Label("Choose the VFX you want to create", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.Popup("Choose VFX", Index, VFXNames);

        if (GUILayout.Button("CREATE"))
        {
            if (VFXNames[Index] == VFXNames[0])
            {
                Instantiate(SearchVFX(VFXNames[0]));
            }
            if (VFXNames[Index] == VFXNames[1])
            {
                Instantiate(SearchVFX(VFXNames[1]));
            }
        }
    }

    public VisualEffectAsset SearchVFX(string name)
    {
        string relativePath = "VFX";
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.vfx");
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                VisualEffectAsset vfx = AssetDatabase.LoadAssetAtPath<VisualEffectAsset>(assetPath);
                

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
        Index = 0;
        string relativePath = "VFX";
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.vfx");
            Names = new string[files.Length];
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                VisualEffectAsset vfx = AssetDatabase.LoadAssetAtPath<VisualEffectAsset>(assetPath);
                Debug.Log(assetPath);

                if (vfx != null)
                {
                    Debug.Log($"VFX trouvé : {vfx.name}");
                    Names[Index] += vfx.name;
                    Index++;
                }
            }
            return Names;
        }
        else
        {
            Debug.LogError("Dossier introuvable : " + fullPath);
        }

        Debug.LogError($"VFX '{name}' non trouvé.");
        return null;
    }
}
