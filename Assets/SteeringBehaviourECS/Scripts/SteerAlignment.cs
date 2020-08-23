using Unity.Entities;

[GenerateAuthoringComponent]
public struct SteerAlignment : IComponentData, ISteerAction
{
    private float weight;
    public float Weight { get => weight; set => weight = value; }
}
