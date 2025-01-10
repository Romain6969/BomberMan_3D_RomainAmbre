using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectCreatorModifier : EditorWindow
{
    public int selectedIndex = 0;
    public string[] objectOptions = { "Creator", "Modifier" };

    public int selectedIndex2 = 0;
    public string[] objectOptions2 = { "Cube", "Sphere", "Capsule", "Cylinder" };

    public float x;
    public float y;
    public float z;

    public Color objectColor = Color.white;

    public int count = 0;
    public GameObject ObjectGame;

    [MenuItem("Window/MyWindow/Object Creator|Modifier")]
    public static void ShowWindow()
    {
        GetWindow<ObjectCreatorModifier>("Object Creator/Modifier");
    }

    private void OnGUI()
    {
        /*if (Selection.gameObjects != null)
        {
            if (Selection.gameObjects.Contains(ObjectGame))
            {
                count = 0;
            }

            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Selection.gameObjects[i] = null;
            }
        }*/

        GUILayout.Label("Crée ou modifie des objets plus facilement", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        selectedIndex = EditorGUILayout.Popup("Choose Object", selectedIndex, objectOptions);
        EditorGUILayout.Space();
        
        switch (objectOptions[selectedIndex])
        {
            case "Creator":
                selectedIndex2 = EditorGUILayout.Popup("Choose Object", selectedIndex2, objectOptions2);
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
                        z = obj.transform.localScale.z;
                        Renderer renderer = obj.GetComponent<Renderer>();
                        objectColor = renderer.sharedMaterial.color;
                        count++;
                    }

                    obj.transform.localScale = new Vector3(x, y, z);
                    Renderer renderer2 = obj.GetComponent<Renderer>();
                    renderer2.sharedMaterial.color = objectColor;
                }
                break;
        }

        x = EditorGUILayout.FloatField("X Scale", x);
        y = EditorGUILayout.FloatField("Y Scale", y);
        z = EditorGUILayout.FloatField("Z Scale", z);
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
        }

        if (createdObject != null)
        {
            createdObject.transform.position = Vector3.zero;
            createdObject.transform.localScale = new Vector3(x, y, z);  

            Renderer renderer = createdObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material newMaterial = new Material(renderer.sharedMaterial);
                newMaterial.color = objectColor;
                renderer.material = newMaterial;
            }

            Debug.Log($"{selectedObject} créé avec une couleur unique : {objectColor} !");
        }
        else
        {
            Debug.LogError("Impossible de créer l'objet sélectionné.");
        }
    }
}
