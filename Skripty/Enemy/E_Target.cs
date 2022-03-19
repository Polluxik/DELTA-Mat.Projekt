using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class E_Target : MonoBehaviour
{
    public float sightDistance;
    private P_Controller controller;
    private Animator mAnimator;

    public bool tutorial;
    public float health;

    private UI_Player uiPlayer;
    private Transform movePositionTransform;
    private NavMeshAgent navmesh;
    public float errorMargin;

    public float minDamage;
    public float maxDamage;

    public int minCoins;
    public int maxCoins;

    public float fireRate;
    private float _shootTime = 0;

    private AudioSource audioSource;

    private int ammo;
    public int maxAmmo;

    public bool reloading;

    private float Damping = 6.0f;

    public GameObject weapon;


    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        uiPlayer = GameObject.FindWithTag("Player").GetComponent<UI_Player>();
        navmesh = GetComponent<NavMeshAgent>();
        controller = GameObject.FindWithTag("Player").GetComponent<P_Controller>();
        movePositionTransform = GameObject.FindWithTag("Player").transform;
        mAnimator = GetComponentInChildren<Animator>();
        //tutorial = true;
        ammo = maxAmmo;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        //uiPlayer.SetDebug(amount, health);

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
         if (tutorial != true)
        {
             controller.Coins += Random.Range(minCoins, maxCoins+1);
        }
    }

    void Update()
    {
        Movement();

        StartCoroutine(Reload());
    }

    private void Movement()
    {
        
        var distance = Vector3.Distance(movePositionTransform.position, transform.position);

        if (movePositionTransform != null)
        {
            //  transform.LookAt(movePositionTransform);
            if (distance > sightDistance)
            {
                mAnimator.SetBool("shoot",false);
                mAnimator.SetBool("walk",true);

                weapon.SetActive(false);
                navmesh.destination = movePositionTransform.position;
                navmesh.stoppingDistance = sightDistance;
            }

            else
            {
                weapon.SetActive(true);
                mAnimator.SetBool("walk",false);
                mAnimator.SetBool("shoot",true);
                var rotation = Quaternion.LookRotation(movePositionTransform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);

                if (Time.time >= _shootTime)
                {
                    _shootTime = Time.time + 1f / fireRate;
                    if (ammo != 0 && reloading == false)
                    {
                        Shoot();
                    }
                    else
                    {
                        //print("ENEMY: No ammo");
                    }
                }
            }
        }
        else
        {
            //print("Nothing!");
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        var shotDir = movePositionTransform.position + Random.insideUnitSphere * errorMargin - transform.position;
        shotDir.y += 1f;
        //Debug.DrawRay(transform.position, shotDir, Color.red);
        if (Physics.Raycast(transform.position, shotDir , out hit, sightDistance))
        {
            ammo--;
            //mAnimator.SetTrigger("Shoot");
            audioSource.PlayOneShot(audioSource.clip);
            if (hit.transform.name == controller.transform.name)
            {
                P_Controller player = hit.transform.GetComponent<P_Controller>();
                player.TakeHealth(Random.Range(minDamage, maxDamage));
            }

            //print(shotDir);
        }
    }

    IEnumerator Reload()
    {
        if (ammo == 0 && reloading == false)
        {
            mAnimator.SetTrigger("Reload");
            reloading = true;

            yield return new WaitForSeconds(2f);

            reloading = false;
            ammo = maxAmmo;
        }
    }
}