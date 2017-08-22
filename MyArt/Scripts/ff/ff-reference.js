(function ($) {
    $.fn.autocomplete = function (options) {
        return this.each(function () {
            var options = $.extend({}, $.fn.autocomplete.defaults, options);

            var autocomplete = $(this).find(".input-autocomplete-frame");
            var input = $(this).find(".input-autocomplete");
            var hidden = $(this).find(".input-autocomplete-hidden");
            var list = $(this).find("ul");

            var suggestions = {};
            var selectedIndex = -1;
            var selectedLiTag = "";
            var selectedObjects = {};
            var currentValue = "";
            var isDataLoadingExecuted = false;

            options.action = $(this).data("action");

            autocomplete.ready(function () {
                var references = hidden.val().split(";");
                $.each(references, function (index, reference) {
                    if (reference !== "") {
                        var referenceInfo = reference.split(":");
                        selectedObjects[referenceInfo[0]] = referenceInfo[1];
                        attachToContainer(input, referenceInfo[0], referenceInfo[1]);
                    }
                });
            });

            input.bind('keypress keydown keyup', function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    return false;
                }
            });

            input.keyup(function (e) {
                currentValue = $(this).val();
                if (list.find("li").length === 0) {
                    list.append("<li>Načítám...</li>");
                }
                list.css("width", autocomplete.width() + 12);
                list.css("display", "block");

                switch (e.which) {
                    case 40:
                        return arrowKeyDown(e);
                    case 38:
                        return arrowKeyUp(e);
                    case 13:
                        return arrowKeyEnter(e);
                    case 8:
                        return keyBackscape(e);
                    case 27:
                        return keyEscape(e);
                }

                if (!isCurrentValueEmpty()) {
                    loadData();
                }
            });

            function arrowKeyDown(e) {
                if (!isSelected()) {
                    selectedLiTag = $(list.find("li")[0]);
                    selectedLiTag.addClass("select");
                } else {
                    var next = $(selectedLiTag.next("li"));
                    if (next.length !== 0) {
                        selectedLiTag.removeClass("select");
                        next.addClass("select");
                        selectedLiTag = next;
                        scrollToAnchor(selectedLiTag);
                    }
                }
                selectedIndex++;
            }

            function arrowKeyUp(e) {
                var prev = $(selectedLiTag.prev("li"));
                if (prev.length !== 0) {
                    selectedLiTag.removeClass("select");
                    prev.addClass("select");
                    selectedLiTag = prev;
                    selectedIndex--;
                    scrollToAnchor(selectedLiTag);
                }
            }

            function arrowKeyEnter(e) {
                if (isSelected()) {
                    select(selectedIndex);
                }
            }

            function keyBackscape(e) {
                if (isCurrentValueEmpty()) {
                    var prev = input.prev(".autocomplete-item");
                    delete selectedObjects[prev.val()];
                    prev.remove();
                    updateIds(hidden, selectedObjects);
                    list.empty();
                    list.css("display", "none");
                    return;
                }
            }

            function keyEscape(e) {
                list.empty();
                list.css("display", "none");
            }

            function isCurrentValueEmpty() {
                return currentValue === "";
            }

            function isSelected() {
                return selectedIndex !== -1;
            }

            function select(index) {
                attachToContainer(input, suggestions[index].id, suggestions[index].label);
                selectedObjects[suggestions[index].id] = suggestions[index].label;
                updateIds(hidden, selectedObjects);
                list.empty();
                input.val("");
                input.focus();
                selectedLiTag = "";
                selectedIndex = -1;
                list.css("display", "none");
            }

            function attachToContainer(input, id, label) {
                $("<div></div>")
                    .addClass("autocomplete-item")
                    .val(id)
                    .append(label)
                    .append(
                    $("<i></i>")
                        .addClass("autocomplete-remove fa fa-remove")
                        .click(function () {
                            var item = $(this).parent();
                            delete selectedObjects[id];
                            item.remove();
                            updateIds(hidden, selectedObjects);
                        })
                    )
                    .insertBefore(input);
            }

            function updateIds(hidden, items) {
                var ids = "";
                for (id in items) {
                    ids = ids + id + ":" + items[id] + ";";
                }
                hidden.val(ids);
            }

            function scrollToAnchor(element) {
                list.animate({ scrollTop: element.position().top }, "fast");
            }

            function loadData() {
                if (isDataLoadingExecuted) {
                    return;
                }
                isDataLoadingExecuted = true;
                setTimeout(function () {
                    $.post(options.action, { prefix: currentValue }, function (data) {
                        suggestions = $.map(data, function (item) {
                            return {
                                id: item.Id,
                                label: item.Label
                            };
                        });
                        list.empty();
                        if (suggestions.length === 0) {
                            $("<li></li>")
                                .append("Nic nebylo nalezeno")
                                .appendTo(list);
                        } else {
                            $.each(suggestions, function (index, item) {
                                $("<li></li>")
                                    .append(item.label)
                                    .click(function () {
                                        select(index);
                                    })
                                    .appendTo(list);
                            });
                        }
                        isDataLoadingExecuted = false;
                    });
                }, 500);
            }
        });
    };

    $.fn.autocomplete.defaults = {
        action: ""
    };
}(jQuery));

