<%@ WebHandler Language="C#" Class="packages" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;

public class packages : IHttpHandler
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
                ZA2010D UserFltr = JsonToObject<ZA2010D>(JsonData);
                ZA2010LD UserVew = new ZA2010LD();

                Mode = "LO";
                ZA2010M UserPrflM = new ZA2010M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "Subscrb")
            {
                ZA2010D UserFltr = JsonToObject<ZA2010D>(JsonData);
                ZA2010LD UserVew = new ZA2010LD();

                Mode = "I";
                ZA2010M UserPrflM = new ZA2010M();
                UserVew = UserPrflM.DoSubscribe(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
             else if (Key == "SubscrbA")
            {
                ZA2010D UserFltr = JsonToObject<ZA2010D>(JsonData);
                ZA2010LD UserVew = new ZA2010LD();

                Mode = "A";
                ZA2010M UserPrflM = new ZA2010M();
                UserVew = UserPrflM.DoSubscribe(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "LoadData")
            {
                ZA2010D UserFltr = JsonToObject<ZA2010D>(JsonData);
                ZA2011LD UserVew = new ZA2011LD();

                Mode = "LD";
                ZA2010M UserPrflM = new ZA2010M();
                UserVew = UserPrflM.DoLoadData(UserFltr, Mode);
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
