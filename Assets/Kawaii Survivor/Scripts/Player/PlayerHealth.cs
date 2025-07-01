using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private int maxHealth;
    private int health;
    
    [Header(" elements ")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;

        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        UpdateUI();
        
        if (health <= 0)
            PassAway();
    }

    private void PassAway()
    {
        Debug.Log("Player has passed away.");
        SceneManager.LoadScene(0);
    }
    
    private void UpdateUI()
    {
        float healthBarValue = (float)health / maxHealth;
        healthSlider.value = healthBarValue;
        
        healthText.text = health + " / " + maxHealth;
    }
}
