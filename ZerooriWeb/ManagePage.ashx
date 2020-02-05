<%@ WebHandler Language="C#" Class="ManagePage" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;
using System.IO;

public class ManagePage : IHttpHandler
{
    #region Properties
    public HttpContext Context { get; private set; }
    public bool IsReusable { get { return false; } }
    #endregion



    #region Methods 

    //Loading
    public void ProcessRequest(HttpContext context)
    {
        this.Context = context;

         

        this.doLoad();
    }
    #endregion



    #region Private Methods
    private void doLoad()
    {

        this.Context.Response.ContentType = "application/json";

        string Key = "";

        if ((this.Context.Request.QueryString).Keys.Count > 0)
            Key = (this.Context.Request.QueryString).AllKeys[0];   // OnLoad: JSON.stringify($scope.ViewData.FilterData)


        string Mode = "LO";
        if (Key != "")
        {
            String JsonData = this.Context.Request[Key];// Key ==  OnLoad

            if (Key == "LoadInit")
            {
                ZA3010D UserFltr = JsonToObject<ZA3010D>(JsonData);
                ZA3010D UserVew = new ZA3010D();

                Mode = "LD";
                ZA3010M UserPrflM = new ZA3010M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }

            else if (Key == "SaveData")
            {
                ZA3010D UserFltr = JsonToObject<ZA3010D>(JsonData);
                ZA3010D UserVew = new ZA3010D();

                Mode = "SD";

                Stream fs = this.Context.Request.InputStream;

                long FileLength = fs.Length;
                ZA3010M UserPrflM = new ZA3010M();
                UserVew = UserPrflM.DoSave(UserFltr, Mode,FileLength);
                    
                    
                String FilePath = UserVew.BannerImage;

                if (FilePath != "")
                {
                        
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath;

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath ;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    File.WriteAllBytes(filePath, bytes);

                }
                
                this.Context.Response.Write(ObjectToJson(UserVew));

            }
           else if (Key == "SaveLogo")
            {
                ZA3010D UserFltr = JsonToObject<ZA3010D>(JsonData);
                ZA3010D UserVew = new ZA3010D();

                Mode = "SD";

                Stream fs = this.Context.Request.InputStream;

                long FileLength = fs.Length;
                ZA3010M UserPrflM = new ZA3010M();
                UserVew = UserPrflM.DoSave(UserFltr, Mode,FileLength);
                    
                String FilePath = UserVew.BannerImage;

                if (FilePath != "")
                {
                        
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath;

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath ;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    File.WriteAllBytes(filePath, bytes);

                }
                
                this.Context.Response.Write(ObjectToJson(UserVew));

            }
           

            else if (Key == "SignOut")
            {
                ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
                ZA3240D UserVew = new ZA3240D();

                Mode = "SO";
                ZA3240M UserPrflM = new ZA3240M();
                UserVew = UserPrflM.DoSave(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "GetC")
            {

                ZA3240D UserVew = new ZA3240D();
                UserVew.UserData.ZaBase.ZaKey = Utils.GetKey();
                UserVew.UserData.ZaBase.ErrorMsg = "";
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
        }
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

    #endregion
}
