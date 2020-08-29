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
            ref SteerWanderEdit steerWanderEdit,
            in SteerWander steerWander) =>
            {
                // int idx = nativeThreadIndex;
                int idx = entityInQueryIndex % randomArray.Length;
                var rand = randomArray[idx];

                float3 lookahead = steerWander.distance * math.normalize(steerVelocity.lastVelocity);

                quaternion randq = rand.NextQuaternionRotation();
                steerWanderEdit.qrot = math.slerp(steerWanderEdit.qrot, randq, steerWander.turnWeight);
                float3 dir = math.mul(steerWanderEdit.qrot, new float3(0, 0, 1));

                steerVelocity.velocity = math.lerp(steerVelocity.velocity, (lookahead + dir), steerWander.weight);

                randomArray[idx] = rand;

            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}
            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}