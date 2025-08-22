using UnityEngine;
using UnityEngine.SceneManagement;

public class CharMenu : MonoBehaviour
{
    public GameObject menuPanel;
    bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }
    public void ToggleMenu() 
    {
        isOpen = !isOpen;
        menuPanel.SetActive(isOpen);

        if (isOpen)
        {
            UpdateMenu();
        }
    }
    void UpdateMenu() { }
}
