namespace GameSystem.Data.Instance;

/// <summary>
/// Chứa thông tin về frame của 1 đối tượng
/// </summary>
public class FrameData
{
	/// <summary>
	/// Số frame của SpriteSheetPlayer
	/// </summary>
	/// <value></value>
	public int Length { get; set; }

	public int TransitionFrame { get; set; }

	/// <summary>
	/// Tốc độ của SpriteSheetPlayer
	/// </summary>
	/// <value></value>
	public double Speed { get; set; }

	public FrameData(int frameNumber, double speed, int transitionFrame)
	{
		Length = frameNumber;
		Speed = speed;
		TransitionFrame = transitionFrame;
	}
}