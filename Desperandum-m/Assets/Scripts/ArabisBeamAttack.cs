using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArabisBeamAttack : MonoBehaviour
{
    public GameObject beamPrefab; // the prefab for the beam game object
    public float beamRotationDuration; // the duration of the beam rotation in seconds
    public float beamRotationSpeed; // the speed at which the beam rotates in degrees per second
    public Transform beamSpawnPos;
    private BossFightCharacter player;
    private GameObject beam; // the beam game object
    private float startTime; // the time at which the beam rotation started
    private float currentAngle; // the current angle of the beam
    public int beamDamage;

    void Start()
    {
        player = GetComponent<BossFightCharacter>();

        // instantiate the beam prefab at the position of the BeamSpawner game object
        beam = Instantiate(beamPrefab, beamSpawnPos.position, Quaternion.identity);

        // rotate the beam along the z axis by x degrees
        beam.transform.Rotate(0f, 0f, -30f);
        currentAngle = -30f;

        // set the start time to the current time
        startTime = Time.time;
    }

    void Update()
    {

        // get the elapsed time since the beam rotation started
        float elapsedTime = Time.time - startTime;



        // if the elapsed time is less than the beam rotation duration
        if (elapsedTime < beamRotationDuration)
        {
            // rotate the beam along the z axis by the specified rotation speed
            beam.transform.Rotate(0f, 0f, beamRotationSpeed * Time.deltaTime);
            currentAngle += beamRotationSpeed * Time.deltaTime;

            // if the current angle is greater than or equal to 360 degrees
            if (currentAngle <= -150f)
            {

                BeamRotateBack();
            }
        }

        else if (elapsedTime > beamRotationDuration)
        {

            //Destroy after elapsed time
            Debug.Log("Destroyed beam");
            Destroy(beam);

        }

        // if the elapsed time is equal to or greater than the beam rotation duration
        else
        {
            // rotate the beam along the z axis to reach the target angle of -135 degrees
            beam.transform.Rotate(0f, 0f, -150f - currentAngle);

        }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(beamDamage);
        }
    }
    void BeamRotateBack()
    {
        currentAngle = -30f;
        // Get the current rotation of the game object
        float currentRotation = beam.transform.rotation.eulerAngles.z;

        // Rotate the game object towards -30 degrees
        float targetRotation = -30.0f;

        // Calculate the new rotation for the game object
        float newRotation = currentRotation;
        if (currentRotation > targetRotation)
        {
            newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, Time.deltaTime * beamRotationSpeed);
        }
        else if (currentRotation < targetRotation)
        {
            newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, Time.deltaTime * beamRotationSpeed);
        }

        // Clamp the new rotation to the range [-150, -30]
        newRotation = Mathf.Clamp(newRotation, -150.0f, -30.0f);

        // Set the new rotation for the game object
        beam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotation);
    }
}