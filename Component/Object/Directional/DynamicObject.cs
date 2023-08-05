using Godot;
using System;
using GameSystem.Data.Instance;
using GameSystem.Component.Animation;
using GameSystem.Component.FiniteStateMachine;
using GameSystem.Component.Manager;
using GameSystem.Component.Object.Equipment;

namespace GameSystem.Component.Object.Directional;
	[GlobalClass]
	/// <summary>
	/// Object động, có State Machine & Animation
	/// </summary>
	public partial class DynamicObject : BaseObject{
		/// <summary>
		/// Kiểm soát signal từ input
		/// </summary>
		public InputManager InputManager { get; protected set; }
		/// <summary>
		/// Sprite Sheet của đối tượng
		/// </summary>
		public SpriteSheet Sheet { get; protected set; }
		/// <summary>
		/// State Machine của đối tượng
		/// </summary>
		public StateMachine StateMachine { get; protected set; }
		/// <summary>
		/// Metadata, chứa thông tin về State ID, hướng nhìn của object, Animation có loop hay không,...
		/// </summary>
		public ObjectData Information { get; protected set; }
		public Weapon Weapon { get; set; }
		[Export] public bool FourDirectionAnimation { get; protected set; } = true;
		public override void _EnterTree(){
			try{
				Sheet = GetFirstChildOfType<SpriteSheet>();
				InputManager = GetFirstChildOfType<InputManager>();
				}
			catch (InvalidCastException CannotGetSpriteSheet){
				GD.Print("Không thể cast tới Sprite Sheet & Player Input Manager");
				throw CannotGetSpriteSheet;
				}
			catch (NullReferenceException DontHaveSpriteSheet){
				GD.Print("Chưa có Sprite Sheet & Player Input Manger");
				throw DontHaveSpriteSheet;
				}
			}
		public override void _Ready(){
			try{
				StateMachine = GetFirstChildOfType<StateMachine>();
				Information = new(){
					IsFourDirection = FourDirectionAnimation,
					};
				}
			catch (InvalidCastException CannotGetStateMachine){
				GD.Print("Không thể cast tới State Machine");
				throw CannotGetStateMachine;
				}
			catch (NullReferenceException DontHaveStateMachine){
				GD.Print("Chưa có State Machine");
				throw DontHaveStateMachine;
				}
			}
		public override void _PhysicsProcess(double delta){
			UpdateMetadata();
			ActiveAnimation();
			MoveAndSlide();
			}
		/// <summary>
		/// Cập nhật Metadata của đối tượng
		/// </summary>
		protected void UpdateMetadata(){
			try {
				Information.CurrentState = StateMachine.CurrentState;
					if (!Velocity.IsEqualApprox(Vector2.Zero)){
						Information.SetDirection(Velocity);
						}
				}
			catch (NullReferenceException CurrentStateMissing){
				GD.Print("Không thể tìm thấy State hiện tại của đối tượng: \'" + Name + "\'");
				throw CurrentStateMissing;
				}
			}
		public void Transition(){
			Information.Transitioning = !Information.Transitioning;
				GD.Print(Information.Transitioning);
			}
		/// <summary>
		/// Animate Sprite Sheet dựa trên thông tin lấy được từ method UpdateMetadata
		/// </summary>
		protected void ActiveAnimation(){
			try {
				var _state = StateMachine.CurrentState;
				var _frame = _state.Frame;
					Sheet.Animate(_frame, Information);
				}
			catch (NullReferenceException CurrentStateMissing){
				GD.Print("Không thể tìm thấy State hiện tại của đối tượng: \'" + Name + "\'");
				throw CurrentStateMissing;
				}
			}
		}
 