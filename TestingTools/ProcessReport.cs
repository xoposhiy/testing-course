using System.Linq;

namespace Legacy.ProviderProcessing
{
	public class ProcessReport
	{
		public readonly bool Success;
		public readonly string Error;
		public readonly string[] ProductErrors;

		public ProcessReport(bool success, string error, params string[] productErrors)
		{
			Success = success;
			Error = error;
			ProductErrors = productErrors;
		}

		protected bool Equals(ProcessReport other)
		{
			return Success == other.Success && string.Equals(Error, other.Error) && ProductErrors.SequenceEqual(other.ProductErrors);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ProcessReport) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Success.GetHashCode();
				hashCode = (hashCode*397) ^ (Error != null ? Error.GetHashCode() : 0);
				return hashCode;
			}
		}

		public override string ToString()
		{
			return $"Success: {Success}, Error: {Error}, ProductErrors:\r\n{string.Join("\r\n", ProductErrors)}";
		}
	}
}