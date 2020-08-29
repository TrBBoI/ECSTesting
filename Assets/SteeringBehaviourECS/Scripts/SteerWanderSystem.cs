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
            int entityInQueryIndex,
            ref SteerVelocity steerVelocity,
            ref SteerWander steerWander) =>
            {
                // int idx = nativeThreadIndex;
                int idx = entityInQueryIndex % randomArray.Length;
                var rand = randomArray[idx];

                float3 lookahead = steerWander.distance * math.normalize(steerVelocity.lastVelocity);

                steerWander.r = nativeThreadIndex;
                steerWander.randRot = rand.NextFloat3Direction();
                steerWander.rot = math.normalize(math.lerp(steerWander.rot, steerWander.randRot, 0.2f));
                float3 dir = steerWander.radius * steerWander.rot;
                randomArray[idx] = rand;

            }).Schedule(inputDeps);
                steerVelocity.velocity = math.lerp(steerVelocity.velocity, (lookahead + dir), steerWander.Weight);

        // job.Complete();

        return job;
    }
}
            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}