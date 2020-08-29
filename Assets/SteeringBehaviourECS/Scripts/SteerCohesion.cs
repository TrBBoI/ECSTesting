using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerCohesion : IComponentData
{
    public float weight;
    public float cohesionDistance;
}
