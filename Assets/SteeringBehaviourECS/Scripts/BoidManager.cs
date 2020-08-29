using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private EntityManager entityManager;
    public GameObject boidPrefab;
    public int boidSize = 500;

    public BlobAssetStore store;
    
    // Start is called before the first frame update
    void Start()
    {
        store = new BlobAssetStore();
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        Entity boid = GameObjectConversionUtility.ConvertGameObjectHierarchy(boidPrefab, settings);

        Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint)System.DateTime.Now.Millisecond);

        for (int i = 0; i < boidSize; ++i)
        {
            var instance = entityManager.Instantiate(boid);
            float3 xyz = rand.NextFloat3(-50f, 50f);
            quaternion q = rand.NextQuaternionRotation();
            entityManager.SetComponentData(instance, new Translation { Value = xyz });
            entityManager.SetComponentData(instance, new Rotation { Value = q });

            float3 velocity = math.mul(q, new float3(0, 0, rand.NextFloat(1f,10f)));
            entityManager.SetComponentData(instance, new SteerVelocity { velocity = velocity, lastVelocity = velocity });

            if (entityManager.HasComponent<SteerWander>(instance))
            {
                entityManager.AddComponentData(instance, new SteerWanderEdit() { qrot = quaternion.identity });
            }
        }
    }

    private void OnDestroy()
    {
        store.Dispose();
    }
}
