using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Shot : MonoBehaviour
{
    public GameObject Ship;
    public GameObject bullet;
    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    public int Municiones = 0;

    public AudioSource source;
    public AudioClip clip;

    public ParticleSystem smoke;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;
    public ParticleSystem smoke3;

    /*public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;
    */
    public GameObject Cannon;
    public GameObject Cannon1;
    public GameObject Cannon2;
    public GameObject Cannon3;

    private PlayerInputActions playerControls;

    private float shotRateTime = 0;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnTriggerEnter(Collider collision)
    {

         if (collision.gameObject.tag == "Ammo")
         {
            Destroy(collision.gameObject);
            Municiones = Municiones + 1;

         }
    }


    void Update()
    {
        Shoot();

    }

    public void Shoot()
    {


        if (Municiones >= 1)
        {
            if (playerControls.Player.Shot.ReadValue<float>() == 1)
            {
                Municiones = Municiones - 1;

                Cannon.GetComponent<Animation>().Play("Cannon");
                Cannon1.GetComponent<Animation>().Play("Cannon1");
                Cannon2.GetComponent<Animation>().Play("Cannon2");
                Cannon3.GetComponent<Animation>().Play("Cannon3");


                source.PlayOneShot(clip);

                smoke.Play();
                smoke1.Play();
                smoke2.Play();
                smoke3.Play();
                /*
                source1.PlayOneShot(clip);
                source2.PlayOneShot(clip);
                source3.PlayOneShot(clip);
                */


                if (Time.time > shotRateTime)
                {
                    GameObject newBullet;
                    GameObject newBullet1;
                    GameObject newBullet2;
                    GameObject newBullet3;
                    

                    newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

                    newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                    newBullet1 = Instantiate(bullet, spawnPoint1.position, spawnPoint1.rotation);

                    newBullet1.GetComponent<Rigidbody>().AddForce(spawnPoint1.forward * shotForce);

                    newBullet2 = Instantiate(bullet, spawnPoint2.position, spawnPoint2.rotation);

                    newBullet2.GetComponent<Rigidbody>().AddForce(spawnPoint2.forward * shotForce);

                    newBullet3 = Instantiate(bullet, spawnPoint3.position, spawnPoint3.rotation);

                    newBullet3.GetComponent<Rigidbody>().AddForce(spawnPoint3.forward * shotForce);

                 

                    shotRateTime = Time.time + shotRate;

                    Destroy(newBullet, 4);
                    Destroy(newBullet1, 4);
                    Destroy(newBullet2, 4);
                    Destroy(newBullet3, 4);
       
                }
            }
        }

    }
}