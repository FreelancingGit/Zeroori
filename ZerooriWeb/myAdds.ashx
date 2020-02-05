<%@ WebHandler Language="C#" Class="myAdds" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;

public class myAdds : IHttpHandler
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
                ZA3000D UserVew = new ZA3000D();

                Mode = "SE";
                ZA3000M UserPrflM = new ZA3000M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "Init")
            {
                ZA3800D UserFltr = JsonToObject<ZA3800D>(JsonData);
                ZA3800D UserVew = new ZA3800D();

                Mode = "LO";
                ZA3800M UserPrflM = new ZA3800M();
                UserVew = UserPrflM.DoLoad(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            
            else if (Key == "GetC")
            {

                ZA3000D UserVew = new ZA3000D();
                UserVew.ZaBase.ZaKey = Utils.GetKey();
                UserVew.ZaBase.ErrorMsg = "";
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
