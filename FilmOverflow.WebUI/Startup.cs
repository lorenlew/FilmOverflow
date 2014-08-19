using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FilmOverflow.WebUI.Startup))]
namespace FilmOverflow.WebUI
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
