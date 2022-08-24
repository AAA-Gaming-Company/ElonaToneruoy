using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendManager : MonoBehaviour
{
    public int friendsToSpawn = 4;
    public GameObject player;
    public GameObject friendPrefab;

    public TextMeshProUGUI friendCounter;
    private int friendCount;

    public List<FriendBrain> friends = new List<FriendBrain>();

    public GameStateManager gameStateManager;

    private void Start()
    {
        for (int i = 0; i < friendsToSpawn; i++)
        {
            GameObject friend = Instantiate(friendPrefab, player.transform.position, Quaternion.identity, transform);

            FriendBrain brain = friend.GetComponent<FriendBrain>();
            brain.SetupBrain(player.transform, this);
            friends.Add(brain);
        }

        friendCount = friendsToSpawn;
        friendCounter.SetText(friendCount.ToString());
    }

    public IEnumerator FriendEaten(GameObject friend)
    {
        friends.Remove(friend.GetComponent<FriendBrain>());
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