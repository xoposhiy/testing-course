namespace ConwaysGameOfLife
{
	public interface IReadonlyField
	{
		bool IsAlive(int x, int y);
	}
}