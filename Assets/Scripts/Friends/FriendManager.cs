using System.Collections;
using TMPro;
using UnityEngine;

public class FriendManager : MonoBehaviour
{
    public int friendsToSpawn = 4;
    public GameObject player;
    public GameObject friendPrefab;

    public TextMeshProUGUI friendCounter;
    private int friendCount;

    public GameStateManager gameStateManager;

    private void Start()
    {
        for (int i = 0; i < friendsToSpawn; i++)
        {
            GameObject friend = Instantiate(friendPrefab, player.transform.position, Quaternion.identity, transform);

            FriendBrain brain = friend.GetComponent<FriendBrain>();
            brain.SetupBrain(player.transform, this);
        }

        friendCount = friendsToSpawn;
        friendCounter.SetText(friendCount.ToString());
    }

    public IEnumerator FriendEaten(GameObject friend)
    {
        Destroy(friend);
        friendCount--;

        friendCounter.SetText(friendCount.ToString());

        if (friendCount == 0)
        {
            gameStateManager.LevelFailed();
        }

        yield return null;
    }
}