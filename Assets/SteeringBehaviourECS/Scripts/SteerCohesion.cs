using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerCohesion : IComponentData, ISteerAction
{
    private float weight;
    public float Weight { get => weight; set => weight = value; }
    public float cohesionDistance;
}
