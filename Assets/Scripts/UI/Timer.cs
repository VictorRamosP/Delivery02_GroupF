using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0;

    public TextMeshProUGUI timerText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        UpdateTime();
    }

    void UpdateTime()
    {
        int min = Mathf.RoundToInt(currentTime/60);
        int sec = Mathf.RoundToInt(currentTime%60);
        timerText.text= string.Format("{0:00}:{1:00}",min,sec);
    }
}
