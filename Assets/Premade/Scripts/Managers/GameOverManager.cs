using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5;
    private float restartTimer;
    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("Gameover");
            restartTimer += Time.deltaTime;
        }
        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene(0);
        }
    }
}
