using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 2f;
    private static Rigidbody2D rb;
    [SerializeField]
    public float rotationSpeed = -200.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy1 enemy = collision.GetComponent<Enemy1>();

        if (enemy != null)
        {
            enemy.TakeDamage(50);
        }

        Destroy(gameObject);
    }
}
