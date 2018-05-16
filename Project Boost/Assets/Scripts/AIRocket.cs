using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIRocket : MonoBehaviour
{
    [SerializeField] float MainThrust = 250f;
    [SerializeField] Transform Target;

    Rigidbody rigidBody;
    
	// Use this for initialization
	void Start ()
    {
               
	}
	
	// Update is called once per frame
	void Update ()
    {

        rigidBody.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
       

    }



}


