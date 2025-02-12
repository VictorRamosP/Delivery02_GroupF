using System.Collections.Generic;
using UnityEngine;


public class VisionDetector : MonoBehaviour
{
    public enum EnemyState { Patrolling, Chasing }
    public EnemyState currentState = EnemyState.Patrolling;

    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;
    public ChasePlayer _chasePlayer;
    private Transform currentTarget;
    public float maxdistance;
    public GameObject Alert;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        Gizmos.color = Color.red;

        var direction = Quaternion.AngleAxis(VisionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);
        var direction2 = Quaternion.AngleAxis(-VisionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);
        Gizmos.color = Color.white;
    }

    void Update()
    {
        switch (currentState) 
        {
            case EnemyState.Patrolling:
                Patrol();
            break;

            case EnemyState.Chasing:
                Chase();
            break;
        }
    }
    public void ChangeState(EnemyState newState) 
    {
        currentState = newState;
    }
    private void Patrol() {
        Transform[] detectedPlayers = DetectPlayers();
        if (detectedPlayers.Length > 0)
        {
            Transform player = detectedPlayers[0];
            currentTarget = player;
            _chasePlayer.SetTarget(player);
            Debug.Log("Player detected");
            Alert.SetActive(true);
            audioManager.PlaySFX(audioManager.Detection);
            ChangeState(EnemyState.Chasing);
        }
    }
    public void Chase() 
    {
        if (currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.position);
            if (distance > maxdistance)
            {
                Debug.Log("Sa perdio");
                Alert.SetActive(false);

                _chasePlayer.SetTarget(null);
                currentTarget = null;
                ChangeState(EnemyState.Patrolling);
            }
        }
    }
    private Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();
        if (PlayerInRange(ref players) && PlayerInAngle(ref players) && !this)
        {
            PlayerIsVisible(ref players);
        }
        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);
        foreach (var item in playerColliders)
        {
            players.Add(item.transform);
        }
        return players.Count > 0;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        players.RemoveAll(p => GetAngle(p) > VisionAngle / 2);
        return players.Count > 0;
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        return Vector2.Angle(targetDir, transform.right);
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        players.RemoveAll(p => !IsVisible(p));
        return players.Count > 0;
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, DetectionRange, WhatIsVisible);
        return hit.collider != null && hit.collider.transform == target;
    }
}