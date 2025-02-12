using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    private Transform target;
    private Vector2 moveDirection;
    private VisionDetector _visionDetector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _visionDetector = GetComponent<VisionDetector>();
    }
    private void Update()
    {
        if (target != null && _visionDetector.currentState == VisionDetector.EnemyState.Chasing)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {
        if (target != null && _visionDetector.currentState == VisionDetector.EnemyState.Chasing)
        {
            Vector2 newPosition = _rb.position + moveDirection * speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}