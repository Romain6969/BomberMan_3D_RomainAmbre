using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelDesigner : EditorWindow
{
    public int Width = 0;
    public int Length = 0;
    public int Space = 0;

    public int NumberWall = 0;
    public int NumberBreakableWall = 0;
    public int NumberSpawnPlayer = 0;

    public GameObject WallPrefab = null;
    public GameObject BreakableWallPrefab = null;
    public GameObject SpawnPlayerPrefab = null;
    public GameObject SpawnMapPrefab = null;

    private List<GameObject> _listBaseMap = new List<GameObject>();

    [MenuItem("Window/MyWindow/Level Designer")]
    public static void ShowWindow()
    {
        GetWindow<LevelDesigner>("Level Designer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Crée une base de niveau", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        Width = EditorGUILayout.IntField("Width", Width);
        Length = EditorGUILayout.IntField("Length", Length);
        Space = EditorGUILayout.IntField("Space", Space);
        EditorGUILayout.Space();

        NumberWall = EditorGUILayout.IntField("Wall Number", NumberWall);
        NumberBreakableWall = EditorGUILayout.IntField("Breakable Wall Number", NumberBreakableWall);
        NumberSpawnPlayer = EditorGUILayout.IntField("Player Spawn Number", NumberSpawnPlayer);
        EditorGUILayout.Space();

        WallPrefab = EditorGUILayout.ObjectField("Wall Prefab", WallPrefab, typeof(GameObject), true) as GameObject;
        BreakableWallPrefab = EditorGUILayout.ObjectField("Breakable Wall Prefab", BreakableWallPrefab, typeof(GameObject), true) as GameObject;
        SpawnPlayerPrefab = EditorGUILayout.ObjectField("Player Spawn Prefab", SpawnPlayerPrefab, typeof(GameObject), true) as GameObject;
        SpawnMapPrefab = EditorGUILayout.ObjectField("Map Spawn Prefab", SpawnMapPrefab, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate New Map"))
        {
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        CleanNullReferences(); // Avant de commencer, nettoyez les références nulles.

        if (WallPrefab == null || SpawnMapPrefab == null)
        {
            Debug.LogError("Wall Prefab or Spawn Map Prefab is not assigned!");
            return;
        }

        GameObject parentObject = new GameObject("GeneratedGrid");

        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Length; z++)
            {
                Vector3 position = new Vector3(x * Space, 0, z * Space);
                GameObject instance = Instantiate(SpawnMapPrefab, position, Quaternion.identity);
                instance.transform.parent = parentObject.transform;
                _listBaseMap.Add(instance); // Ajouter à la liste.
            }
        }

        GenerateWalls(parentObject);

        for (int i = 0; i < NumberSpawnPlayer; i++)
        {
            CleanNullReferences(); // Assurez-vous que la liste est valide avant chaque itération.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = Instantiate(SpawnPlayerPrefab);
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
            DestroyImmediate(_listBaseMap[choose]);
            _listBaseMap.RemoveAt(choose);
        }

        for (int i = 0; i < NumberWall; i++)
        {
            CleanNullReferences(); // Vérifiez la liste avant chaque itération.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = Instantiate(WallPrefab);
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
            DestroyImmediate(_listBaseMap[choose]);
            _listBaseMap.RemoveAt(choose);
        }

        for (int i = 0; i < NumberBreakableWall; i++)
        {
            CleanNullReferences(); // Vérifiez la liste avant chaque itération.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = Instantiate(BreakableWallPrefab);
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
            DestroyImmediate(_listBaseMap[choose]);
            _listBaseMap.RemoveAt(choose);
        }

        Debug.Log("Grid and walls generated successfully!");
    }


    private void GenerateWalls(GameObject parentObject)
    {
        HashSet<Vector3> wallPositions = new HashSet<Vector3>();

        // Génération des murs du haut et du bas
        for (int x = 0; x < Width; x++)
        {
            Vector3 topPosition = new Vector3(x * Space, 0, 0);
            wallPositions.Add(topPosition);
            GameObject topWall = Instantiate(WallPrefab, topPosition, Quaternion.identity);
            topWall.transform.parent = parentObject.transform;
            OnDestroyate(topPosition);

            Vector3 bottomPosition = new Vector3(x * Space, 0, (Length - 1) * Space);
            wallPositions.Add(bottomPosition);
            GameObject bottomWall = Instantiate(WallPrefab, bottomPosition, Quaternion.identity);
            bottomWall.transform.parent = parentObject.transform;
            OnDestroyate(bottomPosition);
        }

        // Génération des murs des côtés gauche et droit
        for (int z = 0; z < Length; z++)
        {
            Vector3 leftPosition = new Vector3(0, 0, z * Space);
            wallPositions.Add(leftPosition);
            GameObject leftWall = Instantiate(WallPrefab, leftPosition, Quaternion.identity);
            leftWall.transform.parent = parentObject.transform;
            OnDestroyate(leftPosition);

            Vector3 rightPosition = new Vector3((Width - 1) * Space, 0, z * Space);
            wallPositions.Add(rightPosition);
            GameObject rightWall = Instantiate(WallPrefab, rightPosition, Quaternion.identity);
            rightWall.transform.parent = parentObject.transform;
            OnDestroyate(rightPosition);
        }
    }

    private void OnDestroyate(Vector3 position)
    {
        for (int i = _listBaseMap.Count - 1; i >= 0; i--) // Boucle inversée
        {
            if (_listBaseMap[i] != null && _listBaseMap[i].transform.position == position) // Vérifier si l'objet est valide
            {
                DestroyImmediate(_listBaseMap[i]); // Supprimer immédiatement l'objet
                _listBaseMap.RemoveAt(i); // Retirer de la liste
                CleanNullReferences();
            }
        }
    }

    private void CleanNullReferences()
    {
        _listBaseMap.RemoveAll(item => item == null);
    }
}
