using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIRocket : MonoBehaviour
{
    [SerializeField] float MainThrust = 250f;
    [SerializeField] Transform Target;

    Rigidbody rigidBody;
    
    Vector3 currentLocation;
	// Use this for initialization
	void Start ()
    {
               
        currentLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

        rigidBody.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
       

    }



}


