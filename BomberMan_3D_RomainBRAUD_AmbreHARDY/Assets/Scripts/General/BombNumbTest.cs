using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
public class BombNumbTest
{
    [UnityTest]
    public IEnumerator Attack_ShouldDecreaseNumberOfBombs()
    {
        
        // -- Arrange --
        GameObject Bomb = new GameObject();
        Bomb.AddComponent<IAAttack>();
        Bomb.AddComponent<ObjectPoolBomb>();
        IAAttack attack = Bomb.GetComponent<IAAttack>();
        ObjectPoolBomb Test = Bomb.GetComponent<ObjectPoolBomb>();

        Test.Prefab = new GameObject();
        int InitialNumberBomb = 1;
        int NumberBombLeft = 0;

        // -- Act --
        // Ca ne marche pas, car pour je ne sais qu'elle raison, Le test runner c'est dit de checker le FixedUpdate de IAAttack
        // Alors que je ne lui demande que de checker la fonction Bomb de IAAttack.
        attack.Bomb(InitialNumberBomb);
        yield return new WaitForSeconds(1);

        // -- Assert --
        Assert.That(attack.IaMovement.NumberBomb, Is.EqualTo(NumberBombLeft));
        
    }
}
