using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;  
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 750f;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Rotate()
    {
        rigidBody.freezeRotation = true;  //take manual control of rotation
        
        float roatationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * roatationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * roatationThisFrame);
        }

         rigidBody.freezeRotation = false;  //resume physics control of rotation

    }

    private void Thrust()
    {
        float mainThrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrustThisFrame);
            if (!audioSource.isPlaying)
            { audioSource.Play(); }
        }
        else
        {
            audioSource.Stop();
        }
              
    }
}
