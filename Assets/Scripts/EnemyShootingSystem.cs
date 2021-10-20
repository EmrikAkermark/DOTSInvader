using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShootingSystem : SystemBase
{

	Unity.Mathematics.Random RandomData;
	protected override void OnStartRunning()
	{
		base.OnStartRunning();
		RandomData = new Unity.Mathematics.Random(1);

	}

	protected override void OnUpdate()
	{
		float deltaTime = Time.DeltaTime;
		float newShotDelay = RandomData.NextFloat(2f, 4f);
		Entities.ForEach((ref EnemyData enemyData, in Translation translation) =>
		{
			enemyData.TimeSinceLastShot += deltaTime;
			if (enemyData.TimeSinceLastShot > enemyData.ShotDelay )
			{
				Debug.Log($"I started shooting from {translation.Value}, my shotdelay was {enemyData.ShotDelay}");
				enemyData.UpdateShotDelay(newShotDelay);
			}
		}).ScheduleParallel();
	}
}
