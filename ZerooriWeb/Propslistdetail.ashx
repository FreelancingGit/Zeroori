<%@ WebHandler Language="C#" Class="Propslistdetail" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;

public class Propslistdetail : IHttpHandler
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
            Key = (this.Context.Request.QueryString).AllKeys[0]; ; // OnLoad: JSON.stringify($scope.ViewData.FilterData)


        string Mode = "LO";
        if (Key != "")
        {
            String JsonData = this.Context.Request[Key];// Key ==  OnLoad

            //if (Key == "LoadUser")
            //{
            //    ZA3000D UserFltr = JsonToObject<ZA3000D>(JsonData);
            //    ZA2990D UserVew = new ZA2990D();

            //    Mode = "SE";
            //    ZA2990M UserPrflM = new ZA2990M();
            //    UserVew = UserPrflM.DoLoad(UserFltr, Mode);
            //    this.Context.Response.Write(ObjectToJson(UserVew));
            //}
            //else 

            if (Key == "Init")
            {
                ZA3711LD UserFltr = JsonToObject<ZA3711LD>(JsonData);
                ZA3711LD UserVew = new ZA3711LD();

                Mode = "LD";
                ZA3711M UserPrflM = new ZA3711M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
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
