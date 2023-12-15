using System.Web;

namespace OAuthServer.Endpoints;

public static class GetLogin
{
    public static async Task Handler(string returnUrl, HttpResponse response)
    {
        response.Headers.ContentType = new string[] { "text/html" };

        string htmlContent = $@"
                <html>
                    <head>
                        <title>Login</title>
                    </head>
                    <body>
                        <form action=""/login?returnUrl={HttpUtility.UrlEncode(returnUrl)}"" method=""post"">
                            <input value=""Submit"" type=""submit"" />
                        </form>
                    </body>
                </html>";

        await response.WriteAsync(htmlContent);
    }
}