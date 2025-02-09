using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    public Transform player; 
    public TextMeshProUGUI distanceText; 

    private Vector2 lastPosition;
    private float totalDistance = 0f;

    void Start()
    {
        if (player != null)
            lastPosition = player.position; 
    }

    void Update()
    {
        if (player != null)
        {
            float distanceMoved = Vector2.Distance(player.position, lastPosition);
            totalDistance += distanceMoved;
            lastPosition = player.position;

            UpdateDistanceUI();
        }
    }

    void UpdateDistanceUI()
    {
        distanceText.text = $"Distancia: {totalDistance:F2}";
    }
}
