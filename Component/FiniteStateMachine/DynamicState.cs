using System;
using GameSystem.Object.Compositor;
using Godot;

namespace GameSystem.Component.FiniteStateMachine;

[GlobalClass]
public partial class DynamicState : State
{
	public override void _EnterTree()
	{
		base._EnterTree();
		try
		{
			Compositor = GetOwner<ObjectCompositor>();
		}
		catch (NullReferenceException)
		{
			GD.Print("Cannot find any Compositor");
			throw;
		}
	}

	public override void EnteredMachine()
	{
		// throw new System.NotImplementedException();
	}

	protected override void UpdateCondition(double delta)
	{
		// throw new System.NotImplementedException();
	}

	public override void ExitMachine()
	{
		// throw new System.NotImplementedException();
	}
}