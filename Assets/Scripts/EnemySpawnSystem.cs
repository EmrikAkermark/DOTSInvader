using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public class EnemySPawnSystem : SystemBase
{
	EnemySpawnSettings settings;
	float spawnDelay;
	Random randomData;
	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		settings = GetSingleton<EnemySpawnSettings>();
		randomData = new Random(1);
	}

	protected override void OnUpdate()
	{
		spawnDelay += (float)Time.ElapsedTime;

		if(spawnDelay < settings.TimeBetweenSpawns)
		{
			return;
		}
		else
		{
			spawnDelay %= settings.TimeBetweenSpawns;
		}

		Entity enemy = EntityManager.Instantiate(settings.Enemy);
		EntityManager.DestroyEntity(settings.Enemy);

		EntityManager.SetComponentData(enemy, new Translation { Value = new float3 { x = randomData.NextFloat(-10f, 10f), y = 10 } });

	}
}
