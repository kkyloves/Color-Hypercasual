using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameNow()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
