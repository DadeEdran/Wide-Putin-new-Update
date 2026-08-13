using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool E;
		public bool Enter;
		public bool R;
		public bool F;
		public bool Escape;
		public bool Alpha1;
		public bool Alpha2;
		public bool Alpha3;
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		public void OnE(InputValue value)
		{
			EInput(value.isPressed);
		}
		public void OnEnter(InputValue value)
		{
			EnterInput(value.isPressed);
		}
		public void OnR(InputValue value)
		{
			RInput(value.isPressed);
		}

		public void OnF(InputValue value)
		{
			FInput(value.isPressed);
		}
				public void OnEscape(InputValue value)
		{
			EscapeInput(value.isPressed);
		}
		public void OnAlpha1(InputValue value)
		{
			Alpha1Input(value.isPressed);
		}
		public void OnAlpha2(InputValue value)
		{
			Alpha2Input(value.isPressed);
		}
		public void OnAlpha3(InputValue value)
		{
			Alpha3Input(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}
		public void EInput(bool EState)
		{
			E = EState;
		}
		public void EnterInput(bool EnterState)
		{
			Enter = EnterState;

		}

		public void RInput(bool RState)
		{
			R = RState;

		}
		public void FInput(bool FState)
		{
			F = FState;

		}
		public void EscapeInput(bool EscapeState)
		{
			Escape = EscapeState;

		}
		public void Alpha1Input(bool Alpha1State)
		{
			Alpha1 = Alpha1State;

		}
		public void Alpha2Input(bool Alpha2State)
		{
			Alpha2 = Alpha2State;

		}
		public void Alpha3Input(bool Alpha3State)
		{
			Alpha3 = Alpha3State;

		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}