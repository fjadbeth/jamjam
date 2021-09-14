using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Rigidbody2D rb2d;

    GameObject spawner;
    GameObject paddle;
    GameObject level;

    bool stick = false;
    bool falling = false;
    float gravity = 2f;

    float heightIncrease = 0.2f;
    float moveSpeedIncrease = 0.3f;

    Vector2 topRightCorner;
    Vector2 edgeVector;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        spawner = GameObject.Find("Spawner");
        paddle = GameObject.Find("Paddle");
        level = GameObject.Find("Level");


        topRightCorner = new Vector2(1, 1);
        edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !falling)
        {
            rb2d.gravityScale = gravity;
            level.GetComponent<AudioSource>().pitch = Random.Range(1, 4f);
            level.GetComponent<AudioSource>().Play();
            falling = true;
        }

        if (!stick && transform.position.y < -edgeVector.y )
        {
            SceneManager.LoadScene("IzgeScene");
        }

        //if (stick && transform.position.y < -edgeVector.y)
        //{
        //    Destroy(GetComponent<BoxCollider2D>());
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Camera.main.transform.position += new Vector3(0, heightIncrease, 0);
        spawner.transform.position += new Vector3(0, heightIncrease, 0);

        topRightCorner = new Vector2(1, 1);
        edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        paddle.GetComponent<MovePlatform>().moveSpeed += moveSpeedIncrease;
        if(paddle.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(moveSpeedIncrease, 0);
        }
        else
        {
            paddle.GetComponent<Rigidbody2D>().velocity += new Vector2(-moveSpeedIncrease, 0);
        }

        Vector3 h1 = collision.gameObject.GetComponent<Collider2D>().bounds.extents;
        Vector3 h2 = transform.GetComponent<Collider2D>().bounds.extents;

        if (transform.position.y - collision.transform.position.y >= (h1.y + h2.y) / 2)
        {
            GetComponent<AudioSource>().Play();
            stick = true;
            rb2d.isKinematic = true;
            Destroy(this);
            transform.SetParent(collision.transform);
        }

       
    }
}
