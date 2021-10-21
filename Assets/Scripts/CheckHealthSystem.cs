using Unity.Entities;

public class CheckHealthSystem : SystemBase
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

		Entities.ForEach((int entityInQueryIndex, Entity entity, in HealthData healthdata) =>
		{
			if(healthdata.Value <= 0)
			{
				ecb.DestroyEntity(entityInQueryIndex, entity);
			}
		}).ScheduleParallel();

		this.CompleteDependency();

		endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
	}
}
