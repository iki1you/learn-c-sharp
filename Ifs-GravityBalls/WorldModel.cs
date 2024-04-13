using System;

namespace GravityBalls
{
	public class WorldModel
	{
		public double BallX;
		public double BallY;
		public double BallRadius;
		public double WorldWidth;
		public double WorldHeight;

		public void SimulateTimeframe(double dt)
		{
			BallY = Math.Min(BallY + 200*dt, WorldHeight - BallRadius);
		}
	}
}