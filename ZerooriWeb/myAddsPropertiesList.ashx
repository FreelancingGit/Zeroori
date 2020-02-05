<%@ WebHandler Language="C#" Class="myAddsPropertiesList" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;

public class myAddsPropertiesList : IHttpHandler
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

            if (Key == "LoadUser")
            {
                ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
                ZA2990D UserVew = new ZA2990D();

                Mode = "SE";
                ZA2990M UserPrflM = new ZA2990M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "Init")
            {
                ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
                ZA3710ILD UserVew = new ZA3710ILD();

                Mode = "LO";
                ZA3710myaddsM UserPrflM = new ZA3710myaddsM();
                UserVew = UserPrflM.DoInit(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "DeleteProp")
            {
                ZA3710BD UserFltr = JsonToObject<ZA3710BD>(JsonData);
                ZA3710ILD UserVew = new ZA3710ILD();

                ZA3710myaddsM UserPrflM = new ZA3710myaddsM();
                UserVew = UserPrflM.DoDelete(UserFltr);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
           
            else if (Key == "LoadData")
            {
                ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
                ZA3710ILD UserVew = new ZA3710ILD();

                Mode = "LD";
                ZA3710myaddsM UserPrflM = new ZA3710myaddsM();
                UserVew = UserPrflM.DoInit(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
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
