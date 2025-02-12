using System.Diagnostics;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;
    public float Speed = 2f;
    public float RaycastDistance;
    public LayerMask WhatIsWall;
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
        
        var transform = animator.transform;
        Vector2 newDirection = new Vector2(Random.Range(-1f, 1f) * 4, Random.Range(-1f, 1f) * 4);
        if (!EdgeDetected(transform.position, transform.up)) {
            if (!EdgeDetected(transform.position, transform.right)) {
                newDirection = Vector2.up + Vector2.right;
                UnityEngine.Debug.Log("UR");                     
            }else if (!EdgeDetected(transform.position, -transform.right)) {
                newDirection = Vector2.up + Vector2.left;
                UnityEngine.Debug.Log("UL");                          
            }                         
        }else if (!EdgeDetected(transform.position, -transform.up)) {
            if (!EdgeDetected(transform.position, transform.right)) {
                newDirection = Vector2.down + Vector2.right;
                UnityEngine.Debug.Log("DR");                        
            }else if (!EdgeDetected(transform.position, -transform.right)) {
                newDirection = Vector2.down + Vector2.left;    
                UnityEngine.Debug.Log("DL");                    
            }                       
        }

        Vector2 startPos = animator.transform.position;
        _target = startPos + newDirection;
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
    private bool EdgeDetected(Vector3 pos, Vector3 _direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, _direction, RaycastDistance, WhatIsWall);
        return (hit.collider == null);
    }
}
