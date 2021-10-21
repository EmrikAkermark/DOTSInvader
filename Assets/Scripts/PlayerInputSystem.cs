using System;
using Unity.Entities;
using UnityEngine;

public class PlayerInputSystem : SystemBase
{
	protected override void OnUpdate()
	{
		Entities.
			WithAll<PlayerTag>().
			ForEach((ref MoveData moveData, ref ShootData shootData, in InputData inputData) =>
			{
				bool isRightKeyPressed = Input.GetKey(inputData.rightKey);
				bool isLeftKeyPressed = Input.GetKey(inputData.leftKey);
				bool isUpKeyPressed = Input.GetKey(inputData.upKey);
				bool isDownKeyPressed = Input.GetKey(inputData.downKey);

				shootData.isShooting = Input.GetKeyDown(inputData.ShootKey);

				moveData.direction.x = Convert.ToInt16(isRightKeyPressed);
				moveData.direction.x -= Convert.ToInt16(isLeftKeyPressed);
				moveData.direction.y = Convert.ToInt16(isUpKeyPressed);
				moveData.direction.y -= Convert.ToInt16(isDownKeyPressed);

			}).Run();

	}
}