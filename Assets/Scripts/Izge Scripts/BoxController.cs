using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Rigidbody2D rb2d;
    GameObject level;
    LevelManager levelManager;

    bool stick = false;
    bool falling = false;

    Vector2 topRightCorner;
    Vector2 edgeVector;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        level = GameObject.Find("Level");
        levelManager = GameObject.Find("Level").GetComponent<LevelManager>();

        topRightCorner = new Vector2(1, 1);
        edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.inputEnabled && Input.GetKeyDown(KeyCode.Space) && !falling)
        {
            rb2d.gravityScale = levelManager.gravity;
            level.GetComponent<AudioSource>().pitch = Random.Range(1, 4f);
            level.GetComponent<AudioSource>().Play();
            falling = true;
        }

        if (!stick && transform.position.y < -edgeVector.y )
        {
            levelManager.EndGame();
        }

        //if (stick && transform.position.y < -edgeVector.y)
        //{
        //    Destroy(GetComponent<BoxCollider2D>());
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AdjustScreenBounds();
        levelManager.MoveCameraUpwards();
        levelManager.IncreasePaddleSpeed();

        Vector3 h1 = collision.gameObject.GetComponent<Collider2D>().bounds.extents;
        Vector3 h2 = transform.GetComponent<Collider2D>().bounds.extents;

        if (transform.position.y - collision.transform.position.y >= (h1.y + h2.y) / 2)
        {
            levelManager.PlayBlockFallSound();
            stick = true;
            rb2d.isKinematic = true;
            Destroy(this);
            transform.SetParent(collision.transform);

            levelManager.UpdateScore(1);
        }
    }

    void AdjustScreenBounds()
    {
        topRightCorner = new Vector2(1, 1);
        edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
    }
}
