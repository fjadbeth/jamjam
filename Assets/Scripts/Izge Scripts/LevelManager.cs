using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject paddle;
    public GameObject spawner;
    public Text scoreText;

    public float gravity = 2f;
    public float heightIncrease = 0.2f;
    public float moveSpeedIncrease = 0.3f;
    public int score = 0;

    public void MoveCameraUpwards()
    {
        Camera.main.transform.position += new Vector3(0, heightIncrease, 0);
        spawner.transform.position += new Vector3(0, heightIncrease, 0);
    }

    public void IncreasePaddleSpeed()
    {
        paddle.GetComponent<MovePlatform>().moveSpeed += moveSpeedIncrease;
        if (paddle.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(moveSpeedIncrease, 0);
        }
        else
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(-moveSpeedIncrease, 0);
        }
    }

    public void UpdateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;
    }
}
