using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftEngineParticles;
    [SerializeField] private ParticleSystem rightEngineParticles;

    private Rigidbody rb;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void StartThrusting()
    {
        PlayAudio(mainEngine);

        PlayParticles(mainEngineParticles);

        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }

    private void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            StartRotatingRight();
        }
        else
        {
            rightEngineParticles.Stop();
            leftEngineParticles.Stop();
        }
    }

    private void StartRotatingRight()
    {
        PlayParticles(leftEngineParticles);
        ApplyRotation(-rotationSpeed);
    }

    private void StartRotatingLeft()
    {
        PlayParticles(rightEngineParticles);
        ApplyRotation(rotationSpeed);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void PlayParticles(ParticleSystem particleSystem)
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // freezing rotation to change rotation manually
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing rotation to let the physics system take over
    }
}
