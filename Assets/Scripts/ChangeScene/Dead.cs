using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
