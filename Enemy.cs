using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy: MonoBehaviour
{
    public LogicScript logic;

    Rigidbody2D rb;
    Animator animator;

    public BoxCollider2D boxCollider;


    [SerializeField]
    float maxHealth = 100;
    float currentHealth;

    [SerializeField]
    float speed = 1f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            boxCollider.enabled = false;
            animator.SetBool("IsDying", true);
            StartCoroutine(DestroyAfterAnimation());
            
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the length of the dying animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        
        // Destroy the GameObject
        if(gameObject)
        {
            logic.addScore(1);
        }
        

        if(logic.playerScore >= 29 )
        {
            logic.victory();
            logic.playerScore = 0;
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    
}
