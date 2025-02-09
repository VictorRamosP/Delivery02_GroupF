using UnityEngine;

public class PatrolPoints : MonoBehaviour
{

    public Transform[] patrolPoints;
    public int target;

    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[target].position)
        {
            IncreaseNumTarget();
        }

        RotationEnemy();
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[target].position, speed * Time.deltaTime);
    }

    public void RotationEnemy()
    {
        Vector2 direction = (patrolPoints[target].position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void IncreaseNumTarget()
    {
        target++;
        if (target >= patrolPoints.Length)
        {
            target = 0;
        }
    }

}
