(function ($) {
    $.ajaxSetup({
        cache: false
    });

    $("body").on("click", ".show-dialog", function () {
        $(this).dialog();
    });

    $.fn.dialog = function () {
        return this.each(function (index, dialog) {
            _load(dialog);
        });

        function _load(dialog) {
            $("#modal-dialogs").load($(dialog).attr("href"), function (result) {
                var closeButton = $("#modal-dialogs").find(".btn-dialog-close");
                var removeFileButton = $("#modal-dialogs").find(".btn-modal-remove-attachement");
                var submitButton = $("#modal-dialogs").find(".asynchronous-form");

                closeButton.click(_close);
                removeFileButton.click(_removeFile);
                submitButton.submit(_submit);

                $(".wizard-step:not(:first)").hide();

                initPlugins();
            });
        }

        function _close() {
            $("#modal-dialogs").empty();
        }

        function _removeFile() {
            _onRemoveFile($(this));
        }

        function _submit(e) {
            // var form = e.parent(".asynchronous-form"); //$(".asynchronous-form").get(0);
            var form = e.currentTarget;
            var formData = new FormData(form);
            formData.append("currentStep", $(form).data("wizard-current-step"));
            _appendFile(formData);

            var xhr = new XMLHttpRequest();
            xhr.open("post", form.action, true);
            xhr.upload.addEventListener('progress', function (event) {
                if (event.lengthComputable) {
                    $("#modal-dialogs").find('.progress-bar').width(event.loaded / event.total * 100 + "%");
                }
            }, false);
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    _onFinish(form, JSON.parse(xhr.response));
                }
            };
            xhr.send(formData);
            e.preventDefault();
        }


        function _appendFile(formData) {
            var fileInput = document.getElementById("file-input");
            if (fileInput !== null) {
                formData.append("file", fileInput.files[0]);
            }
        }

        function _onRemoveFile(removeButton) {
            $.post(removeButton.data("action"), { id: removeButton.data("id") }, function () {
                $(removeButton).parent().empty().append("<input type='file' name='file' id='file-input' />");
            });
        }

        function _onFinish(form, data) {
            if (data.Success) {
                _onSuccess(form, data);
            } else {
                _onFail(form, data);
            }
        }

        function _onSuccess(form, data) {
            switch (data.RefreshMode) {
                case 1:
                    _refreshWizard(form, data);
                    break;
                case 2:
                    _refreshPartialWithDialogClose(data);
                    break;
                case 3:
                    _refreshTree(data);
                    break;
                case 4:
                    _refreshRichTextBox(data);
                    break;
            }
        }

        function _onFail(form, data) {
            $("#" + data.TargetElementId).html(data.Message);
        }

        function _refreshWizard(form, data) {
            //if (data.TargetElementId !== null) {
            //    $("#" + data.TargetElementId).html("<div id='loading'><i class='fa fa-circle-o-notch fa-spin'></i></div>");
                //$.post(data.AfterAction, function (result) {
                 //   $("#" + data.TargetElementId).html(result);
            if (data.TargetElementId !== null) {
                $("#" + data.TargetElementId).html("<div id='loading'><i class='fa fa-circle-o-notch fa-spin'></i></div>");
                $.post(data.AfterAction, function (result) {
                    $("#" + data.TargetElementId).html(result);
                });
            }
            $("#modal-dialogs").find("#Id").val(data.Id);
            $("#modal-dialogs").find("#CreatedDate").val(data.CreatedDate);
            //$("#modal-dialogs").find("#UserDefinableOwnerId").val(data.Id);
            $(".wizard-step:visible").hide();
            $(".wizard-step[data-wizard-step='" + data.NextStepIndex + "']").show();
            $(form).data("wizard-current-step", data.NextStepIndex);
                    //initPluginsOnRemoteContent();
                //});
            //} else {
            //    window.location.href = data.Action;
            //}
        }

        function _refreshPartialWithDialogClose(data) {
            if (data.TargetElementId !== null) {
                $("#" + data.TargetElementId).html("<div id='loading'><i class='fa fa-circle-o-notch fa-spin'></i></div>");
                $.post(data.AfterAction, function (result) {
                    $("#modal-dialogs").empty();
                    $("#" + data.TargetElementId).html(result);
                });
            } else {
                window.location.href = data.AfterAction;
            }
        }

        function _refreshTree(data) {
            $("#modal-dialogs").empty();
            $.expandTree(data.TargetElementId, true);
        }

        function _refreshRichTextBox(data) {
            $("#modal-dialogs").empty();
            $.richTextBoxSetImage(data.ThumbnailPath, data.Path);
        }


    };
}(jQuery));