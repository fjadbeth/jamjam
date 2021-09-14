using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0.01f, 1)]
    public float y_axis_adjuster;
    public float spawnTimer = 1f;
    public bool spawning = true;

    Vector2 edgeVector;

    public List<GameObject> prefabs;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        transform.position = new Vector2(0, edgeVector.y * y_axis_adjuster);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (spawning)
        {
            // RaycastHit2D hit = Physics2D.Raycast(new Vector2(-edgeVector.x, transform.position.y), Vector3.right, edgeVector.x*2);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down, Vector3.up, 1.5f);

            if(hit.collider == null)
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnTimer);
        }
        
    }
}
