using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Rigidbody2D rb2d;
    bool stick = false;
    float gravity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !stick)
        {
            rb2d.gravityScale = gravity;
        }

        if (transform.position.y < -10)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stick)
        {
            return;
        }

        Vector3 h1 = collision.gameObject.GetComponent<Collider2D>().bounds.extents;
        Vector3 h2 = transform.GetComponent<Collider2D>().bounds.extents;

        Debug.Log(h1 + " " + h2 + " " + transform.position.y + " " + collision.transform.position.y);
        Debug.Log(transform.name + " " + collision.transform.name);
        Debug.Log(collision.GetContact(0).point);

        if (transform.position.y - collision.transform.position.y >= (h1.y + h2.y) / 2)
        {
            stick = true;
            Destroy(rb2d);
            transform.SetParent(collision.transform);
        }

       
    }
}
