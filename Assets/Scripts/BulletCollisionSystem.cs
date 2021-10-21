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
		var healthDatas = entityQuery.ToComponentDataArray<HealthData>(Allocator.TempJob);
		var translations = entityQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

		Entities.WithAll<BulletTag>().ForEach((in Translation translation) =>
		{
			
		}
		).ScheduleParallel();
	}
}
