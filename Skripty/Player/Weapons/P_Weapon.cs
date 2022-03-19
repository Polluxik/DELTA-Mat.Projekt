using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class P_Weapon : MonoBehaviour
{
    private Animator m_animator;
    public GameObject weapon;
    public P_WeaponSwitcher switcher;
    public GameObject impact;
    public GameObject bloodSplash;
    public GameObject muzzleFlash;
    private Transform player;
    private P_Controller character;

    private UI_Controller uiController;
    
    public int maxAmmo;
    public int ammo;
    
    private AudioSource audioSource;

    private GameObject muzzleSpawnPoint;
    
    public float reloadTime;
    public float minDamage;
    public float maxDamage;


    public float fireRate;
    private float _shootTime = 0;

    public bool primary = false;
    public bool manual = false;
    public bool reloading = false;
    public Camera playerCamera;

    //public ParticleSystem muzzleFlash;

    void Start()
    {
        m_animator = GetComponentInParent<Animator>();
        
        uiController = FindObjectOfType<UI_Controller>();
        ammo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        character = GameObject.FindWithTag("Player").GetComponent<P_Controller>();
        muzzleSpawnPoint = GameObject.FindWithTag("MuzzleSpawn");
    }

    void Update()
    {
        if (!UI_PauseMenu.gameIsPaused || !P_UpgradeShop.isUpgradeMenuOpen)
        {
            
        
        var pcTransform = playerCamera.transform;
        Debug.DrawRay(pcTransform.position, pcTransform.forward * 100.0f, Color.green);

        if ((manual ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1")) && Time.time >= _shootTime &&
            reloading == false)
        {
            _shootTime = Time.time + 1f / fireRate;
            if (ammo != 0)
            {
                Shoot();
            }
            else
            {
                print("no ammo!");
            }
        } 

        StartCoroutine(Reload());

        uiController.SetAmmo(ammo, maxAmmo);
        }
    }

    private void Shoot()
    {
        if (gameObject.activeSelf)
        {
            
            audioSource.PlayOneShot(audioSource.clip);
            Instantiate(muzzleFlash, muzzleSpawnPoint.transform.position, Quaternion.identity);
            m_animator.SetTrigger("Shoot");
            ammo--;
            //print(ammo + " left");
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100.0f))
            {
                E_Target target = hit.transform.GetComponent<E_Target>();
                if (target != null)
                {
                    
                    if (character.Health < 100)
                    {
                        character.Health += Random.Range(2f, 5f);
                        if (character.Health > character.maxHealth)
                        {
                            character.Health = character.maxHealth;
                        }
                    }
                    target.TakeDamage(Random.Range(minDamage, maxDamage));
                    Instantiate(bloodSplash, hit.point, Quaternion.identity).transform.LookAt(player);
                }

                if (hit.transform.name != null)
                {
                    Instantiate(impact, hit.point, Quaternion.identity);
                    
                    
                }
            }
        }
    }

    IEnumerator Reload()
    {

        
        if ((ammo == 0 && reloading == false && ammo != maxAmmo) || (Input.GetButtonDown("Reload") && reloading == false && ammo != maxAmmo))
        {
            m_animator.SetTrigger("Reload");
            reloading = true;
            switcher.reloading = true;

            yield return new WaitForSeconds(2f);
            
            ammo = maxAmmo;

            reloading = false;
            switcher.reloading = false;
        }
    }
}