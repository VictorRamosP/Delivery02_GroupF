using UnityEngine;

public class VisionGizmo : MonoBehaviour
{
    [SerializeField]
    private float VisionRange;
    [SerializeField]
    private float PlayerDistance;
    public GameObject Alert;
    private Transform _player;

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        VisionRange = GetComponent<Animator>()
            .GetBehaviour<IdleBehaviour>().VisionRange;
        Alert.SetActive(false);

    }

    private void Update()
    {
        if (_player != null)
        {
            PlayerDistance = Vector3.Distance(transform.position, _player.position);
            if (PlayerDistance < VisionRange)
            {
                Alert.SetActive(true);
            }
            else
            {
                Alert.SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
        Gizmos.color = Color.yellow;
        if (_player != null) Gizmos.DrawLine(transform.position, _player.position);
        Gizmos.color = Color.white;
    }
}
