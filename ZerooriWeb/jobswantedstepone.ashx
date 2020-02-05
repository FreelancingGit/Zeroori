<%@ WebHandler Language="C#" Class="jobswantedstepone" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;
using System.IO;

public class jobswantedstepone : IHttpHandler
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

        try
        {
            this.Context.Response.ContentType = "application/json";

            string Key = "";

            if ((this.Context.Request.QueryString).Keys.Count > 0)
                Key = (this.Context.Request.QueryString).AllKeys[0]; ; // OnLoad: JSON.stringify($scope.ViewData.FilterData)


            string Mode = "LO";
            if (Key != "")
            {
                String JsonData = this.Context.Request[Key];// Key ==  OnLoad

                if (Key == "LoadUser")
                {
                    ZA3630SD UserFltr = JsonToObject<ZA3630SD>(JsonData);
                    ZA3630LD UserVew = new ZA3630LD();

                    Mode = "LO";
                    ZA3630M UserPrflM = new ZA3630M();
                    UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                    this.Context.Response.Write(ObjectToJson(UserVew));
                }
                if (Key == "DoSave")
                {
                    ZA3630SD UserFltr = JsonToObject<ZA3630SD>(JsonData);
                    ZA3630SD UserVew = new ZA3630SD();

                    Mode = "LO";

                    Stream fs = this.Context.Request.InputStream;

                        long FileLength = fs.Length;
                    ZA3630M UserPrflM = new ZA3630M();
                    UserVew = UserPrflM.DoSave(UserFltr, Mode,FileLength);


                    String FilePath = UserVew.PhotoPath;

                    if (FilePath != "")
                    {
                        
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath;

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath + "/ProPic.Jpg";
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        File.WriteAllBytes(filePath, bytes);

                    }

                    this.Context.Response.Write(ObjectToJson(UserVew));
                }
                if (Key == "SignOut")
                {
                    ZA3630SD UserFltr = JsonToObject<ZA3630SD>(JsonData);
                    ZA3630SD UserVew = new ZA3630SD();

                    Mode = "SO";
                    ZA3630M UserPrflM = new ZA3630M();
                    UserVew = UserPrflM.DoSave(UserFltr, Mode, 0);
                    this.Context.Response.Write(ObjectToJson(UserVew));
                }
                else if (Key == "GetC")// Get cookies Key
                {

                    ZA2990D UserVew = new ZA2990D();
                    UserVew.UserData.ZaBase.ZaKey = Utils.GetKey();
                    UserVew.UserData.ZaBase.ErrorMsg = "";
                    this.Context.Response.Write(ObjectToJson(UserVew));
                }
            }
        }
        catch(Exception EX)
        {

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
