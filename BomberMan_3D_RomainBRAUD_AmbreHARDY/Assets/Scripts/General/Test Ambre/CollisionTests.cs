using NUnit.Framework;
using UnityEngine;
using TMPro;

[TestFixture]
public class CollisionTests
{
    private GameObject _collisionGameObject;
    private Collision _collision;
    private UseBomb _useBomb;
    private TMP_Text _textBomb;

    [SetUp]
    public void SetUp()
    {
        _collisionGameObject = new GameObject();
        _collision = _collisionGameObject.AddComponent<Collision>();

        _useBomb = new GameObject().AddComponent<UseBomb>();
        _useBomb.NumberBomb = 0;

        _textBomb = new GameObject().AddComponent<TextMeshProUGUI>();
        typeof(UseBomb).GetField("_textBomb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_useBomb, _textBomb);

        typeof(Collision).GetField("_useBombe", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_collision, _useBomb);
    }

    [Test]
    public void Test_BombCountIncreases_WhenObjectBombCollected()
    {
        var bombObject = new GameObject();
        bombObject.AddComponent<BoxCollider>().tag = "ObjectBomb";

        _collision.SendMessage("OnTriggerEnter", bombObject);

        Assert.AreEqual(1, _useBomb.NumberBomb);
        Assert.AreEqual("1", _textBomb.text);
        Assert.IsFalse(bombObject.activeSelf);
    }

    [Test]
    public void Test_BombCountDoesNotExceedLimit()
    {
        _useBomb.NumberBomb = 3;
        _useBomb.OnActualiseBomb();
        var bombObject = new GameObject();
        bombObject.AddComponent<BoxCollider>().tag = "ObjectBomb";

        _collision.SendMessage("OnTriggerEnter", bombObject);

        Assert.AreEqual(3, _useBomb.NumberBomb);
        Assert.AreEqual("3", _textBomb.text);
        Assert.IsTrue(bombObject.activeSelf);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(_collisionGameObject);
        GameObject.DestroyImmediate(_useBomb.gameObject);
        GameObject.DestroyImmediate(_textBomb.gameObject);
    }
}