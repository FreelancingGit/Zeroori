 <%@ WebHandler Language="C#" CodeBehind="VSTERPUploadHandler.ashx.cs" Class="VSTERP.VSTERPUploadHandler" %>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace VSTERP
{
    public class VSTERPUploadHandler : IHttpHandler
    {
        #region Properties

        public bool IsReusable { get { return true; } }

        #endregion

        #region Methods

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string filename = string.Empty;
                if (context.Request.Files.Count > 0)
                {
                    if (context.Request.Files.Count > 0)
                    {
                        HttpPostedFile file = context.Request.Files[0];
                        string folder = context.Server.MapPath("docs\\");
                        if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
                        filename = folder + Guid.NewGuid().ToString() + "-" + file.FileName.Replace(" ", "");
                        file.SaveAs(filename);
                    }
                }
                context.Response.ContentType = "text/plain";
                if (string.IsNullOrWhiteSpace(filename) == false)
                { context.Response.Write(Path.GetFileName(filename)); }
            }
            catch { context.Response.Write(""); }
        }

        #endregion
    }
}