using Unity.Entities;
using Unity.Collections;


public class PlayerSpawnSystem : SystemBase
{
	protected override void OnUpdate()
	{
		var settings = GetSingleton<PlayerSpawnSettings>();
		EntityManager.Instantiate(settings.Player);

		EntityManager.DestroyEntity(settings.Player);
		Enabled = false;
	}
}
