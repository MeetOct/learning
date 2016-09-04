using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Learning.HtmlExtensions.Extentions
{
	public static class  ChexkBoxExtention
	{
		public static MvcHtmlString CheckList(this HtmlHelper helper, List<string> list,object attributes)
		{
			var sb = new StringBuilder();

			list.ForEach(l=> 
			{
				sb.AppendFormat(@"<label for={0}>xuanwo<input type='checkbox' id={0} name={0} value={0}></label> <br/>", l);
			});
			return MvcHtmlString.Create(sb.ToString());
		}
	}
}