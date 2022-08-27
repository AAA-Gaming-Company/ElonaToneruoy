using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Transform door;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (EnemyBrain enemy in enemyManager.enemies)
        {
            enemy.transform.position = door.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

        }

        Destroy(gameObject);
    }
}
