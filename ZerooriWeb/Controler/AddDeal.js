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
	function readURL(input) {
		if (input.files && input.files[0]) {
			var reader = new FileReader();

			reader.onload = function (e) {
				$('#Imgupload1').attr('src', e.target.result);
			}

			reader.readAsDataURL(input.files[0]);
		}
	}
	$("#upload1").change(function () {
		readURL(this);
	});

	function readURL2(input) {
		if (input.files && input.files[0]) {
			var reader = new FileReader();

			reader.onload = function (e) {
				$('#Imgupload2').attr('src', e.target.result);
			}

			reader.readAsDataURL(input.files[0]);
		}
	}
	$("#upload2").change(function () {
		readURL2(this);
	});
 
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
    };

  
	$scope.SaveData = function ()
	{

		if (($scope.SessionId != null && $scope.SessionId != "")) {
			try {
				var file = $('#upload1').get(0).files;
				$http({
					method: 'POST',
					url: 'AddDeal.ashx',
					data: file[0],
					params: {
						SaveData: JSON.stringify($scope.SelectedData)
					}
				}).then(function successCallback(response) {
					$scope.SaveLogo(response.data.DealMastId);
					
					$scope.SetSaveData(response);
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
	};
	$scope.SaveLogo = function (dealId) {
		try {
			$scope.SelectedData.DealMastId = dealId;
			var file = $('#upload2').get(0).files;
			$http({
				method: 'POST',
				url: 'AddDeal.ashx',
				data: file[0],
				params: {
					SaveLogo: JSON.stringify($scope.SelectedData)
				}
			}).then(function successCallback(response) {
				alert('Save Successfully');
				//$cookies.remove("DEALID");
				//$cookies.remove("MYPROD");
				

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

	$scope.SetSaveData = function (response) {

		$scope.SelectedData.DealName = response.data.DealName;
		$scope.SelectedData.Price = response.data.Price;
		$scope.SelectedData.Descrptn = response.data.Descrptn;
		$scope.SelectedData.StartDt = response.data.StartDt;
		$scope.SelectedData.EndDt = response.data.EndDt;
	};




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
