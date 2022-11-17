using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Finish")){
            Debug.Log(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}
