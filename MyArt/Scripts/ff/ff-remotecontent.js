(function ($) {
    $.fn.remoteContent = function () {
        return this.each(function (index, remoteContent) {
            _appendLoading(remoteContent);
            $(remoteContent).load($(remoteContent).data("action"), { page: 1 }, function (result) {
                _onSuccess(remoteContent, result);
            });
        });

        function _appendLoading(remoteContent) {
            $(remoteContent).empty().append("<div id='loading'><i class='fa fa-circle-o-notch fa-spin'></i></div>");
        }

        function _onSuccess(remoteContent, result) {
            $(remoteContent).html(result);
            initPluginsOnRemoteContent();
        }
    };

    $.fn.remoteTab = function () {
        return this.each(function (index, remoteTab) {
            $(remoteTab).parents(".panel").find(".tab-pane.active:not(#form)").empty();
            //_appendLoading($(remoteTab).attr("href"));
            $(remoteTab.hash).load($(remoteTab).data("action"), function (result) {

            });
        });
    };
}(jQuery));