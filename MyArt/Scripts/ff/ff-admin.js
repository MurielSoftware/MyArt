$.navigation = $("nav > ul.nav");

$(document).ready(function () {
    $.navigation.on("click", "a", function () {
        if ($(this).hasClass("nav-dropdown-toggle")) {
            $(this).toggleClass("open");
        }
    });

    $(".remotecontent").remoteContent();
    initPlugins();
});

function initPlugins() {
    $(".form-control-editable").inlineEditable();
    $(".remotecontent").remoteContent();
    $(".upload").imageUpload();
    $(".autocomplete").autocomplete();
    $(".referencelist").referenceList();
    initPluginsOnRemoteContent();
}

function initPluginsOnRemoteContent() {

    
    $(".crop-image").imageCrop();

    //$("#Email").removeData("validator");
    //$("#Email").removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse("asynchronous-form");
    $.validator.unobtrusive.parse(document);
    //$.validator.unobtrusive.parse("#Email");
}

