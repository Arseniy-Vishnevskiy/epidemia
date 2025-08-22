using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Transform player;
    int maxHealth;
    int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = player.GetComponent<PlayerCombat>().maxHealt;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.GetComponent<PlayerCombat>().currentHealth;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
