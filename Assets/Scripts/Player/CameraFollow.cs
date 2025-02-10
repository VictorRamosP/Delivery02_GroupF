using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float YOffset = 1f;
    public Transform Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(Target.position.x, Target.position.y + YOffset,-10F);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);        
    }
}
