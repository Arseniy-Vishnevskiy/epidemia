using UnityEngine;
using UnityEngine.SceneManagement;

public class exitButton : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        // ��������� �������� ������� �����
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(currentScene);

        // �� ������ ������ ���������� Time.timeScale
        Time.timeScale = 1f;

        // ��������� ������� ����
        SceneManager.LoadScene(0); // ��� �� �������
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
