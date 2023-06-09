using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    [SerializeField] float mainThrust=100f;
    [SerializeField] float rotationThrust=1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
     [SerializeField] ParticleSystem leftThrusterParticles;
     [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audios;

   


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audios =GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    


void ProcessThrust()
{
    if (Input.GetKey(KeyCode.Space))
    {
       StartThrusting();
    }
    else
    {
        StopThrusting();
    }
  
}
void ProcessRotation()
{
    if (Input.GetKey(KeyCode.A)) 
    {
       RotateLeft();
    }
     else if (Input.GetKey(KeyCode.D))
    {
       RotateRight();
    }
    else
    {
        StopRotating();
    }
}


void StartThrusting()
{
 rb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
        if(!audios.isPlaying)
        {
              audios.PlayOneShot(mainEngine);
        }
        if(!mainEngineParticles.isPlaying)
        {
              mainEngineParticles.Play();
        }
}
void StopThrusting()
{
       audios.Stop();
        mainEngineParticles.Stop();
}


void RotateLeft()
{
    ApplyRotation(rotationThrust);
        if(!rightThrusterParticles.isPlaying)
        {
              rightThrusterParticles.Play();
        }
}

void RotateRight()
{
 ApplyRotation(-rotationThrust);
        if(!leftThrusterParticles.isPlaying)
        {
              leftThrusterParticles.Play();
        }
}

void StopRotating()
{
    rightThrusterParticles.Stop();
    leftThrusterParticles.Stop();
}

void ApplyRotation(float rotationThisFrame)
{
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
}
}
