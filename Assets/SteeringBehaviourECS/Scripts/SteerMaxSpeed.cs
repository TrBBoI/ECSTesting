using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerMaxSpeed : IComponentData
{
    public float maxSpeed;
}