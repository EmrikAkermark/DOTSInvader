using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BulletSpawnSettings : IComponentData
{
	public Entity Bullet;

	public float EnemySpeed;
	public float3 EnemyDirection;

	public float PlayerSpeed;
	public float3 PlayerDirection;
}
