using UnityEngine;
using UnityEngine.SceneManagement;

public class exitButton : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        // Назначаем активной текущую сцену
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(currentScene);

        // На всякий случай сбрасываем Time.timeScale
        Time.timeScale = 1f;

        // Загружаем главное меню
        SceneManager.LoadScene(0); // или по индексу
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
