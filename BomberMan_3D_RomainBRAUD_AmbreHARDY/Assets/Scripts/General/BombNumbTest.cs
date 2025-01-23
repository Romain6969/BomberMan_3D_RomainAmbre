using System.Collections;
using NUnit.Framework;
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
        IAAttack attack = Bomb.GetComponent<IAAttack>();

        int InitialNumberBomb = 1;
        int NumberBombLeft = 0;

        // -- Act --
        attack.Bomb(InitialNumberBomb);
        yield return new WaitForSeconds(1);

        // -- Assert --
        Assert.That(attack.IaMovement.NumberBomb, Is.EqualTo(NumberBombLeft));
        
    }
}
