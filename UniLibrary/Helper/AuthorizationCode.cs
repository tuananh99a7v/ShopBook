namespace UniLibrary.Helper
{
	public class AuthorizationCode
	{
		public static void GetAuthorizationCode(long app_Id, string redirect_Uri, string code_Challenge, string state)
		{
			var respone = ApiHelper.GetApi("", "https://oauth.zaloapp.com/",
				$"v4/permission?app_id={app_Id}&redirect_uri=<{redirect_Uri}&code_challenge={code_Challenge}&state={state}").Content.ReadAsStringAsync().Result;
		}
	}
}
