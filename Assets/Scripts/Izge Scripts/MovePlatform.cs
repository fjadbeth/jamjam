using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    float x_bound;
    Rigidbody2D rb2d;

    public float moveSpeed = 1.5f;

    private void Start()
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        x_bound = edgeVector.x - GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(moveSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x >= x_bound)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
        if (transform.position.x <= -x_bound)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
        }
    }
}
