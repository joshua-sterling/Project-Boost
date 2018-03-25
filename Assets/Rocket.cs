using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;  
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 750f;

    enum State    { Alive, Dying, Transcending }
    State state = State.Alive;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(state !=State.Alive){ return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing for now
                print("You're okay");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);  //todo parameterize
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                //dead
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
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
