using System.Globalization;

namespace SOC.IoT.ApiGateway.Exceptions
{
	public class AppException : Exception
	{
		public AppException() : base() { }

		public AppException(string message) : base(message) { }

		public AppException(string message, params object[] args)
			: base(String.Format(CultureInfo.CurrentCulture, message, args))
		{
		}
	}
}
