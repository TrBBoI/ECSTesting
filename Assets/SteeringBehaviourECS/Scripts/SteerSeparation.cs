using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerSeparation : IComponentData, ISteerAction
{
    private float weight;
    public float Weight { get => weight; set => weight = value; }
    public float seperationDistance;
}
