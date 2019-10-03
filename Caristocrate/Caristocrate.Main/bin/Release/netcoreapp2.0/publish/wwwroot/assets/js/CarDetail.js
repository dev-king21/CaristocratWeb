function uploadImage($this) {
        var filename = $($this).val();
        if (filename != "") {
            var path = filename;
            filename = path.replace(/^.*\\/, "");
            $($this).parents(".uploadInnerdiv").find(".uploadedimagename").text(filename);
        }
    }
    function AddMyCar($this) {

        var islogin = $("#EvaluationForm").val();
        if (islogin == "") {
            $(".btnShowLogin").trigger("click");
            return;
        }

        var txtUserNameMC = $("#txtUserNameMC").val();
        var txtEmailMC = $("#txtEmailMC").val();
        var txtPhoneNoMC = $("#txtPhoneNoMC").val();
        var PSPhoneCodeFIeld = $("#PSPhoneCodeFIeld").find(".selected-dial-code").text();;
        var dllMakeMC = $("#dllMakeMC").val();
        var ddlModelMC = $("#ddlModelMC").val();
        var ddlYearMC = $("#ddlYearMC").val();
        var ddlVersionMC = $("#ddlVersionMC").val();
        var txtKMMC = $("#txtKMMC").val();
        var txtChassisMC = $("#txtChassisMC").val();
        var ddlRegionalSpecsMC = $("#ddlRegionalSpecsMC").val();
        var ddlExteriorColorMC = $("#ddlExteriorColorMC").val();
        var ddlInteriorColorMC = $("#ddlInteriorColorMC").val();
        var ddlAccidentMC = $("#ddlAccidentMC").val();
        var txtWarrantyRemMC = $("#txtWarrantyRemMC").val();
        var txtServiceRemMC = $("#txtServiceRemMC").val();
        var txtNotesMC = $("#txtNotesMC").val();

        var validationcount = 0;
        if (txtUserNameMC == "") {
            $("#txtUserNameMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#txtUserNameMC").css("border", "none"); }

        if (txtEmailMC == "") {
            $("#txtEmailMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#txtEmailMC").css("border", "none"); }

        if (txtPhoneNoMC == "") {
            $("#txtPhoneNoMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#txtPhoneNoMC").css("border", "none"); }

        if (dllMakeMC == "0") {
            $("#dllMakeMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#dllMakeMC").css("border", "none"); }

        if (ddlModelMC == "0") {
            $("#ddlModelMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#ddlModelMC").css("border", "none"); }

        if (ddlYearMC == "0") {
            $("#ddlYearMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#ddlYearMC").css("border", "none"); }

        if (ddlRegionalSpecsMC == "0") {
            $("#ddlRegionalSpecsMC").css("border", "1px solid red");
            validationcount++;
        }
        else { $("#ddlRegionalSpecsMC").css("border", "none"); }

        if (validationcount > 0) {
            notify("Fill Required fileds", "danger");
            return;
        }

        $($this).hide();
        $(".loaderdiv").show();

        var formData = new FormData();
        formData.append('front', $('#fufrontMC')[0].files[0]); 
        formData.append('Back', $('#fuBackMC')[0].files[0]); 
        formData.append('Right', $('#fuRightMC')[0].files[0]); 
        formData.append('Left', $('#fuLeftMC')[0].files[0]); 
        formData.append('Interior', $('#fuInteriorMC')[0].files[0]); 
        formData.append('RegCard', $('#fuRegistrationCardMC')[0].files[0]); 
        formData.append('username', txtUserNameMC); 
        formData.append('email', txtEmailMC); 
        formData.append('countrycode', PSPhoneCodeFIeld); 
        formData.append('phone', txtPhoneNoMC); 
        formData.append('makeid', dllMakeMC); 
        formData.append('modelid', ddlModelMC); 
        formData.append('year', ddlYearMC); 
        formData.append('verionid', ddlVersionMC); 
        formData.append('KM', txtKMMC); 
        formData.append('Chassis', txtChassisMC); 
        formData.append('regionalSpecid', ddlRegionalSpecsMC); 
        formData.append('exteriorcolorid', ddlExteriorColorMC); 
        formData.append('interiorcolorid', ddlInteriorColorMC); 
        formData.append('accidentid', ddlAccidentMC); 
        formData.append('warrantyrem', txtWarrantyRemMC); 
        formData.append('servicerem', txtServiceRemMC); 
        formData.append('notes', txtNotesMC); 
        
        var _url = '@Url.Action("AddMyCar", "Car")';

        $.ajax({
            url: _url,
            type: 'POST',
            data: formData,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (result) {
                $($this).show();
                $(".loaderdiv").hide();
            },
            error: function (jqXHR) {

            },
            complete: function (jqXHR, status) {
                $($this).show();
                $(".loaderdiv").hide();

                GetCustomerMyCars();
            }
        });

    }

    function GetModels($this) {

        var makeid = $($this).find("option:selected").val();

        $("#ddlModelMC").find("option").each(function () {
            $(this).hide();
        })

        $("#ddlModelMC").val(0);

        $("#ddlModelMC").find("option").each(function () {
            if ($(this).attr("brandID") == makeid) {
                $(this).show();
            }
        })
    }

    function OnYearChange($this) {

        var makeid = $("#ddlMakeMC").find("option:selected").val();
        var modelid = $("#ddlModelMC").find("option:selected").val();
        var Year = $($this).find("option:selected").val();

        if (makeid == "0") {
            $("#ddlMakeMC").css("border", "1px solid red");
            return;
        }
        else { 
            $("#ddlMakeMC").css("border", "none");
        }

        if (modelid == "0") {
            $("#ddlModelMC").css("border", "1px solid red");
            return;
        }
        else {
            $("#ddlModelMC").css("border", "none");
        }


        $("#ddlVersionMC").html("");
        $("#ddlVersionMC").html('<option value="0">Version (Optional)</option>');

        var data = {};
        data.categoryID = 0;
        data.makeid = makeid;
        data.modelid = modelid;
        data.minyear = Year;
        data.masyear = Year;

        $.ajax({
            dataType: "json",
            async: true,
            type: 'GET',
            data: data,
            url: '@Url.Action("CarsforCompareCar", "Car")',
            type: 'GET',
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#ddlVersionMC").append('<option  value="' + data[i].carid + '">' + data[i].Version + '</option>');
                }
            },
        });
    }

    function CHeckLogin() { 
        var islogin = $("#EvaluationForm").val();
        if (islogin == "") {
            $(".btnShowLogin").trigger("click");
        }
    }

    $(window).bind("load", function () {
        


        $("#ddlModelMC").find("option").each(function () {
            $(this).hide();
        });

        var mySwiper = new Swiper('.slider_wrap.swiper-container', {
            spaceBetween: 10,
            freeMode: true,

            loop: true,
            autoplay: true,
            speed: 1000,
            slidesPerView: 3,
            navigation: {
                nextEl: '.nextCar',
                prevEl: '.prevCar',
            },
            breakpoints: {

                1367: {
                    slidesPerView: 2,
                    freeMode: false,
                    spaceBetween: 10
                },
                320: {
                    slidesPerView: 1,
                    freeMode: false,
                    spaceBetween: 10
                },

                480: {
                    slidesPerView: 1,
                    spaceBetween: 10
                },

                640: {
                    slidesPerView: 2,
                    spaceBetween: 20
                }
            }
        });
    });

    function RequestaCallBank(hdnBankID) {
        $("#hdnBankID").val(hdnBankID);
        $("#ModalRequestaCallBank").modal('show');
    }

    function btnVBASKSubmitForm() {

        var txtPSCarID = $("#txtPSCarID").val();
        var txtPSUserName = $("#txtVBASKUserName").val();
        var txtPSEmail = $("#txtVBASKUserEmail").val();
        var ddlPSCountryCode = $("#VBASKPhoneCodeFIeld").find(".selected-dial-code").text();
        var txtPSPhone = $("#txtVBASKUserPhone").val();
        var bankID = $("#hdnBankID").val();

        if (txtPSUserName == "" || txtPSEmail == "" || txtPSPhone == "")
        {
            //$("#spanPSmessage").show();
            //$("#spanPSmessage").text("Kindly fill all the required fields")
            notify("Kindly fill all the required fields", "danger");
        }
        else
        {
            //$("#spanPSmessage").hide();
            //$("#spanPSmessage").text("");

            var data = {};
            data.car_id = txtPSCarID;
            data.bank_id = bankID;
            data.name = txtPSUserName;
            data.email = txtPSEmail;
            data.country_code = ddlPSCountryCode;
            data.phone = txtPSPhone;
            data.type = 30;

            $.ajax({
                type: 'GET',
                url: '@Url.Action("PersonalShopperRequest", "Car")',
                data: data,
                success: function (data) {
                    if (data == "Contact Us saved successfully") {
                        notify("Request has been submitted", "success");
                        $("#btncloseBankRequestModal").click();
                        //$("#spanPSmessage").show();
                        //$("#spanPSmessage").text("Request has been submitted")
                    }
                    else {
                        notify("Something went wrong while submitting request, Please try again", "danger");
                        //$("#spanPSmessage").show();
                        //$("#spanPSmessage").text("Something went wrong while submitting request, Please try again");
                    }
                },
            });
        }
    }

    function ConsultancyToggle($this, type) {

        $(".btns_parent").find("a.custBtn").each(function () {
            $(this).removeClass("current")
        })

        $($this).addClass("current");
        $("#hdnConsultancyType").val(type);

        if (type == 10) {
            $("#descAskforConsultancy").show();
            $("#descPersonalShopper").hide();
        }
        else {
            $("#descAskforConsultancy").hide();
            $("#descPersonalShopper").show();
        }

    }

