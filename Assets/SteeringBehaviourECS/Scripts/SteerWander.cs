using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct SteerWander : IComponentData, ISteerAction
{
    public float weight;
    public float Weight { get => weight; set => weight = value; }
    public float distance;
    public float radius;

    public float3 rot;
    public int r;
    public float3 randRot;
}
