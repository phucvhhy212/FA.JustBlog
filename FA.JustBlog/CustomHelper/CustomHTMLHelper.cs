using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.CustomHelper
{
	public static class CustomHTMLHelper
	{


		public static TagBuilder TagLink(this IHtmlHelper helper, string url, string linkText)
		{
			var a = new TagBuilder("a");
			a.Attributes.Add("href", url);
			a.Attributes.Add("class", "mx-2 p-1 text-white bg-black");

            a.Attributes.Add("style", "text-decoration: underline;");

			a.InnerHtml.Append(linkText);
			return a;
		}



		public static TagBuilder CategoryLink(this IHtmlHelper helper, string url, string linkText)
		{
			var a = new TagBuilder("a");
			a.Attributes.Add("href", url);

			a.Attributes.Add("style", "text-decoration: underline");
			a.InnerHtml.Append(linkText);
			return a;
		}


	}
}
