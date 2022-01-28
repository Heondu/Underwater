using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BehaviorStruct
{
    public FlockBehavior behaviors;
    public float weights;
}

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public BehaviorStruct[] behaviorStructs;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 move = Vector3.zero;

        for (int i = 0; i < behaviorStructs.Length; i++)
        {
            Vector3 partialMove = behaviorStructs[i].behaviors.CalculateMove(agent, context, flock) * behaviorStructs[i].weights;

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > behaviorStructs[i].weights * behaviorStructs[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviorStructs[i].weights;
                }

                move += partialMove;
            }
        }

        return move;
    }
}
