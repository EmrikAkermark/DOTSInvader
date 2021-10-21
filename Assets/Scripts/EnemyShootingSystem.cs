using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShootingSystem : SystemBase
{
	EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;
	Unity.Mathematics.Random RandomData;

	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		RandomData = new Unity.Mathematics.Random(1);
		endSimulationEcbSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

	}

	protected override void OnUpdate()
	{
		var enemySettings = GetSingleton<EnemySpawnSettings>();
		var bulletSettings = GetSingleton<BulletSpawnSettings>();
		float deltaTime = Time.DeltaTime;
		float newShotDelay = RandomData.NextFloat(1f, 3f);

		var ecb = endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

		Entities.ForEach((int entityInQueryIndex, ref EnemyData enemyData, in Translation translation) =>
		{
			enemyData.TimeSinceLastShot += deltaTime;
			if (enemyData.TimeSinceLastShot > enemyData.ShotDelay )
			{
				enemyData.UpdateShotDelay(newShotDelay);
				var bullet = ecb.Instantiate(entityInQueryIndex, bulletSettings.Bullet);
				ecb.SetComponent(entityInQueryIndex, bullet, new Translation { Value = translation.Value - new float3 { y = 1.5f } });
				ecb.SetComponent(entityInQueryIndex, bullet, new MoveData { direction = bulletSettings.EnemyDirection, speed = bulletSettings.EnemySpeed });
			}
		}).ScheduleParallel();

		this.CompleteDependency();

		endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);


	}
}
