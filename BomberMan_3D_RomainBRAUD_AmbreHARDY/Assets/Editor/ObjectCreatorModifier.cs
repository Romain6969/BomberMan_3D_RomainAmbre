using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectCreatorModifier : EditorWindow
{
    public int selectedIndex0 = 0;
    public string[] objectOptions0 = { "2D", "3D" };

    public int selectedIndex = 0;
    public string[] objectOptions = { "Creator", "Modifier" };

    public int selectedIndex1 = 0;
    public string[] objectOptions1 = { "Square", "Circle", "Capsule", "Triangle", "Isometric Diamond", "Hexagon Flat Top", "Hexagon Point Top", "9-Sliced", "Star", "Arrow" };

    public int selectedIndex2 = 0;
    public string[] objectOptions2 = { "Cube", "Sphere", "Capsule", "Cylinder", "Pyramide", "Cone", "Torus" };

    public float x;
    public float y;
    public float z;

    public Color objectColor = Color.white;

    public int count = 0;
    public GameObject ObjectGame;

    [MenuItem("Window/MyWindow/Object Creator or Modifier")]
    public static void ShowWindow()
    {
        GetWindow<ObjectCreatorModifier>("Object Creator/Modifier");
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
        count = 0;
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Crée ou modifie des objets plus facilement", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        selectedIndex0 = (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode2D) ? 0 : 1;
        EditorGUILayout.Space();

        selectedIndex = EditorGUILayout.Popup("Choose Mode", selectedIndex, objectOptions);
        EditorGUILayout.Space();

        switch (objectOptions[selectedIndex])
        {
            case "Creator":
                if (objectOptions0[selectedIndex0] == "3D")
                {
                    selectedIndex2 = EditorGUILayout.Popup("Choose Object", selectedIndex2, objectOptions2);
                }
                else
                {
                    selectedIndex1 = EditorGUILayout.Popup("Choose Object", selectedIndex1, objectOptions1);
                }
                EditorGUILayout.Space();
                break;

            case "Modifier":
                foreach (GameObject obj in Selection.gameObjects)
                {
                    count++;

                    if (count <= 1)
                    {
                        x = obj.transform.localScale.x;
                        y = obj.transform.localScale.y;

                        if (objectOptions0[selectedIndex0] == "3D")
                        {
                            z = obj.transform.localScale.z;
                            Renderer renderer = obj.GetComponent<Renderer>();
                            if (renderer != null)
                            {
                                objectColor = renderer.sharedMaterial.color;
                            }
                            count++;
                        }
                        else
                        {
                            SpriteRenderer renderer2D = obj.GetComponent<SpriteRenderer>();
                            if (renderer2D != null)
                            {
                                objectColor = renderer2D.color;
                            }
                        }
                    }

                    if (objectOptions0[selectedIndex0] == "3D")
                    {
                        Renderer renderer2 = obj.GetComponent<Renderer>();
                        if (renderer2 != null)
                        {
                            obj.transform.localScale = new Vector3(x, y, z);
                            Material newMaterial = new Material(renderer2.sharedMaterial) { color = objectColor };
                            renderer2.material = newMaterial;
                        }
                    }
                    else
                    {
                        SpriteRenderer renderer2D2 = obj.GetComponent<SpriteRenderer>();
                        if (renderer2D2 != null)
                        {
                            obj.transform.localScale = new Vector2(x, y);
                            renderer2D2.color = objectColor;
                        }
                    }
                }
                break;
        }

        x = EditorGUILayout.FloatField("X Scale", x);
        y = EditorGUILayout.FloatField("Y Scale", y);
        if (objectOptions0[selectedIndex0] == "3D")
        {
            z = EditorGUILayout.FloatField("Z Scale", z);
        }
        EditorGUILayout.Space();

        objectColor = EditorGUILayout.ColorField("Choose Color", objectColor);
        EditorGUILayout.Space();

        if (objectOptions[selectedIndex] == "Creator")
        {
            if (GUILayout.Button("Create Object"))
            {
                CreateObject();
            }
        }
    }

    private void CreateObject()
    {
        if (objectOptions0[selectedIndex0] == "3D")
        {
            string selectedObject = objectOptions2[selectedIndex2];
            GameObject createdObject = null;

            switch (selectedObject)
            {
                case "Cube":
                    createdObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case "Sphere":
                    createdObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                case "Capsule":
                    createdObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;
                case "Cylinder":
                    createdObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case "Pyramide":
                    createdObject = Create3DCustom("Pyramide");
                    break;
                case "Cone":
                    createdObject = Create3DCustom("Cone");
                    break;
                case "Torus":
                    createdObject = Create3DCustom("Torus");
                    break;
            }

            if (createdObject != null)
            {
                createdObject.transform.position = Vector3.zero;
                createdObject.transform.localScale = new Vector3(x, y, z);

                Renderer renderer = createdObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material newMaterial = new Material(renderer.sharedMaterial) { color = objectColor };
                    renderer.material = newMaterial;
                }
            }
        }
        else
        {
            string selectedObject = objectOptions1[selectedIndex1];
            GameObject emptyObject = new GameObject(selectedObject);
            SpriteRenderer renderer = emptyObject.AddComponent<SpriteRenderer>();

            switch (selectedObject)
            {
                case "Square":
                    renderer.sprite = Search2D("Square");
                    break;
                case "Circle":
                    renderer.sprite = Search2D("Circle");
                    break;
                case "Capsule":
                    renderer.sprite = Search2D("Capsule");
                    break;
                case "Triangle":
                    renderer.sprite = Search2D("Triangle");
                    break;
                case "Isometric Diamond":
                    renderer.sprite = Search2D("Isometric Diamond");
                    break;
                case "Hexagon Flat Top":
                    renderer.sprite = Search2D("Hexagon Flat-Top");
                    break;
                case "Hexagon Point Top":
                    renderer.sprite = Search2D("Hexagon Pointed-Top");
                    break;
                case "9-Sliced":
                    renderer.sprite = Search2D("9-Sliced");
                    renderer.drawMode = SpriteDrawMode.Tiled;
                    break;
                case "Star":
                    renderer.sprite = Search2D("Star_0");
                    break;
                case "Arrow":
                    renderer.sprite = Search2D("Arrow_0");
                    break;
            }

            if (renderer != null)
            {
                renderer.transform.position = Vector3.zero;
                renderer.transform.localScale = new Vector2(x, y);
                renderer.color = objectColor;
            }
        }
    }

    public GameObject Create3DCustom(string name)
    {
        GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        createdObject.transform.Rotate(Vector3.left, 90);
        createdObject.AddComponent<MeshCollider>();
        BoxCollider collider = createdObject.GetComponent<BoxCollider>();
        Destroy(collider);
        MeshFilter renderer = createdObject.GetComponent<MeshFilter>();
        renderer.mesh = Search3D(name);
        createdObject.name = name;
        return createdObject;
    }

    public Sprite Search2D(string name)
    {
        string relativePath = "Resources/2D";
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.png");
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

                if (sprite != null && sprite.name == name)
                {
                    Debug.Log($"Sprite trouvé : {sprite.name}");
                    return sprite;
                }
            }
        }
        else
        {
            Debug.LogError("Dossier introuvable : " + fullPath);
        }

        Debug.LogError($"Sprite '{name}' non trouvé.");
        return null;
    }

    public Mesh Search3D(string name)
    {
        string relativePath = "Resources/3D";
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath, "*.fbx");
            foreach (string file in files)
            {
                string assetPath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);

                if (mesh != null && mesh.name == name)
                {
                    Debug.Log($"Sprite trouvé : {mesh.name}");
                    return mesh;
                }
            }
        }
        else
        {
            Debug.LogError("Dossier introuvable : " + fullPath);
        }

        Debug.LogError($"Sprite '{name}' non trouvé.");
        return null;
    }
}
