<%@ WebHandler Language="C#" Class="FrelncHiringStepone" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;
using System.IO;

public class FrelncHiringStepone : IHttpHandler
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

            if (Key == "Init")
                {
                    ZA3650SD UserFltr = JsonToObject<ZA3650SD>(JsonData);
                    ZA3650LD UserVew = new ZA3650LD();

                    Mode = "LO";
                    ZA3680M UserPrflM = new ZA3680M();
                    UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                    this.Context.Response.Write(ObjectToJson(UserVew));
                }

            else if (Key == "SaveData")
            {
                    ZA3650SD UserFltr = JsonToObject<ZA3650SD>(JsonData);
                    ZA3650LD UserVew = new ZA3650LD();


                    Mode = "SD";

                    Stream fs = this.Context.Request.InputStream;

                     long FileLength = fs.Length;
                    ZA3680M UserPrflM = new ZA3680M();
                    UserFltr = UserPrflM.DoSave(UserFltr, Mode,FileLength);




                    
                    String FilePath = UserFltr.PhotoPath;

                    if (FilePath != "")
                    {
                        
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath;

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        filePath = HttpContext.Current.Server.MapPath("~") + "/" + FilePath + "/CompanyLogo.Jpg";
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        File.WriteAllBytes(filePath, bytes);

                    }
                    this.Context.Response.Write(ObjectToJson(UserFltr));
            }
           
             
            else if (Key == "SignOut")
            {
                ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
                ZA2990D UserVew = new ZA2990D();

                Mode = "SO";
                ZA2990M UserPrflM = new ZA2990M();
                UserVew = UserPrflM.DoSave(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "GetC")
            {

                ZA2990D UserVew = new ZA2990D();
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
