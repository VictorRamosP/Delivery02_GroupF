using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsWall;
    public float Speed;

    void Update()
    {
        if (!EdgeDetected()) Flip();

        Move();
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, Vector2.right, 1.5f, WhatIsWall);

        return (hit.collider == null);
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && EdgeDetected())
        {
            Flip();
        }
       
    }
}
