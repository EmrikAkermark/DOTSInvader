using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class PlayerShootSystem : SystemBase
{
	EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;

	protected override void OnCreate()
	{
		endSimulationEcbSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
	}


	protected override void OnUpdate()
	{
		var ecb = endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
		var bulletSettings = GetSingleton<BulletSpawnSettings>();


		Entities.WithAll<PlayerTag>().ForEach((int entityInQueryIndex, in Translation translation, in ShootData shootdata) =>
		{
			if(shootdata.isShooting)
			{
				var bullet = ecb.Instantiate(entityInQueryIndex, bulletSettings.Bullet);
				ecb.SetComponent(entityInQueryIndex, bullet, new Translation { Value = translation.Value + new float3 { y = 1.5f } });
				ecb.SetComponent(entityInQueryIndex, bullet, new MoveData { direction = bulletSettings.PlayerDirection, speed = bulletSettings.PlayerSpeed });
			}
		}).ScheduleParallel();


		this.CompleteDependency();

		endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
	}
}
