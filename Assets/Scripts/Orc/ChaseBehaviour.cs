using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    public float Speed = 2;
    public float VisionRange;

    private Transform _player;
    private Rigidbody2D _rb; 
    
    AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D
        audioManager.PlaySFX(audioManager.Detection);
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);

        // Move to player
        if (_rb != null && _player != null)
        {
            Vector2 dir = (_player.position - animator.transform.position).normalized;
            _rb.MovePosition(_rb.position + dir * Speed * Time.deltaTime); 
        }
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        return dist < VisionRange;
    }
}
