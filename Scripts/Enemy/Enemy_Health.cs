using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class Enemy_Health : MonoBehaviour
{
    public Animator animator;
    private Enemy_Movement enemyMoving;
    [SerializeField] private float maxhealth = 10;
    private float health;
    private float speedBeforeDeath;

   // [SerializeField] private GameObject BloodEffect;
    [SerializeField] private GameObject Coins;
    [SerializeField] private GameObject DamageText;
    [SerializeField] private bool IsActived = false;


    

    private void Awake()
    {
        
        enemyMoving = GetComponent<Enemy_Movement>();
        speedBeforeDeath = enemyMoving.speed;
    }
    private void OnEnable()
    {
        health = maxhealth;
        IsActived = true;
    }


    void Update()
    {
        if (!IsActived) return;

        if (health <= 0)
        {
            if (Coins != null)
            {
               
                ObjectPoolingManager.instance.spawnGameObject(Coins, transform.position, Quaternion.identity);
            }
            animator.SetBool("IsDead", true);
            GameManager.Instance.NumberOfKills++;
            
            IsActived = false;
            enemyMoving.speed = 0;
            StartCoroutine(HideBody());
            //
            //   Destroy(gameObject);

        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(GetComponent<Enemy_Movement>()!=null)
        GetComponent<Enemy_Movement>().NockBackTime = .2f;

        AudioManager.instance.PlaySound("Enemy_Hurt");
        if (DamageText != null)
        {
            GameObject Text = Instantiate(DamageText, transform.position, Quaternion.identity);

            Text.GetComponent<TMP_Text>().text = damage.ToString();
        }
        
    }

    public void TakeDamageByPet(float damage)
    {
        health -= damage;
        if (GetComponent<Enemy_Movement>() != null)
            GetComponent<Enemy_Movement>().NockBackTime = .2f;

        
        if (DamageText != null)
        {
            GameObject Text = Instantiate(DamageText, transform.position, Quaternion.identity);

            Text.GetComponent<TMP_Text>().text = damage.ToString();
        }

    }

    IEnumerator HideBody()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        yield return new WaitForSeconds(0.5f);
        ObjectPoolingManager.instance.ReturnObjectToPool(gameObject);
        animator.SetBool("IsDead", false);
        this.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        enemyMoving.speed = speedBeforeDeath;
    }
}
