using Godot;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem.Component.FiniteStateMachine;

[GlobalClass]
public partial class StateMachine : Node
{
	[Signal] public delegate void StateEnteredEventHandler();
	[Signal] public delegate void StateExitedEventHandler();
	[Export] public State InitualizedState { get; set; }
	
	public State CurrentState { get; protected set; }
	public State PreviousState { get; protected set; }
	public List<State> States { get; } = new();

	public override void _Ready()
	{
		foreach (var _target in GetChildren().OfType<State>())
		{
			States.Add(_target);
		}
		Init();
	}

	protected void Init()
	{
		foreach (var _selected in States)
		{
			StateEntered += _selected.EnteredMachine;
			_selected.StateRunning += CheckingCondition;
			StateExited += _selected.ExitMachine;
		}
		SelectState();
		CurrentState = InitualizedState;
		PreviousState = CurrentState;
	}

	protected void SelectState()
	{
		foreach (var _selected in States.Where(selected => selected.Condition))
		{
			CurrentState = _selected;
			EmitSignal(SignalName.StateEntered);
			return;
		}
	}

	protected void CheckingCondition()
	{
		if (CurrentState.Condition) return;
		PreviousState = CurrentState;
		EmitSignal(SignalName.StateExited);
		SelectState();
	}
}