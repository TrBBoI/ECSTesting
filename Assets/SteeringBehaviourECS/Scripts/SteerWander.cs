using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct SteerWander : IComponentData
{
    public float weight;
    public float distance;
    public float radius;

    public float3 rot;
    public int r;
    public float3 randRot;
}