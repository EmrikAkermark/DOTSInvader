
using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemySpawnSettings : IComponentData
{
	public Entity Enemy;
	public float TimeBetweenSpawns;
}
