using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject paddle;
    public GameObject spawner;
    public Text scoreText;
    public GameObject endScreen;
    public Button replayButton;

    public float gravity = 2f;
    public float heightIncrease = 0.40f;
    public float moveSpeedIncrease = 0.3f;
    public float maxSpeed = 15;
    public int score = 0;
    public bool inputEnabled = true;

    

    private void Start()
    {
        replayButton.onClick.AddListener(ReplayGame);
    }

    public void MoveCameraUpwards()
    {
        Camera.main.transform.position += new Vector3(0, heightIncrease, 0);
        spawner.transform.position += new Vector3(0, heightIncrease, 0);
    }

    public void IncreasePaddleSpeed()
    {
        if(paddle.GetComponent<MovePlatform>().moveSpeed == maxSpeed)
        {
            return;
        }

        paddle.GetComponent<MovePlatform>().moveSpeed += moveSpeedIncrease;
        if (paddle.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(moveSpeedIncrease, 0);
        }
        else
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(-moveSpeedIncrease, 0);
        }

        if(paddle.GetComponent<MovePlatform>().moveSpeed > maxSpeed)
        {
            paddle.GetComponent<MovePlatform>().moveSpeed = maxSpeed;
            if (paddle.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, 0);
            }
            else
            {
                paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(-maxSpeed, 0);
            }
        }
    }

    public void UpdateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;
    }

    public void EndGame()
    {
        inputEnabled = false;
        endScreen.SetActive(true);
        scoreText.gameObject.SetActive(false);
        GameObject.Find("FinalScore").GetComponent<Text>().text = "Your Score: " + score;
    }

    public void ReplayGame()
    {
        endScreen.SetActive(false);
        SceneManager.LoadScene("IzgeScene");
    }

    public void PlayBlockFallSound()
    {
        paddle.GetComponent<AudioSource>().Play();
    }
}
