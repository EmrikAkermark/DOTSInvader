using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public class BulletCollisionSystem : SystemBase
{
	EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;
	EntityQuery entityQuery;

	protected override void OnCreate()
	{
		endSimulationEcbSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
		entityQuery = GetEntityQuery(typeof(HealthData), typeof(Translation));
	}

	protected override void OnUpdate()
	{
		var entities = entityQuery.ToEntityArray(Allocator.TempJob);
		var translations = entityQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
		var healthDatas = entityQuery.ToComponentDataArray<HealthData>(Allocator.TempJob);
		var ecb = endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

		Entities.WithAll<BulletTag>().ForEach((int entityInQueryIndex, Entity entity, in Translation translation) =>
		{
			
			for (int i = 0; i < translations.Length; i++)
			{
				float distance = math.distance(translation.Value, translations[i].Value);
				if (distance < 1.3f)
				{
					ecb.DestroyEntity(entityInQueryIndex, entity);
					ecb.SetComponent(entityInQueryIndex, entities[i], new HealthData {Value = healthDatas[i].Value-1 });
				}

			}
		}
		).ScheduleParallel();
		this.CompleteDependency();
		
		endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
		
		entities.Dispose();
		translations.Dispose();
		healthDatas.Dispose();
	}
}
