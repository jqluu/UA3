using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;


public class AttackScript : MonoBehaviour
{
    public Transform shootingPoint;

    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public PlayerController playerController;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip clip2;

    public Animator animator;
    [SerializeField]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [SerializeField]
    float fireRate = 300f;
    private float nextFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();   
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.qKey.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            // Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);

            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Debug.Log("e");
            Melee();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);
        

        if (playerController.IsFacingRight)
        {
            // right
            Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        }
        else
        {   
            // left
            Instantiate(bulletPrefab2, shootingPoint.position, rotation);
        }


        source.PlayOneShot(clip);
    }

    void Melee()
    {
        Debug.Log("Melee");

       
        animator.SetTrigger("IsAttacking");
        source.PlayOneShot(clip2);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(shootingPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (playerController.IsFacingRight)
            {
                enemy.GetComponent<Enemy>().TakeDamage(150);
            }
            else
            {   
                enemy.GetComponent<Enemy1>().TakeDamage(150);
            }
           
        }
    
    }

    void OnDrawGizmosSelected()
    {
        if(shootingPoint == null)
            return;

        Gizmos.DrawWireSphere(shootingPoint.position, attackRange);
    }
    
}
