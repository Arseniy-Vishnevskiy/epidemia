using UnityEngine;
using UnityEngine.InputSystem.UI;

public class SpawnManager : MonoBehaviour
{
    public EntrancePoint[] entrances;
    public GameObject player;
    private void Start()
    {
        string entranceId = SceneTransitionData.entranceId;
        foreach(var entry in entrances)
        {
            player.transform.position = entry.spawnPoint.position;
        }
        player.transform.position = entrances[0].spawnPoint.position;
    }
}
