using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public static GameController Instance;
    bool isDead;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Переживает смену сцен
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
