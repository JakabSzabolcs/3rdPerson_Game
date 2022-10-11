using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;

    private float speed = 150f;
    private float timeToDestroy = 5f;

    public Vector3 target {get; set; }
    public bool hit{get;set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
            SoundManagerScript.PlaySound("missed");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //SoundManagerScript.PlaySound("hit");
        ContactPoint contact = other.GetContact(0);
        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0301f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}
