using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    GameSystem gameSystem;
    int health;
    private void Awake()
    {
        gameSystem = GameObject.Find("GameObject").GetComponent<GameSystem>();
        health = gameSystem.blockCnt;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            health--;
        }
        if (health <= 0)
            Destroy(gameObject);
    }
}
