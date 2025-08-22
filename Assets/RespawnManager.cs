using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;
    private Vector3 respawnPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetRespawnPoint(Vector3 point)
    {
        respawnPoint = point;
    }
    public Vector3 GetRespawnPoint() => respawnPoint;

    public void Respawn(GameObject player)
    {
        SceneManager.LoadScene(1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
