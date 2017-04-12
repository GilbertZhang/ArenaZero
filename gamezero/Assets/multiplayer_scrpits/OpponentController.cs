using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour {

    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlayerInformation(float posX, float posZ, float velX, float velZ, float rotY)
    {
        transform.position = new Vector3(posX, 2.86f, posZ);
        transform.rotation = Quaternion.Euler(0, rotY, 0);
        // We're going to do nothing with velocity.... for now
    }

    public void Fire(float m_CurrentLaunchForce)
    {
        // Set the fired flag so only Fire is only called once.
        //m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }
}
