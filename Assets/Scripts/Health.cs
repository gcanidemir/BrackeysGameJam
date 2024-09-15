using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxhealth = 100;
    public float currenthealth;
    public HealthBar healthBar;
    public SpriteRenderer sprite;
    public float shieldmod;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    public IEnumerator FlashRed()
    {
        sprite.color = new Color32(233, 108, 108, 255);
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }

    public void takedamage(float damage)
    {
        if (damage < currenthealth)
        {
            currenthealth -= damage/shieldmod;
        StartCoroutine(FlashRed());

        }
        else
            currenthealth = 0;
        healthBar.SetHealth(currenthealth);
    }


    public void Heal(float heal)
    {
        if (heal <= maxhealth - currenthealth)
            currenthealth += heal;
        else
            currenthealth = maxhealth;
        healthBar.SetHealth(currenthealth);
    }
}