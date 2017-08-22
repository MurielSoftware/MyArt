(function ($) {
    $.fn.referenceList = function (options) {
        return this.each(function () {
            var thisReferenceList = $(this);
            var options = $.extend({}, $.fn.autocomplete.defaults, options);
            options.action = thisReferenceList.data("action");

            var targetControl = thisReferenceList.parent().prev(".form-control-editable");
            targetControl.text("Načítám...");
            thisReferenceList.empty().append("<option>Načítám...</option>");

            $.post(options.action, null, function (data) {
                var items = [];
                if (data.length === 0) {
                    items[0] = "<option>Nic nebylo nalezeno</option>";
                    _setSelected(targetControl, "Nic nebylo nalezeno", true);
                } else {
                    $.each(data, function (i, option) {
                        if (option.Key === thisReferenceList.data("selected")) {
                            items[i] = "<option value='" + option.Key + "' selected='selected'>" + option.Value + "</option>";
                            _setSelected(targetControl, option.Value, true);
                        } else {
                            items[i] = "<option value='" + option.Key + "'>" + option.Value + "</option>";
                            _setSelected(targetControl, option.Value, false);
                        }
                    });
                }
                thisReferenceList.empty().append(items.join(""));
            });

            function _setSelected(targetControl, label, set) {
                if (targetControl === null || targetControl.length === 0) {
                    return;
                }
                if (set) {
                    targetControl.text(label);
                } else if (targetControl.text() === "Načítám...") {
                    targetControl.text(label);
                }
            }
        });
    };
}(jQuery));