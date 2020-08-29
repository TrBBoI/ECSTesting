using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct SteerWander : IComponentData
{
    public float weight;
    public float turnWeight;
    public float distance;
    public float radius;
}

public struct SteerWanderEdit : IComponentData
{
    public quaternion qrot;
}