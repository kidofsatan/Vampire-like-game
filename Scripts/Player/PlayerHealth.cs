using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth;
    public Slider slider;
    public Vector3 SliderOffset;
    public bool IsDead;


   

    void Start()
    {
        CurrentHealth = MaxHealth;
        setmaxhealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + SliderOffset);
        
        if (CurrentHealth <= 0)
        {
            IsDead = true;

        }
        else
            IsDead = false;

       
    }



   
    public void TakeDamage(float damage)
    {

        
        {
            AudioManager.instance.PlaySound("Hurt");
            CurrentHealth -= damage;
            sethealth(CurrentHealth);
            
        }



    }
    public void Heal(float Healing)
    {
        if (CurrentHealth + Healing >= MaxHealth)
        {
            CurrentHealth = MaxHealth;

        }
        else
        {
            CurrentHealth += Healing;
        }

    }
    public void setmaxhealth(float mhealth)
    {

        slider.maxValue = mhealth;

        slider.value = mhealth;



    }

    public void sethealth(float health)
    {


        slider.value = health;

    }

    

    


}

