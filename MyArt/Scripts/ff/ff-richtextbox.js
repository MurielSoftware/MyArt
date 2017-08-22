(function ($) {
    $.fn.richTextBox = function () {
        $(this).each(function () {
            var hiddenToSave = $(this).find("input[type=hidden]");
            var textArea = $(this).find(".richtextbox-textarea");
            textArea.html(hiddenToSave.val());

            $(this).find(".btn").click(function (e) {
                e.preventDefault();
                var command = $(this).data("command");
                var parameter = $(this).data("parameter");
                if (command === null) {
                    return;
                }
                switch (command) {
                    case "createLink":
                        var link = prompt("Insert the link");
                        if (link) {
                            document.execCommand(command, false, link);
                        }
                        break;
                    case "clear":
                        $(textArea).html("");
                        break;
                    default:
                        document.execCommand(command, false, parameter);
                        break;
                }
                _update();
                textArea.focus();
            });

            $(textArea).on("input change keyup propertychange", textArea, function () {
                _update();
            });

            $(textArea).on("paste", function (e) {
                var clipboardContent = "";
                e.preventDefault();
                if (e.originalEvent.clipboardData) {
                    clipboardContent = (e.originalEvent || e).clipboardData.getData("text/plain");
                }
                else if (window.clipboardData) {
                    clipboardContent = window.clipboardData.getData("Text");
                }

                if (document.queryCommandSupported("insertText")) {
                    document.execCommand("insertText", false, clipboardContent);
                } else {
                    document.execCommand("paste", false, clipboardContent);
                }
                _update();
            });

            $(textArea).on("DOMNodeRemoved", function (e) {
                if (e.target.nodeName === "IMG") {
                    $(e.target).parent("a").remove();
                    _update();
                }
            });

            $.richTextBoxSetImage = function (thumbnailSource, source) {
                textArea.append(_createHrefTag(thumbnailSource, source));
                _update();
                _save();
            };

            _update = function () {
                hiddenToSave.val($(textArea).html());
            };

            _save = function () {
                $.post($(this).data("save-action"), $("form").serialize());
            };

            _createHrefTag = function (thumbnailSource, source) {
                return $("<a></a>")
                    .attr("href", source)
                    .attr("data-lightbox", "article")
                    .append(_createImgTag(thumbnailSource));
            };

            _createImgTag = function (thumbnailSource) {
                return $("<img></img>")
                    .attr("src", thumbnailSource);
            };
        });
    };
}(jQuery));