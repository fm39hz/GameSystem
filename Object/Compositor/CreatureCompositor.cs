using System;
using GameSystem.Utils;
using GameSystem.Component.DamageSystem;
using GameSystem.Object.Decorator;
using GameSystem.Data.Instance;
using Godot;

namespace GameSystem.Object.Compositor;

[GlobalClass]
public partial class CreatureCompositor : ObjectCompositor
{
	[Export] public float Health { get; set; }
	public HurtBox Hurtbox { get; set; }

	public override void _Ready()
	{
		base._Ready();
		if (Decorator is not Creature _target)
		{
			throw new InvalidCastException("Target must be Creature");
		}
		Hurtbox = _target.GetFirstChild<HurtBox>();
		SpriteSheet.PolygonChanged += Hurtbox.UpdateCollision;
	}

	protected override void InformationInit()
	{
		var _bitmap = new Bitmap();
		_bitmap.CreateFromImageAlpha(SpriteSheet.Texture.GetImage());
		Information = new CreatureData
		{
			Health = Health,
			ShapePool = PolygonCreator.GetArea(SpriteSheet, _bitmap, Name)
		};
	}

	protected override void InformationUpdate()
	{
		base.InformationUpdate();
		if (Decorator is not Creature _target)
		{
			throw new InvalidCastException("Target must be Creature");
		}
		if (!_target.Velocity.IsEqualApprox(Vector2.Zero))
		{
			Information.Direction.SetDirection(_target.Velocity);
		}
	}
}