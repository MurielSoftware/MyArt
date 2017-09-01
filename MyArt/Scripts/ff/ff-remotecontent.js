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
        //.remoteTab();
        //$(".remoteTabs .remoteTab").click(function () { $(this).remoteTab(); });
        return this.each(function (index, remoteTab) {
            var activeTab = $(this).find(".active .remoteTab");
            if (activeTab) {
                _loadTabContent(activeTab);
            }
            $(this).find(".remoteTab").each(function (index, tab) {
                $(tab).click(function () {
                    _loadTabContent(tab);
                })
            });
            //$(remoteTab).parents(".panel").find(".tab-pane.active:not(#form)").empty();
            ////_appendLoading($(remoteTab).attr("href"));
            //$(remoteTab.hash).load($(remoteTab).data("action"), function (result) {

            //});
        });

        function _loadTabContent(tab) {
            $(tab).parents(".panel").find(".tab-pane.active:not(#form)").empty();
            //_appendLoading($(remoteTab).attr("href"));
            //var data = $(tab).data("toggle");
            //var href = $(tab).attr("href");
            //bar hash = $(tab).hash
            $($(tab).attr("href")).load($(tab).data("action"), function (result) {
            });
        }
    };
}(jQuery));