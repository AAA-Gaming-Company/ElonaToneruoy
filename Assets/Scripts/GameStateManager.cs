using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject player;
    public GameObject enemies;
    public GameObject startScreen;

    private void Awake()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        startScreen.SetActive(true);

        enemies.SetActive(false);
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

    public void StartGame()
    {
        startScreen.SetActive(false);

        enemies.SetActive(true);
    }
}
