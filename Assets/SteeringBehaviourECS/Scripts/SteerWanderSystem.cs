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

        // Entities.q
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

        var job = Entities
            .WithName(nameof(SteerWanderSystem))
            //.WithNativeDisableParallelForRestriction(randomArray)
            //.WithStoreEntityQueryInField()
            .ForEach(
            (int nativeThreadIndex, 
            ref SteerVelocity steerVelocity,
            ref SteerWander steerWander) =>
            {
                var rand = randomArray[nativeThreadIndex];

                float3 lookahead = steerWander.distance * math.normalize(steerVelocity.lastVelocity);

                steerWander.r = nativeThreadIndex;
                steerWander.randRot = rand.NextFloat3Direction();
                steerWander.rot = math.normalize(math.lerp(steerWander.rot, steerWander.randRot, 0.2f));
                float3 dir = steerWander.radius * steerWander.rot;

                steerVelocity.velocity = math.lerp(steerVelocity.velocity, (lookahead + dir), steerWander.Weight);

            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}