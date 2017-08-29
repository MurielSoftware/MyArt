(function ($) {
    $.fn.imageUpload = function () {
        return this.each(function () {
            var uploadedImages = $(this).find($(this).data("upload-target"));
            var uploadMode = $(this).data("upload-mode");
            var previewTemplate = "<div class='thumbnail'><img /></div>";
            var returnFileType = $(this).data("return-file-type");

            jQuery.event.addProp.drop = { props: ["dataTransfer"] };

            $(this).on("dragenter", ".upload-content-dropbox", function (e) {
                _defaults(e);
            });

            $(this).on("dragleave", ".upload-content-dropbox", function (e) {
                _defaults(e);
            });

            $(this).on("dragover", ".upload-content-dropbox", function (e) {
                _defaults(e);
            });

            $(this).on("drop", ".upload-content-dropbox", function (e) {
                _defaults(e);
                _handleImageFiles(e.originalEvent.dataTransfer.files, e);
            });

            $(this).find("input[type='file']").each(function (index, item) {
                $(item).change(function (e) {
                    _uploadBySelect(item, e);
                });
            });
            

            function _uploadBySelect(fileSelector, e) {
                _defaults(e);
                _handleImageFiles(fileSelector.files);
            }

            function _handleImageFiles(files, e) {
                for (var i = 0; i < files.length; i++) {
                    $("#dialog-validation-summary").empty();
                    var preview = _createThumbnailImage(files[i]);
                    _uploadImage(files[i], preview);
                }
            }

            function _createThumbnailImage(file) {
                if (!file) {
                    return;
                }
                var preview = $(previewTemplate);
                var previewImage = $("img", preview);
                previewImage.attr("src", "/Content/images/presentation/nophoto.png");
                preview.appendTo(uploadedImages);
                return preview;
            }

            function _uploadImage(file, preview) {
                var xhr = new XMLHttpRequest();
                xhr.open("post", "/Admin/Photo/Upload", true);

                //xhr.upload.addEventListener("progress", function (event) {
                //    if (event.lengthComputable) {
                //        var percentage = event.loaded / event.total * 100;
                //        $.data(file).find(".progress-bar").width(percentage + "%");
                //    }
                //}, false);

                xhr.onreadystatechange = function (event) {
                    if (xhr.readyState === 4 && xhr.status === 200) {
                        var jsonResult = jQuery.parseJSON(xhr.response);
                        if (jsonResult.Success) {
                            if (uploadMode === "append") {
                                var img = $(preview).find("img");
                                $(img).attr("src", jsonResult.RelativeFilePath);
                                $(img).data("id", jsonResult.Id);
                                $(img).data("path", jsonResult.AbsoluteFilePath);
                            } else if(uploadMode === "replace") {
                                $(uploadedImages).attr("src", jsonResult.RelativeFilePath);
                                $(uploadedImages).data("id", jsonResult.Id);
                                $(uploadedImages).data("path", jsonResult.AbsoluteFilePath);
                            }
                            initPluginsOnRemoteContent();
                        }
                    }
                };
                var form = $(".photo-upload-form").get(0);
                var formData = new FormData(form);
                formData.append("file", file);
                formData.append("returnFileType", returnFileType);
                xhr.send(formData);
            }

            function _defaults(e) {
                e.stopPropagation();
                e.preventDefault();
            }

        });
    };
}(jQuery));

//(function ($) {
//    $.fn.imageUpload = function () {
//        var template = "<div class='thumbnail-centralize'>" +
//            "<div class='btn-group'>" +
//            "<button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><i class='fa fa-pencil'></i></button>" +
//            "<ul class='dropdown-menu pull-right' role='menu'>" +
//            "<li><a href='#' class='delete-image'>Delete</a></li>" +
//            "<li><a class='show-remote-dialog' href='#' data-toggle='modal' data-action='/Admin/Photo/DialogCropPhoto' data-target='#dialog-crop-photo'>Make cover</a></li>" +
//            "</ul>" +
//            "</div>" +
//            "<img />" +
//            "<div class='progress'>" +
//            "<div class='progress-bar' />" +
//            "</div>" +
//            "</div>";

//        var thumbnails = $(this).find(".thumbnails");

//        jQuery.event.fixHooks.drop = { props: ["dataTransfer"] };

//        $(this).on("dragenter", "#upload-content-dropbox", function (e) {
//            defaults(e);
//        });

//        $(this).on("dragleave", "#upload-content-dropbox", function (e) {
//            defaults(e);
//        });

//        $(this).on("dragover", "#upload-content-dropbox", function (e) {
//            defaults(e);
//        });

//        $(this).on("drop", "#upload-content-dropbox", function (e) {
//            uploadByDrop(e.dataTransfer.files, e);
//        });

//        function uploadBySelect(fileSelector, e) {
//            defaults(e);
//            handleImageFiles(fileSelector.files);
//        }

//        function defaults(e) {
//            e.stopPropagation();
//            e.preventDefault();
//        }

//        function uploadByDrop(files, e) {
//            defaults(e);
//            handleImageFiles(files, e);
//        }

//        function handleImageFiles(files, e) {
//            var xhr = new Array();
//            for (var i = 0; i < files.length; i++) {
//                xhr[i] = new XMLHttpRequest();

//                $("#dialog-validation-summary").empty();
//                createImage(files[i]);
//                uploadImageFile(files[i], xhr[i]);
//            }
//        }

//        function createImage(file) {
//            var preview = $(template);
//            var image = $("img", preview);
//            image.width = 100;
//            image.height = 100;
//            image.attr("src", "/Content/images/presentation/nophoto.png");
//            preview.appendTo(thumbnails);
//            $.data(file, preview);
//        }

//        function uploadImageFile(file, xhr) {
//            xhr.open("post", "/Admin/Photo/Upload", true);

//            xhr.upload.addEventListener("progress", function (event) {
//                if (event.lengthComputable) {
//                    var percentage = event.loaded / event.total * 100;
//                    $.data(file).find(".progress-bar").width(percentage + "%");
//                }
//            }, false);

//            xhr.onreadystatechange = function (event) {
//                if (xhr.readyState === 4 && xhr.status === 200) {
//                    var jsonResult = jQuery.parseJSON(xhr.response);
//                    if (jsonResult.Success) {
//                        $.data(file).find(".progress-bar").addClass("progress-bar-success");
//                        $.data(file).find("img").attr("src", jsonResult.Message);
//                        $.data(file).find("a").each(function () {
//                            $(this).attr("data-id", jsonResult.TargetId);
//                        });    
//                    } else {
//                        $("#" + jsonResult.TargetId).append(jsonResult.Message);
//                        $(".thumbnail-centralize").last().remove();
//                    }
//                    $("#file-selector").val("");
//                }
//            };
//            var form = $(".photo-upload-form").get(0);
//            var formData = new FormData(form);
//            formData.append("file", file);
//            xhr.send(formData);
//        }

//        $(".thumbnails").on("click", ".delete-image", function () {
//            $(this).load("/Admin/Photo/Delete", { id: $(this).data("id") }, function (result) {
//                var jsonObject = jQuery.parseJSON(result);
//                if (jsonObject.Success) {
//                    $(this).parents(".thumbnail-centralize").remove();
//                }
//            });
//        });

//        $("body").on("change", "#file-selector", function (e) {
//            uploadBySelect(this, e);
//        });
//    };
//}(jQuery));