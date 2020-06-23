using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int healthMax = 100;
    public int healthValue = 100;
    private Slider healthSlider;
    private GameObject UiHolder;

    void Start()
    {
        healthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthSlider.value = healthMax;

        UiHolder = GameObject.Find("UI Holder");

    }

    public void ApplyDamage(int damageAmout)
    {
        healthValue -= damageAmout;
        if(healthValue < 0)
        {
            healthValue = 0;
        }

        healthSlider.value = healthValue;

        if(healthValue == 0)
        {
            UiHolder.SetActive(false);
            GameplayController.instance.GameOver();
        }
    }
}
