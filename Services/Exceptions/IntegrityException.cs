using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace SallesWebApp.Services.Exceptions
{
	public class IntegrityException : ApplicationException
	{
		public IntegrityException(string message) : base(message)
		{
		}
	}
}
