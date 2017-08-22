(function ($) {
    $("body").on("click", ".expand-tree-button", function (e) {
        var expandButton = $(this);
        var remoteContent = _findNode(expandButton).children(".children-nodes");
        var remote = $(remoteContent).children().length === 0 ? true : false;
        $.expandTree(_findNodeId($(this)), remote);
    });

    $("body").on("click", ".tree-node-down", function (e) {
        var sourceNode = _findNode($(this));
        var targetNode = $(sourceNode).next();
        _treeNodeChangePosition(sourceNode, targetNode);
    });

    $("body").on("click", ".tree-node-up", function (e) {
        var sourceNode = _findNode($(this));
        var targetNode = $(sourceNode).prev();
        _treeNodeChangePosition(sourceNode, targetNode);
    });

    $.expandTree = function (parentId, remoteLoading) {
        var node = $(".tree-node[data-id=" + parentId + "]");
        var expandButton = $(node).find(".expand-tree-button");
        var remoteContent = node.children(".children-nodes");
        if (remoteLoading) {
            $.post("/Admin/MenuItem/ExpandTreeItem", { parentId: parentId }, function (result) {
                $(remoteContent).empty();
                if ($(result).has(".tree-node").length === 0) {
                    $(expandButton).remove();
                    return;
                } else if ($(expandButton).length === 0) {
                    $(node).find(".text").prepend("<i class='fa fa-minus-square-o expand-tree-button'></i>");
                }
                $(remoteContent).html(result);
                $(expandButton[0]).removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
            });
        } else {
            var childrenNodes = $(remoteContent).children("ul");
            if (childrenNodes.is(":visible")) {
                childrenNodes.hide('fast');
                $(expandButton[0]).removeClass("fa-minus-square-o").addClass("fa-plus-square-o");
            }
            else {
                childrenNodes.show('fast');
                $(expandButton[0]).removeClass("fa-plus-square-o").addClass("fa-minus-square-o");
            }
        }
    };

    _treeNodeChangePosition = function (sourceNode, targetNode) {
        $.post("/Admin/MenuItem/TreeNodeChangePosition", { sourceId: $(sourceNode).data("id"), targetId: $(targetNode).data("id") }, function (result) {
            var parentId = $(sourceNode).parents(".tree-node").data("id");
            if (parentId === null) {
                $.post(result.Action, function (partial) {
                    $("#" + result.TargetId).html(partial);
                });
            } else {
                $.expandTree(parentId, true);
            }
        });
    };

    _findNodeId = function (element) {
        return $(_findNode(element)).data("id");
    };

    _findNode = function (element) {
        return $(element).closest(".tree-node");
    };
}(jQuery));

(function ($) {
    $("body").on("mouseover", ".dropdown", function (e) {
        var expandMenuItem = $(this);
        var remoteTarget = $(expandMenuItem).children(".dropdown-menu");
        var remote = $(remoteTarget).children().length === 0 ? true : false;
        _loadDropDownMenu(expandMenuItem.data("id"), remoteTarget, remote);
    });

    _loadDropDownMenu = function (parentId, remoteTarget, remote) {
        if (!remote) {
            return;
        }
        $.post("/MenuItem/CollapseMenu", { parentId: parentId }, function (result) {
            remoteTarget.empty().append(result);
        });
    };
}(jQuery));