using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Facebook;
using Microsoft.Extensions.Configuration;

namespace FacebookGraphAPI_CSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = GetConfiguration();
			var accessToken = config.GetSection("AppSettings")["Facebook_AccessKey"];

			var facebookClient = new FacebookClient();
			var facebookService = new FacebookService(facebookClient);
			var getAccountTask =  facebookService.GetAccountAsync(accessToken);
			Task.WhenAll(getAccountTask);
			var result = getAccountTask.Result;
		}

		static IConfiguration GetConfiguration()
		{
			var configBuilder = new ConfigurationBuilder();

			// 設定ファイルのベースパスをカレントディレクトリ( 実行ファイルと同じディレクトリ )にします。
			configBuilder.SetBasePath(Directory.GetCurrentDirectory());

			// Json ファイルへのパスを設定します。SetBasePath() で設定したパスからの相対パスになります。
			configBuilder.AddJsonFile(@"AppConfig.json");

			return configBuilder.Build();
		}
	}
}
