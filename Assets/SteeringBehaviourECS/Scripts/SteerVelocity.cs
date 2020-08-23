using Unity.Mathematics;
using Unity.Entities;
using Unity.Physics;

[GenerateAuthoringComponent]
public struct SteerVelocity : IComponentData
{
    public float3 velocity;
    public float3 lastVelocity;
}
