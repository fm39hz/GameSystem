﻿using Godot;

namespace GameSystem.VirtualInstance;

public interface IDirectionalInput
{
	public Vector2 GetMovementVector(Vector2 inputVector);
	// public Vector2 GetJumpVector(Vector2 inputVector);
}