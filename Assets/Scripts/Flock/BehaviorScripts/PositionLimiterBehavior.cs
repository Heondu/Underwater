using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Position Limiter")]
public class PositionLimiterBehavior : FlockBehavior
{
    public float yMin;
        
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (agent.transform.position.y < yMin)
        {
            return Vector3.up;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
