using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    public int ballCnt;
    public int blockCnt;
    public GameObject Ball;
    public GameObject Brick;
    public GameObject Candy;
    public float ballSpeed;
    public float ballSummonSpeed;
    Vector3 brickLeftTopPos = new Vector3(-2.773f,4.117f,0);
    Vector3 ballPoint = new Vector3(-0.749f,-3.941f,0);
    public int curBallCnt;
    void Start()
    {
        StartCoroutine(GameUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GameUpdate()
    {
        while (true)
        {
            GameObject firstBall = Instantiate(Ball,ballPoint,Quaternion.identity);
            StartCoroutine(CalcBrick());
            yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
            Destroy(firstBall);
            yield return StartCoroutine(SummonBall());
            yield return new WaitUntil(()=>curBallCnt==0);
            blockCnt++;
            yield return null;
        }
    }
    IEnumerator CalcBrick()
    {
        int randomMask = Random.Range(1,(1<<5)-1);
        float sideX = Brick.GetComponent<BoxCollider2D>().transform.localScale.x;
        float sideY = Brick.GetComponent<BoxCollider2D>().transform.localScale.y;
        int idx = Random.Range(0,4);
        Instantiate(Candy,brickLeftTopPos+Vector3.right*idx,Quaternion.identity);
        randomMask &= ~(1 << idx);
        for(int i = 0; i < 5; i++)
        {
            if ((randomMask & (1 << i)) == 0)
                continue;
            Instantiate(Brick,brickLeftTopPos+Vector3.right*sideX*i,Quaternion.identity);
        } 
        foreach(GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            brick.transform.Translate(Vector3.down*sideY);
        }
        foreach(GameObject candy in GameObject.FindGameObjectsWithTag("Candy"))
        {
            candy.transform.Translate(Vector3.down * sideY);
        }
        yield return null;
    }
    IEnumerator SummonBall()
    {
        Vector2 point =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = new Vector2(point.x-ballPoint.x,point.y-ballPoint.y);
        Vector2 dir = diff.normalized;
        int cnt = ballCnt;
        for(int i = 0; i < cnt; i++)
        {
            GameObject tmp = Instantiate(Ball,ballPoint,Quaternion.identity);
            tmp.GetComponent<Rigidbody2D>().velocity = dir*ballSpeed;
            yield return new WaitForSeconds(ballSummonSpeed);
        }
    }
}
