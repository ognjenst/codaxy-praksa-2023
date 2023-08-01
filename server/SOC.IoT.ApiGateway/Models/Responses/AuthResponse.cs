namespace SOC.IoT.ApiGateway.Models.Responses
{
	public class AuthResponse
	{
		public string Jwt { get; set; }
		public DateTime ExpirationTime { get; set; }
	}
}
