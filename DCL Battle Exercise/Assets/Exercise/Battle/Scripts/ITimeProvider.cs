namespace Exercise.Battle.Scripts
{
	public interface ITimeProvider
	{
		public float GetTime();

		public float GetDeltaTime();
	}
}