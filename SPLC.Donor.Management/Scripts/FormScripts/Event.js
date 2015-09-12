$(function () {
    // use jQuery date time picker

    if(getURLParameter("eid") > 0)
        $("#dvDetails").show();
    else
        $("#dvDetails").hide();

        function getURLParameter(name) {
            return decodeURIComponent(
                (location.search.match(RegExp("[?|&]" + name + '=(.+?)(&|$)')) || [, null])[1]
            );
        }

    $("[id$='txtNEventDate']").datepicker({
        showOn: "both",
        buttonImage: "images/cal.png",
        buttonImageOnly: true
    });

    $("[id$='txtEventDate']").datepicker({
        showOn: "both",
        buttonImage: "images/cal.png",
        buttonImageOnly: true
    });

    $("[id$='txtOnlineCloseDate']").datepicker({
        showOn: "both",
        buttonImage: "images/cal.png",
        buttonImageOnly: true
    });

    /*
    $("[id$='btnAddNew']").click(function () {
        //  Add new Event
 
        if ($("[id$='txtEventName']").val() != "" && $("[id$='txtEventDate']").val() != "") {
            
            var url = "jqws.aspx?fn=AddNewEvent&rn=" + randomNum() + "&p1=" + htmlEscape($("[id$='txtEventName']").val()) +
                "&p2=" + htmlEscape($("[id$='txtEventDate']").val());

            $("#hvTF").load(url, function (msg) {
                if (msg != "Error") {
                    //  If event added is successful
                    $("[id$='hfPK']").val(msg);
                    $("[id$='txtEventName']").prop('disabled', true);
                    $("[id$='txtEventDate']").datepicker('destroy');
                    $("[id$='btnAddNew']").hide();
                    $("#dvDetails").show();
                } else if (msg != "Duplicate") {
                    // If event is a Duplicate
                    $("#dialog-modal p").text("This Event already exists in the system.'");
                    $("#dialog-modal").dialog("open");
                }
                else {
                    // If event added is not successful
                    $("#dialog-modal p").text("An error occured.  Please contact the administrator to enter a new Event.'");
                    $("#dialog-modal").dialog("open");
                }
            });
        }
        else {
            $("#dialog-modal p").text("Please enter a valid name and date!'");
            $("#dialog-modal").dialog("open");
        }        
    });
    */

    $("[id$='txtEventName']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=EventName" +
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

    $("[id$='txtDisplayName']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=DisplayName" +
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


    $("[id$='txtSpeaker']").bind('keyup blur', function () {
        $(this).val($(this).val().replace(/[^a-z\A-Z\s]*$/g, ''));
    });


    $("[id$='txtSpeaker']").blur(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=Speaker" +
                "&p3=" + htmlEscape($(this).val());
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $("[id$='txtSpeaker']").addClass("selFalse");
                }
                else {
                    $("[id$='txtSpeaker']").addClass("selTrue");
                }
            });
        } else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtEventDate']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=EventDate" +
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

    $("[id$='txtOnlineCloseDate']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=OnlineCloseDate" +
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

    $("[id$='txtVName']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueName" +
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

    $("[id$='txtVAddress']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueAddress" +
                "&p3=" + htmlEscape($(this).val());
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $("[id$='txtVAddress']").addClass("selFalse");
                }
                else {
                    $("[id$='txtVAddress']").addClass("selTrue");
                }
            });

        } else {
            $(this).addClass("selFalse");
        }
    });

    //$("[id$='txtVCity']").bind('keyup blur', function () {
    //    alert('yy');
    //    $(this).val($(this).val().replace(/[^a-z\A-Z\s]*$/g, ''));
    //});

    $("[id$='txtVCity']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            $("[id$='hlTicketURL']").attr('href', "https://rsvp.splcenter.org/" + $(this).val());
            $("[id$='hlTicketURL']").text("https://rsvp.splcenter.org/" + $(this).val());

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueCity" +
                "&p3=" + htmlEscape($(this).val());
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $(this).addClass("selFalse");
                }
                else {
                    $(this).addClass("selTrue");

                    $("[id$='txtURL']").val("https://rsvp.splcenter.org/" + $("[id$='txtVCity']").val());
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

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueState" +
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

    $("[id$='txtVZipCode']").blur(function () {

        if ($(this).val().length > 10) {
            $(this).addClass("selFalse");
        }
        else {
            if ($(this).val() != "") {
                $(this).removeClass("selFalse");

                var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueZipCode" +
                    "&p3=" + htmlEscape($(this).val());
                $("#hvTF").load(url, function (msg) {
                    if (msg != "True") {
                        $("[id$='txtVZipCode']").addClass("selFalse");
                    }
                    else {
                        $("[id$='txtVZipCode']").addClass("selTrue");
                    }
                });

            } else {
                $(this).addClass("selFalse");
            }
        }
    });


    //$("[id$='txtVZipCode']").change(function () {

    //    if ($(this).val() != "") {
    //        $(this).removeClass("selFalse");

    //        var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=VenueZipCode" +
    //            "&p3=" + htmlEscape($(this).val());
    //        $("#hvTF").load(url, function (msg) {
    //            if (msg != "True") {
    //                $("[id$='txtVZipCode']").addClass("selFalse");
    //            }
    //            else {
    //                $("[id$='txtVZipCode']").addClass("selTrue");
    //            }
    //        });

    //    } else {
    //        $(this).addClass("selFalse");
    //    }
    //});

    $("[id$='txtVZipCode']").bind('keyup blur', function () {
        $(this).val($(this).val().replace(/[^0-9\-]*$/g, ''));
    });

    $("[id$='txtCapasity']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=Capacity" +
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

    /*
    $("[id$='txtImageURL']").change(function () {

        if ($(this).val() != "") {

            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=ImageURL" +
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
    */

    $("[id$='txtTicketsAllowed']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($(this).val() > 24) {
                $(this).addClass("selFalse");
            }
            else {
                
                var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=TicketsAllowed" +
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

            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });


    function ValDateSEQ_StartEnd() {

        if ($("[id$='txtSTimeHour']").val() != "00" && $("[id$='txtETimeHour']").val() != "00") {

            var SDate = new Date($("[id$='txtEventDate']").val() + " " + $("[id$='txtSTimeHour']").val() + ":" + $("[id$='txtSTimeMin']").val() + " " +
                        $("[id$='ddlStartTime']").val());

            var EDate = new Date($("[id$='txtEventDate']").val() + " " + $("[id$='txtETimeHour']").val() + ":" + $("[id$='txtETimeMin']").val() + " " +
                        $("[id$='ddlEndTime']").val());

            if (SDate > EDate) {
                return true;
            }
        }
        
        return false;
    }

    function ValDateSEQ_DoorsStart() {

        if ($("[id$='txtSTimeHour']").val() != "00" && $("[id$='txtOOTimeHour']").val() != "00") {

            var SDate = new Date($("[id$='txtEventDate']").val() + " " + $("[id$='txtSTimeHour']").val() + ":" + $("[id$='txtSTimeMin']").val() + " " +
                        $("[id$='ddlStartTime']").val());

            var ODate = new Date($("[id$='txtEventDate']").val() + " " + $("[id$='txtOOTimeHour']").val() + ":" + $("[id$='txtOOTimeMin']").val() + " " +
                        $("[id$='ddlOOTime']").val());

            if (ODate > SDate) {
                return true;
            }
        }

        return false;
    }




    $("[id$='txtSTimeHour']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($(this).val() > 12) {
                $(this).addClass("selFalse");
            }
            else
            {
                if (ValDateSEQ_StartEnd()) {
                    $(this).addClass("selFalse");
                }
                else {
                    var intVal = parseInt($("[id$='txtSTimeHour']").val(), 10);

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + intVal + ":" + $("[id$='txtSTimeMin']").val() + " " +
                        $("[id$='ddlStartTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=StartDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtSTimeHour']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtSTimeHour']").val(intVal);
                            $("[id$='txtSTimeHour']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtSTimeMin']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($(this).val() > 60) {
                $(this).addClass("selFalse");
            }
            else {

                if (ValDateSEQ_StartEnd()) {
                    $(this).addClass("selFalse");
                }
                else {
                    var intVal = parseInt($("[id$='txtSTimeMin']").val(), 10);

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtSTimeHour']").val() + ":" + intVal + " " +
                        $("[id$='ddlStartTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=StartDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtSTimeMin']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtSTimeMin']").val(intVal);
                            $("[id$='txtSTimeMin']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='ddlStartTime']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            if (ValDateSEQ_StartEnd()) {
                $(this).addClass("selFalse");
            }
            else {

                var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtSTimeHour']").val() + ":" + $("[id$='txtSTimeMin']").val() + " " +
                        $(this).val();
                var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=StartDate" +
                    "&p3=" + htmlEscape(vDateTime);
                $("#hvTF").load(url, function (msg) {
                    if (msg != "True") {
                        $("[id$='ddlStartTime']").addClass("selFalse");
                    }
                    else {
                        $("[id$='ddlStartTime']").addClass("selTrue");
                    }
                });
            }

        } else {
            $(this).addClass("selFalse");
        }
    });


    $("[id$='txtETimeHour']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if (ValDateSEQ_StartEnd()) {
                $(this).addClass("selFalse");
            }
            else {
                if ($(this).val() > 12) {
                    $(this).addClass("selFalse");
                }
                else {

                    var intVal = parseInt($("[id$='txtETimeHour']").val(), 10);

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + intVal + ":" + $("[id$='txtETimeMin']").val() + " " +
                        $("[id$='ddlStartTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=EndDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtETimeHour']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtETimeHour']").val(intVal);
                            $("[id$='txtETimeHour']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtETimeMin']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if (ValDateSEQ_StartEnd()) {
                $(this).addClass("selFalse");
            }
            else {
                if ($(this).val() > 60) {
                    $(this).addClass("selFalse");
                }
                else {

                    var intVal = parseInt($("[id$='txtETimeMin']").val(), 10);

                    if (intVal < 10)
                        intVal = "0" + intVal;

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtETimeHour']").val() + ":" + intVal + " " +
                        $("[id$='ddlEndTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=EndDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtETimeMin']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtETimeMin']").val(intVal);
                            $("[id$='txtETimeMin']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });


    $("[id$='ddlEndTime']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtETimeHour']").val() + ":" + $("[id$='txtETimeMin']").val() + " " +
                    $(this).val();
            var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=EndDate" +
                "&p3=" + htmlEscape(vDateTime);
            $("#hvTF").load(url, function (msg) {
                if (msg != "True") {
                    $("[id$='ddlEndTime']").addClass("selFalse");
                }
                else {
                    $("[id$='ddlEndTime']").addClass("selTrue");
                }
            });

        } else {
            $(this).addClass("selFalse");
        }
    });


    $("[id$='txtOOTimeHour']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($(this).val() > 12) {
                $(this).addClass("selFalse");
            }
            else {

                if (ValDateSEQ_DoorsStart()) {
                    $(this).addClass("selFalse");
                }
                else {
                    var intVal = parseInt($("[id$='txtOOTimeHour']").val(), 10);

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + intVal + ":" + $("[id$='txtOOTimeMin']").val() + " " +
                        $("[id$='ddlOOTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=DoorsOpenDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtOOTimeHour']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtOOTimeHour']").val(intVal);
                            $("[id$='txtOOTimeHour']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });

    $("[id$='txtOOTimeMin']").change(function () {

        if ($.isNumeric($(this).val()) || $(this).val() == '') {
            $(this).removeClass("selFalse");

            if ($(this).val() > 60) {
                $(this).addClass("selFalse");
            }
            else {

                if (ValDateSEQ_DoorsStart()) {
                    $(this).addClass("selFalse");
                }
                else {
                    var intVal = parseInt($("[id$='txtOOTimeMin']").val(), 10);

                    // Update Start Time
                    var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtOOTimeHour']").val() + ":" + intVal + " " +
                        $("[id$='ddlOOTime']").val();

                    var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=DoorsOpenDate" +
                    "&p3=" + htmlEscape(vDateTime);
                    $("#hvTF").load(url, function (msg) {
                        if (msg != "True") {
                            $("[id$='txtOOTimeMin']").addClass("selFalse");
                        }
                        else {
                            $("[id$='txtOOTimeMin']").val(intVal);
                            $("[id$='txtOOTimeMin']").addClass("selTrue");
                        }
                    });
                }
            }
        }
        else {
            $(this).addClass("selFalse");
        }
    });


    $("[id$='ddlOOTime']").change(function () {

        if ($(this).val() != "") {
            $(this).removeClass("selFalse");

            if (ValDateSEQ_DoorsStart()) {
                $(this).addClass("selFalse");
            }
            else {

                var vDateTime = $("[id$='txtEventDate']").val() + " " + $("[id$='txtOOTimeHour']").val() + ":" + $("[id$='txtOOTimeMin']").val() + " " +
                        $(this).val();
                var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=DoorsOpenDate" +
                    "&p3=" + htmlEscape(vDateTime);
                $("#hvTF").load(url, function (msg) {
                    if (msg != "True") {
                        $("[id$='ddlOOTime']").addClass("selFalse");
                    }
                    else {
                        $("[id$='ddlOOTime']").addClass("selTrue");
                    }
                });
            }

        } else {
            $(this).addClass("selFalse");
        }
    });



    $("[id$='cbActive']").change(function () {

        var url = "jqws.aspx?fn=UpdateEvent&rn=" + randomNum() + "&p1=" + $("[id$='hfPK']").val() + "&p2=Active" +
                "&p3=" + $(this).is(':checked');

        $("#hvTF").load(url, function (msg) {
            if (msg != "True") {
                $("[id$='cbActive']").addClass("selFalse");
            }
            else {
                $("[id$='cbActive']").addClass("selTrue");
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
            //.replace(/'/g, '&#39;')
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
})
