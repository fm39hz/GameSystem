using GameSystem.Component.DamageSystem;
using GameSystem.Data.Instance;
using GameSystem.Object.PhysicsBody;
using GameSystem.Utils;
using Godot;

namespace GameSystem.Object.Root;

[GlobalClass]
public partial class CreatureRoot : ObjectRoot
{
	[Export] public float Health { get; set; }
	public HurtBox? Hurtbox { get; set; }

	public override void _Ready()
	{
		base._Ready();
		if (PhysicsBody is not Creature _target)
		{
			throw new InvalidCastException("Target must be Creature");
		}
		Hurtbox = _target.GetFirstChild<HurtBox>();
		SpriteSheet!.PolygonChanged += Hurtbox.UpdateCollision;
	}

	protected override void InitInformation()
	{
		var _bitmap = new Bitmap();
		_bitmap.CreateFromImageAlpha(SpriteSheet!.Texture.GetImage());
		Information = new CreatureData
		{
			Health = Health,
			ShapePool = PolygonCreator.GetArea(SpriteSheet, _bitmap, Name)
		};
	}

	protected override void UpdateInformation()
	{
		base.UpdateInformation();
		if (PhysicsBody is not Creature _target)
		{
			throw new InvalidCastException("Target must be Creature");
		}
		if (!_target.Velocity.IsEqualApprox(Vector2.Zero))
		{
			Information!.Direction!.SetDirection(_target.Velocity);
		}
	}
}