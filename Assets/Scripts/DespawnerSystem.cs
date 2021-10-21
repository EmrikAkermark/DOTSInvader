using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class DespawnerSystem : SystemBase
{
	EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;

	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		endSimulationEcbSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

	}


	protected override void OnUpdate()
	{
		var ecb = endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();


		Entities.ForEach((int entityInQueryIndex, Entity entity, in Translation translation) =>
		{
			if(translation.Value.y > 30f || translation.Value.y < -10)
			{
				ecb.DestroyEntity(entityInQueryIndex, entity);
			}
		}).ScheduleParallel();


		this.CompleteDependency();

		endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
	}
}
