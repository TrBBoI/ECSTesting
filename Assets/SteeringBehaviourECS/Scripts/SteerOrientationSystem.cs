using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateAfter(typeof(SteerMovementSystem))]
public class SteerOrientationSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float dt = Time.DeltaTime;

        var job = Entities
            .WithName(nameof(SteerOrientationSystem))
            .ForEach(
            (ref Rotation rotation,
            in SteerVelocity steerVelocity) =>
            {
                float3 forward = math.normalizesafe(steerVelocity.velocity, new float3(0, 0, 1));
                
                //float3 right = math.normalize(math.cross(forward, new float3(0, 1, 0)));
                //float3 up = math.normalize(math.cross(right, forward));

                // rotation.Value = new quaternion(new float3x3(right, up, forward));
                // rotation.Value = quaternion.LookRotation(steerVelocity.velocity, up);

                rotation.Value = quaternion.LookRotation(forward, new float3(0, 1, 0));
            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}