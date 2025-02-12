using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsWall;
    public float Speed;
    private VisionDetector _visionDetector;
    public float RaycastDistance;

    private void Awake()
    {
        _visionDetector = GetComponent<VisionDetector>();
    }
    void Update()
    {
        if (!EdgeDetected() && _visionDetector.currentState == VisionDetector.EnemyState.Patrolling) Flip();

        Move();
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, transform.right, RaycastDistance, WhatIsWall);

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
