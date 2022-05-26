using libs;
using Octokit;


namespace Libs
{
    public class GithubApi
    {

        public static Credentials auth = new Credentials(Credenciales.token);
        public static GitHubClient api = new GitHubClient(new ProductHeaderValue("user-agent"));

        private static string user = "RodriEs11";
        public static string repo = "Femecon";



        public static string obtenerNotasLastRelease()
        {


            return api.Repository.Release.GetLatest(user, repo).Result.Body.ToString();

        }

        public static string obtenerUltimaVersion()
        {


            api.Credentials = auth;

            return api.Repository.Release.GetLatest(user, repo).Result.TagName;

        }

    }
}
