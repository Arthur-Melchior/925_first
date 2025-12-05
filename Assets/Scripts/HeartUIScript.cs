using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private Image heartImage;
    [SerializeField] private HorizontalLayoutGroup layoutGroup;
    
    private List<Image> heartInstances = new List<Image>();

    void Start()
    {
        currentHealth = maxHealth;

        GenerateHearts();
        UpdateHearts();
    }

    private void GenerateHearts()
    {
        foreach (Transform child in layoutGroup.transform)
            Destroy(child.gameObject);
        
        heartInstances.Clear();

        
        for (var i = 0; i < maxHealth; i++)
        {
            var img = Instantiate(heartImage, layoutGroup.transform);
            heartInstances.Add(img);
        }
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < heartInstances.Count; i++)
        {
            heartInstances[i].enabled = (i < currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }
}
