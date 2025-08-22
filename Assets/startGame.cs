using Unity.VisualScripting;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public LoadingScene controller;

    void LoadLv1()
    {
        controller.GetComponent<LoadingScene>().LoadByIndex(0);
    }

}
