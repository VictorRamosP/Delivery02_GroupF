using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;
    public float Speed = 2f;

    private float _timer;
    private Transform _player;
    private Vector2 _target;
    private Rigidbody2D _rb;

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D

        Vector2 startPos = animator.transform.position;
        _target = startPos + new Vector2(Random.Range(-1f, 1f) * 4, Random.Range(-1f, 1f) * 4);
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", !timeUp);

        // Move
        if (_rb != null)
        {
            Vector2 dir = (_target - _rb.position).normalized;
            _rb.MovePosition(_rb.position + dir * Speed * Time.deltaTime); 
        }
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime;
        return (_timer > StayTime);
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        return (dist < VisionRange);
    }
}
