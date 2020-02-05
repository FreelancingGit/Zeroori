angular.module("ZerooriApp", ['ngCookies']).controller("AddDeal", function ($scope, $http, $cookies) {


    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'add-deal';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;

     
    $scope.SelectedData = {

        PackDealMastId: "",
        DealMastId: "",
        DealName: "",
        Price: "",
        Descrptn: "",
        StartDt: "",
        EndDt: "",
        UserData: {
            ZaBase: {
                SessionId: ''
            }
        }
    }


    if ($scope.urlArray.length > 1) {
        $scope.PrevPage = $scope.urlArray[0].replace('url=', '');
    }

    $scope.SelectedData.PackDealMastID = $cookies.get("MYPROD");
    $scope.SelectedData.DealMastId = $cookies.get("DEALID");

    $(function () { $("#upload_link1").on('click', function (e) { e.preventDefault(); $("#upload1:hidden").trigger('click'); }); });
    $(function () { $("#upload_link2").on('click', function (e) { e.preventDefault(); $("#upload2:hidden").trigger('click'); }); });
    $(function () { $("#upload_link3").on('click', function (e) { e.preventDefault(); $("#upload3:hidden").trigger('click'); }); });
    $(function () { $("#upload_link4").on('click', function (e) { e.preventDefault(); $("#upload4:hidden").trigger('click'); }); });
    $(function () { $("#upload_link5").on('click', function (e) { e.preventDefault(); $("#upload5:hidden").trigger('click'); }); });
    $(function () { $("#upload_link6").on('click', function (e) { e.preventDefault(); $("#upload6:hidden").trigger('click'); }); });
    $(function () { $("#upload_link7").on('click', function (e) { e.preventDefault(); $("#upload7:hidden").trigger('click'); }); });
    $(function () { $("#upload_link8").on('click', function (e) { e.preventDefault(); $("#upload8:hidden").trigger('click'); }); });
    $(function () { $("#upload_link9").on('click', function (e) { e.preventDefault(); $("#upload9:hidden").trigger('click'); }); });
    $(function () { $("#upload_link10").on('click', function (e) { e.preventDefault(); $("#upload10:hidden").trigger('click'); }); });
    $(function () { $("#upload_link11").on('click', function (e) { e.preventDefault(); $("#upload11:hidden").trigger('click'); }); });
    $(function () { $("#upload_link12").on('click', function (e) { e.preventDefault(); $("#upload12:hidden").trigger('click'); }); });
    $(function () { $("#upload_link13").on('click', function (e) { e.preventDefault(); $("#upload13:hidden").trigger('click'); }); });
    $(function () { $("#upload_link14").on('click', function (e) { e.preventDefault(); $("#upload14:hidden").trigger('click'); }); });
    $(function () { $("#upload_link15").on('click', function (e) { e.preventDefault(); $("#upload15:hidden").trigger('click'); }); });
    $(document).ready(function () { $("#upload1").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload1', "#upload1", 1); } }) })
    $(document).ready(function () { $("#upload2").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload2', "#upload2", 2); } }) })
    $(document).ready(function () { $("#upload3").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload3', "#upload3", 3); } }) })
    $(document).ready(function () { $("#upload4").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload4', "#upload4", 4); } }) })
    $(document).ready(function () { $("#upload5").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload5', "#upload5", 5); } }) })
    $(document).ready(function () { $("#upload6").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload6', "#upload6", 6); } }) })
    $(document).ready(function () { $("#upload7").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload7', "#upload7", 7); } }) })
    $(document).ready(function () { $("#upload8").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload8', "#upload8", 8); } }) })
    $(document).ready(function () { $("#upload9").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload9', "#upload9", 9); } }) })
    $(document).ready(function () { $("#upload10").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload10', "#upload10", 10); } }) })
    $(document).ready(function () { $("#upload11").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload11', "#upload11", 11); } }) })
    $(document).ready(function () { $("#upload12").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload12', "#upload12", 12); } }) })
    $(document).ready(function () { $("#upload13").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload13', "#upload13", 13); } }) })
    $(document).ready(function () { $("#upload14").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload14', "#upload14", 14); } }) })
    $(document).ready(function () { $("#upload15").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload15', "#upload15", 15); } }) })

    $scope.ReadImage = function (file, ImgSrcNam, UploadFileName, Seq) {
        $('#myModal').modal('show');
        $scope.Uploadimage(UploadFileName, Seq, ImgSrcNam);
    }

    $scope.Uploadimage = function (UploadFileName, Seq, ImgSrcNam) {
        var file = $(UploadFileName).get(0).files;
        $scope.ViewData.AdSeq = Seq;
        $http({
            method: 'POST',
            url: 'AddDeal.ashx',
            data: file[0],
            params: {
                SaveImg: JSON.stringify($scope.ViewData)
            }
        }).then(function successCallback(response) {
            if ($scope.isValidSave(response)) {
                $('#myModal').modal('hide');
                $.each(response.data.FileNames, function (name, value) {
                    var pos = value.ValMembr;
                    var impPath = value.DisPlyMembr
                    if (pos == 1) { $('#Imgupload1').attr('src', impPath); }
                    else if (pos == 2) { $('#Imgupload2').attr('src', impPath); }
                    else if (pos == 3) { $('#Imgupload3').attr('src', impPath); }
                    else if (pos == 4) { $('#Imgupload4').attr('src', impPath); }
                    else if (pos == 5) { $('#Imgupload5').attr('src', impPath); }
                    else if (pos == 6) { $('#Imgupload6').attr('src', impPath); }
                    else if (pos == 7) { $('#Imgupload7').attr('src', impPath); }
                    else if (pos == 8) { $('#Imgupload8').attr('src', impPath); }
                    else if (pos == 9) { $('#Imgupload9').attr('src', impPath); }
                    else if (pos == 10) { $('#Imgupload10').attr('src', impPath); }
                    else if (pos == 11) { $('#Imgupload11').attr('src', impPath); }
                    else if (pos == 12) { $('#Imgupload12').attr('src', impPath); }
                    else if (pos == 13) { $('#Imgupload13').attr('src', impPath); }
                    else if (pos == 14) { $('#Imgupload14').attr('src', impPath); }
                    else if (pos == 15) { $('#Imgupload15').attr('src', impPath); }
                });
            }
        }, function errorCallback(response) {
            alert(response);
            $('#myModal').modal('hide');
        });
    };

    $http({
        method: 'POST',
        url: 'AddDeal.ashx',
        params: {
            GetC: JSON.stringify($scope.SelectedData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
        }
    }, function errorCallback(response) {
        alert(response);
        });


    $scope.SignIN = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
    }


    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
        }
    }

    $scope.SetDealData = function (response) {

        $scope.SelectedData.DealName = response.data.Dealcol[0].DealName;
        $scope.SelectedData.Price = response.data.Dealcol[0].Price;
        $scope.SelectedData.Descrptn = response.data.Dealcol[0].Descrptn;
        $scope.SelectedData.StartDt = response.data.Dealcol[0].StartDt;
        $scope.SelectedData.EndDt = response.data.Dealcol[0].EndDt;
    }


    $scope.LoadInit = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {
                $http({
                    method: 'POST',
                    url: 'AddDeal.ashx',
                    params: {
                        LoadInitA: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                    }    // User Login Mode

                    $scope.SetDealData(response)
                    //$scope.SetSaveData(response)

                }, function errorCallback(response) {
                    alert(response);
                    $scope.SetStatus(false);
                });
            }
            catch (err) {
                $scope.SetStatus(false);
                alert(err);
            }
        }
    }

  
    $scope.SaveData = function () {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {
                
                $http({
                    method: 'POST',
                    url: 'AddDeal.ashx',
                    params: {
                        SaveData: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    alert('Save Successfully');
                    $scope.SetSaveData(response)
                    //$cookies.remove("DEALID");
                    //$cookies.remove("MYPROD");
                    $scope.navigate('manage-deal');
                    
                }, function errorCallback(response) {
                    alert(response);
                    $scope.SetStatus(false);
                   
                });
            }
            catch (err) {
                $scope.SetStatus(false);
                alert(err);
            }
        }
    }

    $scope.SetSaveData = function (response) {

        $scope.SelectedData.DealName = response.data.DealName;
        $scope.SelectedData.Price = response.data.Price;
        $scope.SelectedData.Descrptn = response.data.Descrptn;
        $scope.SelectedData.StartDt = response.data.StartDt;
        $scope.SelectedData.EndDt = response.data.EndDt;
    }




    $scope.isValidSave = function (response) {
        if (response.data.UserData.ZaBase != undefined
            && response.data.UserData.ZaBase.ErrorMsg != undefined
            && response.data.UserData.ZaBase.ErrorMsg.trim() != "") {

            alert(response.data.ZaBase.ErrorMsg);
            return false;
        }
        return true
    }



    $scope.navigate = function (URL) {
        url = URL + '.html?url=' + $scope.Page;
        $(location).attr('href', url);
    };
})
