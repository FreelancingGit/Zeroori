﻿ <%@ WebHandler Language = "C#" Class="motorosstepthree" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ZerooriBO;
using System.IO;
using System.Drawing;


public class motorosstepthree : IHttpHandler
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

        this.Context.Response.ContentType = "application/json";
        doSaveImg();
    }
    #endregion



    private void doSaveImg()
    {
        ZA3601SD UserVew = new ZA3601SD();
        try
        {
            string Key = "";
            if ((this.Context.Request.QueryString).Keys.Count > 0)
                Key = (this.Context.Request.QueryString).AllKeys[0];   // OnLoad: JSON.stringify($scope.ViewData.FilterData)

            string Mode = "LO";
            if (Key != "")
            {
                String JsonData = this.Context.Request[Key];// Key ==  OnLoad

                if (Key == "SaveImg")
                {
                    ZA3601SD UserFltr = JsonToObject<ZA3601SD>(JsonData);
                    String SessionID = UserFltr.UserData.ZaBase.SessionId;
                    int? AddMotorsADMastID = UserFltr.AddMotorsADMastID;
                    String Seqance = UserFltr.AdSeq;

                    int? SeqCur = null;
                    if (Seqance != "")
                        SeqCur = Convert.ToInt32(UserFltr.AdSeq);

                    // User Data
                    Mode = "UD";
                    ZA3601M SaveAdd = new ZA3601M();
                    UserVew = SaveAdd.DoSave(UserFltr, Mode);
                    UserVew.UserData.ZaBase.ErrorMsg = "";

                    foreach (ComDisValD FileName in UserVew.FileNames)
                    {
                        if (!System.IO.Directory.Exists(FileName.Descriptn))
                        {
                            int LstIndex = FileName.Descriptn.LastIndexOf("/");
                            String Name = HttpContext.Current.Server.MapPath("~") + "/" + FileName.Descriptn.Substring(0, LstIndex);

                            if (!Directory.Exists(Name))
                            {
                                Directory.CreateDirectory(Name);
                            }
                        }

                        if (SeqCur == FileName.ValMembr)
                        {
                            Stream fs = this.Context.Request.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string filePath = HttpContext.Current.Server.MapPath("~") + "/" + FileName.Descriptn;
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                            File.WriteAllBytes(filePath, bytes);
                        }
                    }
                }
                if (Key == "SaveP")
                {
                    ZA3601SD UserFltr = JsonToObject<ZA3601SD>(JsonData);
                    String SessionID = UserFltr.UserData.ZaBase.SessionId;
                    int? AddMotorsADMastID = UserFltr.AddMotorsADMastID;
                    String Seqance = UserFltr.AdSeq;

                    

                    // User Data
                    Mode = "UP";
                    ZA3601M SaveAdd = new ZA3601M();
                    UserVew = SaveAdd.DoSave(UserFltr, Mode);
                    UserVew.UserData.ZaBase.ErrorMsg = "";
                }
                 if (Key == "Onload")
                {
                    ZA3601SD UserFltr = JsonToObject<ZA3601SD>(JsonData);

                    // User Data
                    Mode = "LO";
                    ZA3601M SaveAdd = new ZA3601M();
                    UserVew = SaveAdd.DoLoad(UserFltr, Mode);
                    UserVew.UserData.ZaBase.ErrorMsg = "";
                }
                if (Key == "RemoveImg")
                {
                    ZA3601SD UserFltr = JsonToObject<ZA3601SD>(JsonData);
                    String SessionID = UserFltr.UserData.ZaBase.SessionId;
                    int? AddMotorsADMastID = UserFltr.AddMotorsADMastID;
                    String Seqance = UserFltr.AdSeq;


                    // User Data
                    Mode = "RI";
                    ZA3601M SaveAdd = new ZA3601M();
                    UserVew = SaveAdd.DoRemove(UserFltr, Mode);
                    UserVew.UserData.ZaBase.ErrorMsg = "";

                    if (UserVew.FileNames.Count > 0)
                    {
                        string filePath = HttpContext.Current.Server.MapPath("~") + "/" + UserVew.FileNames[0].Descriptn;
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }
                else if (Key == "GetC")
                {
                    UserVew.UserData.ZaBase.ZaKey = Utils.GetKey();
                    UserVew.UserData.ZaBase.ErrorMsg = "";
                }
            }
        }
        catch (Exception Catch)
        {
            UserVew.UserData.ZaBase.ErrorMsg = Catch.Message;
        }
        this.Context.Response.Write(ObjectToJson(UserVew));
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




