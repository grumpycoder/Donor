$(function () {


    $("[id$='txtName']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=AccountName" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtAddress']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=AddressLine1" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtCity']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=City" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='ddlState']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=State" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();

            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtZipCode']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=PostCode" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtEmail']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=Email" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtPhone']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorList&rn=" + randomNum() + "&p1=" + $("[id$='hfDPK']").val() + "&p2=PhoneNumber" +
                "&p3=" + htmlEscape($(this).val()) + "&p4=" + $("[id$='hfPK']").val();
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtSPLCComments']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateDonorEventList&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=SPLCComments" +
                "&p3=" + htmlEscape($(this).val());
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtAttending']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($("[id$='hfTicketsAllowed']").val() < $(this).val())
                alert('The ticket limit for this event is ' + $("[id$='hfTicketsAllowed']").val() + " and you entered " + $(this).val() + ".");


            var url = "jqws.aspx?fn=UpdateDonorEventList&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=TicketsRequested" +
                "&p3=" + htmlEscape($(this).val());
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");
                }
            });
            $(this).addClass("selTrue");
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='chkAttending']").change(function () {

        var url = "jqws.aspx?fn=UpdateDonorEventList&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=Attending" +
                "&p3=" + $(this).is(':checked');

        $("#hvTF").load(url, function (msg) {
            if (msg != "True") {
                $("[id$='chkAttending']").addClass("selFalse");
            }
            else {
                $("[id$='chkAttending']").addClass("selTrue");
            }
        });

    });


    function randomNum() {
        return Math.floor(Math.random() * 2000);
    }

    function htmlEscape(str) {
        return String(str)
            .replace(/ /g, '%20')
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
    }

    $(function () {
        $("#dialog-modal").dialog({
            autoOpen: false,
            height: 200,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
});