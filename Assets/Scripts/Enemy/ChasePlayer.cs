using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    private Transform target;
    private Vector2 moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
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