<%@ WebHandler Language="C#" Class="ManageDeal" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;

public class ManageDeal : IHttpHandler
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

            if (Key == "LoadInitM")
            {
                ZA3011D UserFltr = JsonToObject<ZA3011D>(JsonData);
                ZA3011LD UserVew = new ZA3011LD();

                Mode = "LC";
                ZA3011M UserPrflM = new ZA3011M();
                //Load Deal Names In manage PAge
                UserVew = UserPrflM.DoLoadM(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "LoadInitA")
            {
                ZA3011D UserFltr = JsonToObject<ZA3011D>(JsonData);
                ZA3011LD UserVew = new ZA3011LD();

                Mode = "LO";
                ZA3011M UserPrflM = new ZA3011M();
                //Load Deal Names In manage PAge
                UserVew = UserPrflM.DoLoadA(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "LoadInitDeal")
            {
                ZA3011D UserFltr = JsonToObject<ZA3011D>(JsonData);
                ZA3011LD UserVew = new ZA3011LD();
                ZA3011M UserPrflM = new ZA3011M();

                Mode = "LO";
                UserVew = UserPrflM.DoLoadDeal(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));
            }
            else if (Key == "EditData")
            {
                ZA3011D UserFltr = JsonToObject<ZA3011D>(JsonData);
                ZA3011D UserVew = new ZA3011D();

                Mode = "U";
                ZA3011M UserPrflM = new ZA3011M();
                UserVew = UserPrflM.DoSave(UserFltr, Mode);
                this.Context.Response.Write(ObjectToJson(UserVew));

            }

            else if (Key == "DeleteDeal")
            {
                ZA3011D UserFltr = JsonToObject<ZA3011D>(JsonData);
                ZA3011LD UserVew = new ZA3011LD();

                ZA3011M UserPrflM = new ZA3011M();
                UserVew = UserPrflM.DoDelete(UserFltr, Mode);
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
