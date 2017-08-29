$.navigation = $("nav > ul.nav");

$(document).ready(function () {
    $.navigation.on("click", "a", function () {
        if ($(this).hasClass("nav-dropdown-toggle")) {
            $(this).toggleClass("open");
        }
    });

    //$(".remotecontent").remoteContent();
    initPlugins();
});

function initPlugins() {
    $(".form-control-editable").inlineEditable();
    $(".remotecontent").remoteContent();
    $(".upload").imageUpload();
    $(".autocomplete").autocomplete();
    $(".referencelist").referenceList();
    $(".remoteTabs .active .remoteTab").remoteTab();
    $(".remoteTabs .remoteTab").click(function () { $(this).remoteTab(); });
    $(".richtextbox").richTextBox();
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

