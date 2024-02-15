using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public AudioClip ballSound;

    public float maxTravelHeight;
    public float minTravelHeight;
    public float speed;
    public float collisionBallSpeedUp = 1.5f;
    public string inputAxis;

    private void Start()
    {

        
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        float direction = Input.GetAxis(inputAxis);
        Vector3 newPosition = transform.position + new Vector3(0, 0, direction) * speed * Time.deltaTime;
        newPosition.z = Mathf.Clamp(newPosition.z, minTravelHeight, maxTravelHeight);

        transform.position = newPosition;
    }

    //-----------------------------------------------------------------------------
    void OnCollisionEnter(Collision other)
    {
        var paddleBounds = GetComponent<BoxCollider>().bounds;
        float maxPaddleHeight = paddleBounds.max.z;
        float minPaddleHeight = paddleBounds.min.z;

        // Get the percentage height of where it hit the paddle (0 to 1) and then remap to -1 to 1 so we have symmetry
        float pctHeight = (other.transform.position.z - minPaddleHeight) / (maxPaddleHeight - minPaddleHeight);
        float bounceDirection = (pctHeight - 0.5f) / 0.5f;
        // Debug.Log($"pct {pctHeight} + bounceDir {bounceDirection}");

        // flip the velocity and rotation direction
        Vector3 currentVelocity = other.relativeVelocity;
        float newSign = -Math.Sign(currentVelocity.x);
        float newRotSign = -newSign;;

        // Change the velocity between -60 to 60 degrees based on where it hit the paddle
        float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
        Vector3 newVelocity = new Vector3(newSign, 0f, 0f) * newSpeed;
        newVelocity = Quaternion.Euler(0f, newRotSign * 60f * bounceDirection, 0f) * newVelocity;
        other.rigidbody.velocity = newVelocity;


        // temp variable incase the speed is too high

        float temp = 5f;

        if (newSpeed  < 5f)
        {
            temp = newSpeed;
        }

        float pTemp = 3;

        if (newSpeed * .5f < 3f)
        {
            pTemp = newSpeed * .5f;
        }

        other.gameObject.GetComponent<AudioSource>().PlayOneShot(ballSound, temp);
        other.gameObject.GetComponent<AudioSource>().pitch = (int)pTemp;

        // Debug.DrawRay(other.transform.position, newVelocity, Color.yellow);
        // Debug.Break();
    }
}