$(document).ready(function () {

    GetCustomerMyCars();

    setTimeout(function () {
        if ('@ViewData["UserCountryCode"]' != null) {
            if ('@ViewData["UserCountryCode"].ToString()' != "") {
                $(".country-list").find(".country").each(function () {
                    if ($(this).attr("data-dial-code") == '@ViewData["UserCountryCode"].ToString()') {
                        var countrycode = $(this).attr("data-country-code")
                        $(".pncode").intlTelInput("setCountry", countrycode);
                        return;
                    }
                })
            }
        }
    }, 5000);
});

    $(function () {
        CalculateFinanceBuy();
        CalculateCashBuy();

        $(".moth_slider").ionRangeSlider({
            min: 0,
            max: 60,
            from: 0,
            postfix: " Months",
            onChange: function (data) {
                $("#VBPeriod-hidden").text(data.from);
                CalculateFinanceBuy();
            }
        });

        $(".down_payment").ionRangeSlider({
            min: 0,
            max: 75,
            from: 0,
            postfix: " %",
            onChange: function (data) {
                $("#VBDownPayment-hidden").text(data.from);
                CalculateFinanceBuy();
            }
        });

        $(".interest_rate").ionRangeSlider({
            min: 0,
            max: 10,
            from: 0,
            step: .01,
            postfix: " %",
            onChange: function (data) {
                $("#VBInterestRate-hidden").text(data.from);
                CalculateFinanceBuy();
            }
        });

        $(".insurance_rate2").ionRangeSlider({
            min: 0,
            max: 10,
            from: 0,
            step: .01,
            postfix: " %",
            onChange: function (data) {
                $("#VBInsuranceRate-hidden").text(data.from);
                CalculateFinanceBuy();
                CalculateCashBuy();
            }
        });


        $(".insurance_rate").ionRangeSlider({
            min: 0,
            max: 10,
            from: 0,
            step: .01,
            postfix: " %",
            onChange: function (data) {
                $("#VBInsuranceRate-hidden").text(data.from);
                CalculateFinanceBuy();
                CalculateCashBuy();
            }
        });

    });

    function ShowDealerPhoneNumber(phoneno) {
        $(".DealerPhoneNo").text(phoneno);
        $('#ShowPhoneNumber').modal('show');
    }

    function CalculateCashBuy() {
        var CarPrice = parseFloat($("#hdnCarAmount").val());
        var InsuranceRate = parseInt($("#VBInsuranceRate-hidden").text());

        if (InsuranceRate > 0) {
            InsuranceRate = parseFloat(parseFloat(InsuranceRate / 100).toFixed(2) * parseFloat(CarPrice).toFixed(2)).toFixed(2);
            $("#tdCBInsurance").text("AED  " + formatcurrency(Math.round(parseFloat(InsuranceRate).toFixed(2))) );
            calculateUpfrontPayment();
        }
        else {
            $("#tdCBInsurance").text("AED  " + formatcurrency(Math.round(parseFloat(InsuranceRate).toFixed(2))));
        }

        var TotalUpfrontPayment = parseFloat(CarPrice) + parseFloat(InsuranceRate) + 550
        $("#tdCBTotalUpfrontPayment").text("AED  " + formatcurrency(Math.round(parseFloat(TotalUpfrontPayment).toFixed(2))));
    }

    function CalculateFinanceBuy() {        
        var CarPrice = parseFloat($("#hdnCarAmount").val());
        var period = parseInt($("#VBPeriod-hidden").text());
        var DownPayment = parseInt($("#VBDownPayment-hidden").text());
        var InterestRate = parseInt($("#VBInterestRate-hidden").text());
        var InsuranceRate = parseInt($("#VBInsuranceRate-hidden").text());

        if (DownPayment > 0) {
            DownPayment = parseFloat(parseFloat(DownPayment / 100).toFixed(2) * parseFloat(CarPrice).toFixed(2)).toFixed(2);
            $(".tdDownPayment").text("AED  " + formatcurrency(Math.round(parseFloat(DownPayment).toFixed(2))));
        }
        else {
            $(".tdDownPayment").text("AED  " + formatcurrency(Math.round(parseFloat(DownPayment).toFixed(2))));
        }

        if (InsuranceRate > 0) {
            InsuranceRate = parseFloat(parseFloat(InsuranceRate / 100).toFixed(2) * parseFloat(CarPrice).toFixed(2)).toFixed(2);
            $("#tdInsurance").text("AED  " + formatcurrency(Math.round(parseFloat(InsuranceRate).toFixed(2))));
            calculateUpfrontPayment();
        }
        else {
            $("#tdInsurance").text("AED  " + formatcurrency(Math.round(parseFloat(InsuranceRate).toFixed(2))));
        }

        calculateMonthlyInstallment();
        calculateUpfrontPayment();
    }

    function calculateMonthlyInstallment()
    {
        var CarPrice = parseFloat($("#hdnCarAmount").val());
        var period = parseInt($("#VBPeriod-hidden").text());
        var DownPayment = parseFloat($(".tdDownPayment:first").text().replace("AED  ", ""));
        var InterestRate = parseInt($("#VBInterestRate-hidden").text());

        DownPayment = RemoveallComma(DownPayment);

        var AnnualInterest = parseFloat(CarPrice) - parseFloat(DownPayment)
        InterestRate = parseFloat(InterestRate / 100)
        AnnualInterest = parseFloat(AnnualInterest * InterestRate)

        var MonthlyInstallment = AnnualInterest / 12;
        var totalInterestPayable = MonthlyInstallment * period

        if (DownPayment > 0 && period > 0) {
            MonthlyInstallment = (DownPayment + totalInterestPayable) / period;
        }

        $("#tdMonthlyInstallment").text("AED  " + formatcurrency(Math.round(parseFloat(MonthlyInstallment).toFixed(2))));
    }

    function calculateUpfrontPayment()
    {
        var DownPayment = parseFloat($(".tdDownPayment:first").text().replace("AED  ", ""));
        var InsuranceRate = parseFloat($("#tdInsurance").text().replace("AED  ", ""));

        DownPayment = RemoveallComma(DownPayment);
        InsuranceRate = RemoveallComma(InsuranceRate);

        var TotalUpfrontPayment = parseFloat(DownPayment) + parseFloat(InsuranceRate) + 550
        $("#tdTotalUpfrontPayment").text("AED  " + formatcurrency(Math.round(parseFloat(TotalUpfrontPayment).toFixed(2))));
    }

    function MarkFavourite(id, type, $this) {

        var text = $($this).text();

        var data = {};
        data.car_id = id;
        data.type = type;

        $.ajax({
            type: 'GET',
            url: '@Url.Action("MarkFavouriteCar", "Car")',
            data: data,
            success: function (data) {
                if (data == "success") {
                    if (text == "Favorite") {
                        notify("Marked favourite Successfully", "success");
                        $($this).find("p").text("UnFavorite")
                    }
                    else {
                        notify("Marked Unfavourite Successfully", "success");
                        $($this).find("p").text("Favorite")
                    }
                    //$("#spanPSmessage").show();
                    //$("#spanPSmessage").text("Request has been submitted")
                }
                else {
                    notify("Something went wrong while submitting request, Please try again", "danger");
                    //$("#spanPSmessage").show();
                    //$("#spanPSmessage").text("Something went wrong while submitting request, Please try again");
                }
            },
        });
    }


    function btnPSSubmitForm() {

        var txtPSCarID = $("#txtPSCarID").val();
        var txtPSUserName = $("#txtPSUserName").val();
        var txtPSEmail = $("#txtPSEmail").val();
        var ddlPSCountryCode = $("#PSPhoneCodeFIeld").find(".selected-dial-code").text();
        var txtPSPhone = $("#txtPSPhone").val();

        if (txtPSUserName == "" || txtPSEmail == "" || txtPSPhone == "") {
            //$("#spanPSmessage").show();
            //$("#spanPSmessage").text("Kindly fill all the required fields")
            notify("Kindly fill all fields", "danger");
        }
        else {
            //$("#spanPSmessage").hide();
            //$("#spanPSmessage").text("");
            debugger;
            var data = {};
            data.car_id = txtPSCarID;
            data.bank_id = 0;
            data.name = txtPSUserName;
            data.email = txtPSEmail;
            data.country_code = ddlPSCountryCode;
            data.phone = txtPSPhone;
            data.type = 20;

            $.ajax({
                type: 'GET',
                url: '@Url.Action("PersonalShopperRequest", "Car")',
                data: data,
                success: function (data) {
                    if (data == "Contact Us saved successfully") {
                        notify("Request has been submitted", "success");
                        //$("#spanPSmessage").show();
                        //$("#spanPSmessage").text("Request has been submitted")
                    }
                    else {
                        notify("Something went wrong while submitting request, Please try again", "danger");
                        //$("#spanPSmessage").show();
                        //$("#spanPSmessage").text("Something went wrong while submitting request, Please try again");
                    }
                },
            });
        }
    }


function TradeinYourCar() {
    $("#TRadeinClosebtn").trigger("click");
}


function TradeInthisCar($this, NewCarID) {

    var carname = $("#hdnCarName").val();

    $(".TradeInMessage").text("Kindly confirm do you want to trade in " + carname);    
    $("#hdnNewCarID").val(NewCarID);   
    $('#RequestTradeInModal').modal('show');
    
}

function GetCustomerMyCars() {    
    $('.loading').fadeIn();
    $('#carDetailsPage').html('');
    $.ajax({
        dataType: "html",
        async: true,
        type: 'GET',
        url: '@Url.Action("CustCars", "Car")',
        type: 'GET',
        success: function (data) {
            $('#carDetailsPage').html(data);
        }, complete: function (data) {
            $('.loading').fadeOut('');
        }
    });
}