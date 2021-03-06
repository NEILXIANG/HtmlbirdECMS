// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System;
using System.Text;
using System.Web;

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 为 JsonP HTTP 处理程序提供基类。
	/// </summary>
	public abstract class JsonPHttpHandlerBase : HttpHandlerBase<JsonPHttpHandlerArgs>
	{
		private const string _JSON_MIME = "application/json";

		/// <summary>
		/// 通过实现 <see cref="IHttpHandler"/> 接口的自定义 <see cref="IHttpHandler"/> 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context"></param>
		public override void ProcessRequest(HttpContext context)
		{
			base.ProcessRequest(context);

			this.Arguments = new JsonPHttpHandlerArgs();
		}

		/// <summary>
		/// 创建 JsonP 数据格式。
		/// </summary>
		/// <param name="jsonBuilder">包含 Json 数据的 <see cref="StringBuilder"/>。</param>
		/// <returns>返回包含 JsonP 格式的数据的 <see cref="StringBuilder"/>。</returns>
		public StringBuilder CreateJsonP(StringBuilder jsonBuilder)
		{
			if (jsonBuilder == null) throw new ArgumentNullException("jsonBuilder");

			jsonBuilder.Insert(0, "(");
			jsonBuilder.Insert(0, this.Arguments.CallbackName);
			jsonBuilder.Append(");");

			return jsonBuilder;
		}

		/// <summary>
		/// 将 <paramref name="jsonBuilder"/> 写入 HTTP 响应流。
		/// </summary>
		/// <param name="jsonBuilder">包含 Json 数据的 <see cref="StringBuilder"/>。</param>
		public void Output(StringBuilder jsonBuilder)
		{
			this.Response.ContentType = _JSON_MIME;
			this.Response.Write(jsonBuilder);
		}
	}
}