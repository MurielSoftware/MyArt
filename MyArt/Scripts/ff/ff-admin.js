$.navigation = $("nav > ul.nav");

$(document).ready(function () {
    $(".notification").fadeIn(1000);
    $(".notification").fadeOut(5000);

    $.navigation.on("click", "a", function () {
        if ($(this).hasClass("nav-dropdown-toggle")) {
            $(this).toggleClass("open");
        }
    });

    $(".nav-vertical .nav > li").on("click", function (e) {
        localStorage.setItem("lastTab", $(e.target).attr("id"))
    });

    var lastTab = localStorage.getItem("lastTab");
    if (lastTab) {
        var active = $("#" + lastTab);
        $(active).addClass("active");
        $(active).parents("ul .collapse").each(function (index, collapseUl) {
            $(collapseUl).addClass("in");
        });
     //   localStorage.removeItem("lastTab");
    }

    initPlugins();
});

function initPlugins() {
    $(".form-control-editable").inlineEditable();
    $(".remotecontent").remoteContent();
    $(".upload").imageUpload();
    $(".autocomplete").autocomplete();
    $(".referencelist").referenceList();
    $(".remoteTabs").remoteTab();
    $(".richtextbox").richTextBox();
    initPluginsOnRemoteContent();
}

function initPluginsOnRemoteContent() {    
    $(".crop-image").imageCrop();
    $.validator.unobtrusive.parse(document);
}

