using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerSeparation : IComponentData
{
    public float weight;
    public float seperationDistance;
}
