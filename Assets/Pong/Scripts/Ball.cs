using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public AudioClip wallSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(wallSound, 5f);
        }

    }
}
