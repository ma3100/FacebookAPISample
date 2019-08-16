using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGraphAPI_CSharp
{
	public interface IFacebookService
	{
		Task<Account> GetAccountAsync(string accessToken);
		Task PostOnWallAsync(string accessToken, string message);
	}

	public class FacebookService : IFacebookService
	{
		private readonly IFacebookClient _facebookClient;

		public FacebookService(IFacebookClient facebookClient)
		{
			_facebookClient = facebookClient;
		}

		public async Task<Account> GetAccountAsync(string accessToken)
		{
			var result = await _facebookClient.GetAsync<dynamic>(accessToken,"me","fields=id,name,email,first_name,last_name");
			if(result == null)
			{
				return new Account();
			}

			return new Account()
			{
				Id = result.id,
				Email = result.email
			};
		}

		public async Task PostOnWallAsync(string accessToken, string message)
			=> await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
		
	}
}
