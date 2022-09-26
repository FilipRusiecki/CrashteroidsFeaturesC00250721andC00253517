using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }
    [UnityTest]
    public IEnumerator LeftMovementWorks()
    {

        float initialXpos = game.GetShip().transform.position.x;
        game.GetShip().MoveLeft();
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(initialXpos, game.GetShip().transform.position.x);
    }

    [UnityTest]
    public IEnumerator RightMovementWorks()
    {

        float initialXpos = game.GetShip().transform.position.x;
        game.GetShip().MoveRight();
        yield return new WaitForSeconds(0.1f);

        Assert.Less(initialXpos, game.GetShip().transform.position.x);
    }

    [UnityTest]
    public IEnumerator ScoreResetOnNewGame()
    {
        game.NewGame();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(game.score, 0);
    }


    [UnityTest]
    public IEnumerator CheckBoostRight()
    {
        float initialXpos = game.GetShip().transform.position.x;
        game.GetShip().MoveRight();
        yield return new WaitForSeconds(0.1f);
        //Assert.Less(initialXpos, game.GetShip().transform.position.x);
        game.GetShip().resetPos();


        game.GetShip().boostRight();
        float initialXpos2 = game.GetShip().transform.position.x;
        yield return new WaitForSeconds(0.1f);
        //Assert.Less(initialXpos2, game.GetShip().transform.position.x);
        Assert.Greater(initialXpos2, initialXpos);
    }

    [UnityTest]
    public IEnumerator CheckBoostLeft()
    {
        float initialXpos = game.GetShip().transform.position.x;
        game.GetShip().MoveLeft();
        yield return new WaitForSeconds(0.1f);

        game.GetShip().resetPos();

        game.GetShip().boostLeft();
        float initialXpos2 = game.GetShip().transform.position.x;
        yield return new WaitForSeconds(0.1f);
  
        Assert.Less(initialXpos2, initialXpos);

    [UnityTest]
    public IEnumerator UpMovementWorks()
    {
        float initialYpos = game.GetShip().transform.position.y;
        game.GetShip().MoveUp();
        yield return new WaitForSeconds(0.1f);

        Assert.Less(initialYpos, game.GetShip().transform.position.y);
    }

    [UnityTest]
    public IEnumerator DownMovementWorks()
    {
        float initialYpos = game.GetShip().transform.position.y;
        game.GetShip().MoveDown();
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(initialYpos, game.GetShip().transform.position.y);

    }
}
