using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public string identifier;

    public string GetIdentifier()
    {
        return identifier;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
