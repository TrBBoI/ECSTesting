using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateBefore(typeof(SteerMovementSystem))]
public class SteerWanderSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

        var job = Entities
            .WithName(nameof(SteerWanderSystem))
            .WithNativeDisableParallelForRestriction(randomArray)
            .ForEach(
            (int nativeThreadIndex, 
            ref SteerVelocity steerVelocity,
            in SteerWander steerWander) =>
            {
                var rand = randomArray[nativeThreadIndex];

                float3 lookahead = steerWander.distance * math.normalize(steerVelocity.lastVelocity);
                float3 dir = math.mul(rand.NextQuaternionRotation(), new float3(0, 0, steerWander.radius));

                steerVelocity.velocity = math.lerp(steerVelocity.lastVelocity, (lookahead + dir), steerWander.Weight);

            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}