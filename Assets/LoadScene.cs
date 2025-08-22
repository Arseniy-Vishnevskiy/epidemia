using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public LoadingScene loader;
    private void OnTriggerEnter(Collider other)
    {
        loader.LoadByIndex(loader.index);
    }
}
