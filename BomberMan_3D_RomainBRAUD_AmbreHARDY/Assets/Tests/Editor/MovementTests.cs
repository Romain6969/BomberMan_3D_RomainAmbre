using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class MovementTests
{
    private GameObject testObject;
    private Movement movementScript;

    [SetUp]
    public void SetUp()
    {
        // Crée un objet de test et ajoute le script Movement
        testObject = new GameObject();
        movementScript = testObject.AddComponent<Movement>();
    }

    [TearDown]
    public void TearDown()
    {
        // Détruit l'objet de test après chaque test
        Object.DestroyImmediate(testObject);
    }

    [Test]
    public void OnMovement_ChangesCurrentMovement_WhenCalled()
    {
        // Simule une entrée utilisateur
        Vector2 inputVector = new Vector2(1, 0);
        var context = new InputAction.CallbackContext();
        typeof(InputAction.CallbackContext)
            .GetField("m_ValueData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(context, inputVector);

        // Appelle la méthode OnMovement
        movementScript.OnMovement(context);

        // Vérifie que CurrentMovement a changé
        Assert.AreEqual(inputVector, movementScript.CurrentMovement);
    }

    [UnityTest]
    public IEnumerator Wait_SetsSpeedToMax_AndThenResetsSpeed()
    {
        // Initialise les variables
        movementScript.OnMovement(new InputAction.CallbackContext());
        movementScript.IsMoving = true;

        // Lance la coroutine
        movementScript.StartCoroutine("Wait");

        // Vérifie que la vitesse est initialisée à _maxSpeed
        movementScript.Invoke("Wait", 0);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(movementScript._maxSpeed, movementScript.currentSpeed);

    }
}
