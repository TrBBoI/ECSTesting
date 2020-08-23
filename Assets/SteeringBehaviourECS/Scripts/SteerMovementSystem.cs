using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

public class SteerMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float dt = Time.DeltaTime;

        var job = Entities
            .WithName(nameof(SteerMovementSystem))
            .ForEach(
            (ref Translation translation, 
            ref SteerVelocity steerVelocity,
            in SteerMaxSpeed steerMaxSpeed) =>
            {
                float maxSpeed = steerMaxSpeed.maxSpeed;
                float3 velocity = steerVelocity.velocity;
                if (math.lengthsq(velocity) > maxSpeed * maxSpeed)
                {
                    velocity = math.normalize(velocity) * maxSpeed;
                }
                steerVelocity.lastVelocity = velocity;

                var position = translation.Value + velocity * dt;

                if (position.x < -50f) { position.x += 100; }
                else if (position.x > 50f) { position.x -= 100; }

                if (position.y < -50f) { position.y += 100; }
                else if (position.y > 50f) { position.y -= 100; }

                if (position.z < -50f) { position.z += 100; }
                else if (position.z > 50f) { position.z -= 100; }

                translation.Value = position;
            }).Schedule(inputDeps);

        // job.Complete();

        return job;
        // throw new System.NotImplementedException();
    }
}