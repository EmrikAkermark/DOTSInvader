using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public class EnemySPawnSystem : SystemBase
{
	Random RandomData;
	float SpawnDelay;
	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		RandomData = new Random(1);

	}

	protected override void OnUpdate()
	{
		var settings = GetSingleton<EnemySpawnSettings>();

		SpawnDelay += Time.DeltaTime;
		if(SpawnDelay < settings.TimeBetweenSpawns)
		{
			return;
		}
		else
		{
			SpawnDelay %= settings.TimeBetweenSpawns;
		}

		Entity enemy = EntityManager.Instantiate(settings.Enemy);
		EntityManager.SetComponentData(enemy, new Translation { Value = new float3 {x=RandomData.NextFloat(-10f, 10f), y = 10 } });

	}
}
