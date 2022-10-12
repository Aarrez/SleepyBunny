using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroidVersion;
    public GameObject surface;
    public GameObject softSurface;
    public AudioSource breakingSound;
    public Transform CatchingSurface;

    private void Start()
    {
     
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == surface)
        {
            Instantiate(destroidVersion, transform.position, transform.rotation);
            breakingSound.Play();
            Destroy(gameObject);
        }
        else if (collision.collider.gameObject == softSurface)
        {
            Rigidbody catcher = GetComponent<Rigidbody>();
            catcher.transform.parent = CatchingSurface;
        }
    }
}
