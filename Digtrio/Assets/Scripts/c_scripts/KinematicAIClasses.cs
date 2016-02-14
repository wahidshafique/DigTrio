using UnityEngine;
using System.Collections;

public struct Static
{
    public Vector2 position;
    public float orientation;

    public Vector2 OrientationAsVector()
    {
        Vector2 vector = new Vector2(Mathf.Sin(orientation), Mathf.Cos(orientation));
        return vector;
    }
}

public struct Kinematic
{
    public Vector2 position;
    public float orientation;
    public Vector2 velocity;
    public float rotation;

    public void UpdatePositon(KinematicSteeringOutput steering)
    {
        // Update the position and orientation of the enemy
        position += velocity * Time.time + 0.5f * steering.velocity * Time.time * Time.time;
        orientation += rotation * Time.time + 0.5f * steering.rotation * Time.time * Time.time;

        // Update the velocity and the rotation of the enemy
        velocity += steering.velocity * Time.time;
        rotation += steering.rotation * Time.time;
    }

    public Vector2 OrientationAsVector()
    {
        Vector2 vector = new Vector2(Mathf.Sin(orientation), Mathf.Cos(orientation));
        return vector;
    }

}

public struct SteeringOutput
{
    public Vector3 linear;
    public float angular;
}

public struct KinematicSteeringOutput
{
    public Vector2 velocity;
    public float rotation;
}

public class KinematicSeek
{
    // Holds static data for the character and target
    Static character;
    Static target;

    // Holds the maximum speed the character can travel
    float maxSpeed;

    public float GetNewOrientation(float currentOrientation, Vector3 velocity)
    {
        // Make sure we have a velocity
        if (velocity.magnitude > 0)
        {
            // Calculate orientation using an arc tangent of the velocity components
            return Mathf.Atan2(-velocity.x, velocity.z);
        }
        // Otherwise use the current orientation
        else
        {
            return currentOrientation;
        }
    }

    public KinematicSteeringOutput GetSteering()
    {
        // Create the structure for the output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get the direction to the target
        steering.velocity = target.position - character.position;

        // The velocity is along this direction, at full speed
        steering.velocity.Normalize();
        steering.velocity *= maxSpeed;

        // Face in the direction we want to move
        character.orientation = GetNewOrientation(character.orientation, steering.velocity);

        // Output the steering
        steering.rotation = 0;

        return steering;
    }
}

public class KinematicArrive : KinematicSeek
{
    // Holds the static data for the character and target
    Static character;
    Static target;

    // Holds the maximum speed the character can travel
    float maxSpeed;

    // Holds the satisfaction radius
    float radius;

    // Holds the time to target constant
    float timeToTarget = 0.25f;

    public KinematicSteeringOutput GetSteering()
    {
        // Create the stucture for the output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get the direction to the target
        steering.velocity = target.position - character.position;

        // Check if we're within radius
        if (steering.velocity.magnitude < radius)
        {
            // We can return no steering request
            return steering;
        }

        // We need to move to our target, we'd like to get there in timeToTarget seconds
        steering.velocity /= timeToTarget;

        // If this is to fast, clip it to the max speed
        if (steering.velocity.magnitude > maxSpeed)
        {
            steering.velocity.Normalize();
            steering.velocity *= maxSpeed;
        }

        // Face in the direction we want to move
        character.orientation = GetNewOrientation(character.orientation, steering.velocity);

        // Output the steering
        steering.rotation = 0;
        return steering;
    }
}

public class KinematicWander
{
    // Holds the static data for the character
    Kinematic character;

    // Holds the maximum speed the character can travel
    float maxSpeed;

    // Holds the maximum rotation speed we'd like, probably should be smaller than the maximum possible, 
    // to allow a leisurely change in direction
    float maxRotation;

    public void SetWander(Kinematic c, float mS, float mR)
    {
        character = c;
        maxSpeed = mS;
        maxRotation = mR;
    }

    public KinematicSteeringOutput GetSteering()
    {
        // Create a structure for output
        KinematicSteeringOutput steering = new KinematicSteeringOutput();

        // Get velocity from the vector form of the orientation
        steering.velocity = maxSpeed * character.OrientationAsVector();

        // Change our orientation randomly
        steering.rotation = (Random.Range(0,2) - Random.Range(0,2)) * maxRotation;

        // Output the steering
        return steering;
    }
}