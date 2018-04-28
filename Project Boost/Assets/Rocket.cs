using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 250f; // Changable in the editor
    [SerializeField] float MainThrust = 1f; // Changable in the editor

    Rigidbody rigidBody;
    AudioSource RocketSound;
   	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        RocketSound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
    {

        Thrust();
        Rotation();
        
	}


    private void Thrust()
    {


        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up * MainThrust);

            if (!RocketSound.isPlaying)
            {
                RocketSound.Play();
            }

        }
        else
        {
            RocketSound.Stop();
        }

    }

    private void Rotation()
    {
        rigidBody.freezeRotation = true; // Manually take control of the rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // physics take back control of rotation
 

    }


}


