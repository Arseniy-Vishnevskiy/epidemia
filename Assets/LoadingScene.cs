using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public string sceneName;
    public int index;
    public void LoadByIndex(int index)
    {
        FindFirstObjectByType<CameraFading>().LoadScene(index);
    }
}
