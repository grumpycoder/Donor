$(function () {

    alert('wade');

    $("[id$='txtZipCode']").bind('keyup blur', function () {
        alert('yy');
        //$(this).val($(this).val().replace(/[^0-9\-]*$/g, ''));
    });

})
