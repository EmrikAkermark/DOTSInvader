using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemyData : IComponentData
{
	public float Health;
	public float ShotDelay;
	public float TimeSinceLastShot;

	public void UpdateShotDelay(float newShotDelay)
	{
		ShotDelay = newShotDelay;
		TimeSinceLastShot = 0f;
	}
}