 <%@ WebHandler Language = "C#" Class="UploadImages" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;
using System.IO;
using System.Drawing;


public class UploadImages : IHttpHandler
{
    #region Properties
    public HttpContext Context { get; private set; }
    public bool IsReusable { get { return false; } }
    #endregion


    #region Methods 
    //Loading
    public void ProcessRequest(HttpContext context)
    {

        var jsonString = String.Empty;
        Stream fs = context.Request.InputStream;
        BinaryReader br = new BinaryReader(fs);
        byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //string filePath = "~/Files/" + Path.GetFileName(FileUpload1.FileName);
        File.WriteAllBytes("F:\\Works\\ZerooriWeb\\ZerooriWeb\\Bin\\Images\\Test.jpg", bytes);

        // context.Request.InputStream.Position = 0;
        //  using (var inputStream = new StreamReader(context.Request.InputStream))
        //  {
        //      jsonString = inputStream.ReadToEnd();
        //  }
        //  var bufferedReader = new BufferedStream(jsonString);



        ProductViewModel Obj = JsonToObject<ProductViewModel>(jsonString);

    }
    #endregion



    [System.Web.Services.WebMethod(EnableSession = true)]
    public String ImageUpload(ProductViewModel model)
    {
        var file = model.ImageFile;

        if (file != null)
        {
            var fileName = Path.GetFileName(file.FileName);
            var extention = Path.GetExtension(file.FileName);
            var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);

            // file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName) ;
            file.SaveAs("F:\\" + file.FileName);
        }
        return ObjectToJson(file.FileName );
    }


    private T JsonToObject<T>(string json)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Deserialize<T>(json);
    }

    private string ObjectToJson(object data)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(data);
    }
}




