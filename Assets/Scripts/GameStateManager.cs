using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject player;
    public GameObject enemies;

    private void Awake()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void LevelFailed()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Destroy(player.GetComponent<PlayerController>());
    }

    public void LevelComplete()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        Destroy(player.GetComponent<PlayerController>());
        Destroy(enemies);
    }

    public void Restart()
    {
#warning Hihi Alex doesn't know how to do that
    }

    public void NextLevel()
    {
#warning Alex also doesn't know how to do this
    }
}
