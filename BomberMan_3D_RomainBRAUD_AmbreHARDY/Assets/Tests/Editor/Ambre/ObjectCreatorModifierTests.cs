using NUnit.Framework;
using UnityEngine;

public class ObjectCreatorModifierTests
{
    private ObjectCreatorModifier objectCreatorModifier;

    [SetUp]
    public void SetUp()
    {
        objectCreatorModifier = new ObjectCreatorModifier
        {
            selectedIndex0 = 1, // Mode 3D
            selectedIndex2 = 0, // Cube
            x = 1f,
            y = 1f,
            z = 1f,
            objectColor = Color.red
        };
    }

    [Test]
    public void TestCreateCube()
    {
        objectCreatorModifier.CreateObject();
        GameObject createdObject = GameObject.Find("Cube");

        Assert.NotNull(createdObject);
        Assert.AreEqual(Vector3.one, createdObject.transform.localScale, "La taille de l'objet créé n'est pas correcte.");
    }

    [Test]
    public void TestCubeColor()
    {
        objectCreatorModifier.CreateObject();
        GameObject createdObject = GameObject.Find("Cube");
        Renderer renderer = createdObject.GetComponent<Renderer>();

        Assert.NotNull(renderer, "Le Renderer de l'objet n'existe pas.");
        Assert.AreEqual(Color.red, renderer.sharedMaterial.color, "La couleur de l'objet créé n'est pas correcte.");
    }

    [TearDown]
    public void TearDown()
    {
        // Nettoyage des objets créés dans la scène
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
        {
            Object.DestroyImmediate(obj);
        }
    }
}
