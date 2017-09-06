(function ($) {
    $.ajaxSetup({
        cache: false
    });

    $("body").on("click", ".show-dialog", function () {
        var initStep = $(this).data("init-step");
        $(this).dialog({ init_step: initStep });
    });

    $.fn.dialog = function (customOptions) {

        var defaultOptions = {
            init_step: 0,
        };

        var options = defaultOptions;
        _setOptions(customOptions);

        function _setOptions(customOptions) {
            options = $.extend(options, customOptions);
        }

        return this.each(function (index, dialog) {
            _load(dialog);
        });

        function _load(dialog) {
            $("#modal-dialogs").load($(dialog).attr("href"), function (result) {
                var submitButton = $("#modal-dialogs").find(".btn-success");
                var closeButton = $("#modal-dialogs").find(".btn-dialog-close");
                var previousButton = $("#modal-dialogs").find(".btn-dialog-previous");
                var removeFileButton = $("#modal-dialogs").find(".btn-modal-remove-attachement");
                var visibilityElementChangeElement = $("#modal-dialogs").find(".visibility-element-change");
                var form = $("#modal-dialogs").find(".asynchronous-form");
            
                closeButton.click(_close);
                previousButton.click(_previous);
                removeFileButton.click(_removeFile);
                visibilityElementChangeElement.change(_changeElement);
                form.submit(_submit);

                $(".wizard-step:not([data-wizard-step='" + options.init_step + "'])").hide();
                $(form).data("wizard-current-step", options.init_step);
                _setSubmitLabel(options.init_step);

                initPlugins();
            });
        }

        function _close(e) {
            var closeButton = e.currentTarget;
            var afterCloseAction = $(closeButton).data("action");
            if (afterCloseAction) {
                $.post(afterCloseAction, null, function (data) {
                    _onFinish(null, data);
                });
            }
            $("#modal-dialogs").empty();
        }

        function _previous(e) {
            var form = $("#modal-dialogs").find(".asynchronous-form");
            var currentStepIndex = form.data("wizard-current-step");
            if (currentStepIndex === 0) {
                return;
            }
            currentStepIndex--;
            $(".wizard-step:visible").hide();
            $(".wizard-step[data-wizard-step='" + currentStepIndex + "']").show();
            $(form).data("wizard-current-step", currentStepIndex);
        }

        function _removeFile() {
            _onRemoveFile($(this));
        }

        function _changeElement(e) {
            var element = e.currentTarget;
            $.post($(element).data("action"), { currentValue: $(element).val() }, function (result) {
                var targetControl = $(element).data("target-control");
                $(targetControl).hide();
                if (result !== null) {
                    $(result).show();
                }
            });
        }

        function _submit(e) {
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
            if (data.TargetElementId !== null) {
                $("#" + data.TargetElementId).html("<div id='loading'><i class='fa fa-circle-o-notch fa-spin'></i></div>");
                $.post(data.AfterAction, function (result) {
                    $("#" + data.TargetElementId).html(result);
                });
            }
            $("#modal-dialogs").find("#Id").val(data.Id);
            $("#modal-dialogs").find("#CreatedDate").val(data.CreatedDate);
            $(".wizard-step:visible").hide();
            $(".wizard-step[data-wizard-step='" + data.NextStepIndex + "']").show();
            $(form).data("wizard-current-step", data.NextStepIndex);
            _setSubmitLabel(data.NextStepIndex);       
        }

        function _setSubmitLabel(stepIndex) {
            var wizardStepsCount = $(".wizard-step").length;
            if (stepIndex < wizardStepsCount-1) {
                $("#modal-dialogs").find(".btn-success").html("Next <i class='fa fa-angle-right'></i>");
            } else {
                $("#modal-dialogs").find(".btn-success").html("<i class='fa fa-save'></i> Finish");
            }  
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
            $.expandTree(data.ParentId, true);
        }

        function _refreshRichTextBox(data) {
            $("#modal-dialogs").empty();
            $.richTextBoxSetImage(data.ThumbnailPath, data.Path);
        }
    };
}(jQuery));