using Unity.Entities;
using Unity.Transforms;

public class MoveSystem : SystemBase
{
	protected override void OnUpdate()
	{
		var deltaTime = Time.DeltaTime;

		Entities.ForEach((ref Translation translation ,in MoveData moveData) =>
		{
			translation.Value += moveData.direction * moveData.speed * deltaTime;
		}).ScheduleParallel();
	}
}
