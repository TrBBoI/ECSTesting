using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct SteerWander : IComponentData, ISteerAction
{
    public float weight;
    public float Weight { get => weight; set => weight = value; }
    public float distance;
    public float radius;
}
