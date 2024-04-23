using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameSystem gameSystem;
    private void Awake()
    {
        gameSystem = GameObject.Find("GameObject").GetComponent<GameSystem>();
        Debug.Log(gameSystem);
        gameSystem.curBallCnt++;
    }
    private void OnDestroy()
    {
        gameSystem.curBallCnt--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Candy"))
        {
            gameSystem.ballCnt++;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.CompareTag("Bottom"))
        {
            Debug.Log("Bottom");
            Destroy(gameObject);
        }
    }

}
