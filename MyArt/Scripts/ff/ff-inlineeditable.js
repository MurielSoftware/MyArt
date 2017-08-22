(function ($) {
    $.fn.inlineEditable = function () {
        var parent;
        var textBox;

        $(this).click(function () {
            _set($(this));
        });

        function _set(obj) {
            parent = obj;
            obj.css("display", "none");
            obj.next().css("display", "block");
            var textBox = obj.next().find("input");
            textBox.focus();

            $(textBox).keydown(function (e) {
                if (e.keyCode == 9) {
                    var fields = $(this).parents('form:eq(0),body').find('.form-control-editable');
                    var index = fields.index(parent);
                    if (index > -1 && (index + 1) < fields.length) {
                        _set(fields.eq(index + 1));
                    }
                    e.preventDefault();
                }
            });
        };

        $(".form-control-editable").mouseenter(function () {
            $(this).prev().slideToggle();
        });
        $(".form-control-editable").mouseleave(function () {
            $(this).prev().hide();
        });
    };

}(jQuery));

