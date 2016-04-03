namespace TestingTools
{
	public class ProcessingReport
	{
		public ProcessingReport(bool success, string error)
		{
			Success = success;
			Error = error;
		}

		public bool Success { get; }
		public string Error { get; }

	}
}