(function ($) {
    $.imageCrop = function (object, customOptions) {
        var defaultOptions = {
            id: 0,
            path: "",
            allowMove: true,
            allowResize: true,
            allowSelect: true,
            aspectRatio: 1.333,
            minSelect: [0, 0],
            minSize: [0, 0],
            maxSize: [0, 0],
            outlineOpacity: 0.5,
            overlayOpacity: 0.5,
            previewFadeOnBlur: 1,
            previewFadeOnFocus: 0.35,
            selectionPosition: [0, 0],
            selectionWidth: 0,
            selectionHeight: 0,

            onChange: function () { },
            onSelect: function () { }
        };

        var options = defaultOptions;
        _setOptions(customOptions);


        var $image = $(object);
        options.id = $image.data("id");
        options.path = $image.data("path");
        options.minSize[0] = $image.data("min-width");
        options.minSize[1] = $image.data("min-height");

        var $holder = $('<div />')
            .css({
                position: 'relative'
            })
            .width(object.width)
            .height(object.height);

        $image.wrap($holder)
            .css({
                position: 'absolute'
            });

        var $overlay = $('<div id="image-crop-overlay" />')
            .css({
                opacity: options.overlayOpacity,
                position: 'absolute'
            })
            .width(object.width)
            .height(object.height)
            .insertAfter($image);

        var $trigger = $('<div />')
            .css({
                backgroundColor: '#000000',
                opacity: 0,
                position: 'absolute'
            })
            .width(object.width)
            .height(object.height)
            .insertAfter($overlay);

        var $outline = $('<div id="image-crop-outline" />')
            .css({
                opacity: options.outlineOpacity,
                position: 'absolute'
            })
            .insertAfter($trigger);

        var $selection = $('<div />')
            .css({
                background: 'url(' + $image.attr('src') + ') no-repeat',
                position: 'absolute'
            })
            .insertAfter($outline);

        var $nwResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-nw-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $nResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-n-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $neResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-ne-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $wResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-w-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $eResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-e-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $swResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-sw-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $sResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-s-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $seResizeHandler = $('<div class="image-crop-resize-handler" id="image-crop-se-resize-handler" />')
            .css({
                opacity: 0.5,
                position: 'absolute'
            })
            .insertAfter($selection);

        var $selectionPosX = $("<input type='hidden' id='PhotoCropDto.PositionX' name='PhotoCropDto.PositionX' />")
            .insertAfter($selection);
        var $selectionPosY = $("<input type='hidden' id='PhotoCropDto.PositionY' name='PhotoCropDto.PositionY' />")
            .insertAfter($selection);
        var $selectionWidth = $("<input type='hidden' id='PhotoCropDto.Width' name='PhotoCropDto.Width' />")
            .insertAfter($selection);
        var $selectionHeight = $("<input type='hidden' id='PhotoCropDto.Height' name='PhotoCropDto.Height' />")
            .insertAfter($selection);
        var $id = $("<input type='hidden' id='PhotoCropDto.Id' name = 'PhotoCropDto.Id' />")
            .insertAfter($selection);
        $id.val(options.id);
        var $path = $("<input type='hidden' id='PhotoCropDto.Path' name = 'PhotoCropDto.Path' />")
            .insertAfter($selection);
        $path.val(options.path);

        var resizeHorizontally = true,
            resizeVertically = true,
            selectionExists,
            selectionOffset = [0, 0],
            selectionOrigin = [0, 0];

        if (options.selectionWidth > options.minSelect[0] &&
            options.selectionHeight > options.minSelect[1])
            selectionExists = true;
        else
            selectionExists = false;

        _updateInterface();

        if (options.allowSelect)
            $trigger.mousedown(_setSelection);

        if (options.allowMove)
            $selection.mousedown(_pickSelection);

        if (options.allowResize)
            $('div.image-crop-resize-handler').mousedown(_pickResizeHandler);

        function _setOptions(customOptions) {
            options = $.extend(options, customOptions);
        }

        // Get the current offset of an element
        function _getElementOffset(object) {
            var offset = $(object).offset();

            return [offset.left, offset.top];
        }

        // Get the current mouse position relative to the image position
        function _getMousePosition(event) {
            var imageOffset = _getElementOffset($image);

            var x = event.pageX - imageOffset[0],
                y = event.pageY - imageOffset[1];

            x = x < 0 ? 0 : x > $image.width() ? $image.width() : x;
            y = y < 0 ? 0 : y > $image.height() ? $image.height() : y;

            return [x, y];
        }

        // Return an object containing information about the plug-in state
        function _getCropData() {
            $selectionPosX.val(parseInt(options.selectionPosition[0]));
            $selectionPosY.val(parseInt(options.selectionPosition[1]));
            $selectionWidth.val(parseInt(options.selectionWidth));
            $selectionHeight.val(parseInt(options.selectionHeight));
            return {
                selectionX: options.selectionPosition[0],
                selectionY: options.selectionPosition[1],
                selectionWidth: options.selectionWidth,
                selectionHeight: options.selectionHeight,

                selectionExists: function () {
                    return selectionExists;
                }
            };
        }

        // Update the overlay layer
        function _updateOverlayLayer() {
            $overlay.css({
                display: selectionExists ? 'block' : 'none'
            });
        }

        // Update the trigger layer
        function _updateTriggerLayer() {
            $trigger.css({
                cursor: options.allowSelect ? 'crosshair' : 'default'
            });
        }

        // Update the selection
        function _updateSelection() {
            $outline.css({
                cursor: 'default',
                display: selectionExists ? 'block' : 'none',
                left: options.selectionPosition[0],
                top: options.selectionPosition[1]
            })
                .width(options.selectionWidth)
                .height(options.selectionHeight);

            $selection.css({
                backgroundPosition: - options.selectionPosition[0] - 1 + 'px ' + (- options.selectionPosition[1] - 1) + 'px',
                cursor: options.allowMove ? 'move' : 'default',
                display: selectionExists ? 'block' : 'none',
                left: options.selectionPosition[0] + 1,
                top: options.selectionPosition[1] + 1
            })
                .width(options.selectionWidth - 2 > 0 ? options.selectionWidth - 2 : 0)
                .height(options.selectionHeight - 2 > 0 ? options.selectionHeight - 2 : 0);
        }

        // Update the resize handlers
        function _updateResizeHandlers(action) {
            switch (action) {
                case 'hide-all':
                    $('.image-crop-resize-handler').each(function () {
                        $(this).css({
                            display: 'none'
                        });
                    });

                    break;
                default:
                    var display = selectionExists && options.allowResize ? 'block' : 'none';

                    $nwResizeHandler.css({
                        cursor: 'nw-resize',
                        display: display,
                        left: options.selectionPosition[0] - Math.round($nwResizeHandler.width() / 2),
                        top: options.selectionPosition[1] - Math.round($nwResizeHandler.height() / 2)
                    });

                    $nResizeHandler.css({
                        cursor: 'n-resize',
                        display: display,
                        left: options.selectionPosition[0] + Math.round(options.selectionWidth / 2 - $neResizeHandler.width() / 2) - 1,
                        top: options.selectionPosition[1] - Math.round($neResizeHandler.height() / 2)
                    });

                    $neResizeHandler.css({
                        cursor: 'ne-resize',
                        display: display,
                        left: options.selectionPosition[0] + options.selectionWidth - Math.round($neResizeHandler.width() / 2) - 1,
                        top: options.selectionPosition[1] - Math.round($neResizeHandler.height() / 2)
                    });

                    $wResizeHandler.css({
                        cursor: 'w-resize',
                        display: display,
                        left: options.selectionPosition[0] - Math.round($neResizeHandler.width() / 2),
                        top: options.selectionPosition[1] + Math.round(options.selectionHeight / 2 - $neResizeHandler.height() / 2) - 1
                    });

                    $eResizeHandler.css({
                        cursor: 'e-resize',
                        display: display,
                        left: options.selectionPosition[0] + options.selectionWidth - Math.round($neResizeHandler.width() / 2) - 1,
                        top: options.selectionPosition[1] + Math.round(options.selectionHeight / 2 - $neResizeHandler.height() / 2) - 1
                    });

                    $swResizeHandler.css({
                        cursor: 'sw-resize',
                        display: display,
                        left: options.selectionPosition[0] - Math.round($swResizeHandler.width() / 2),
                        top: options.selectionPosition[1] + options.selectionHeight - Math.round($swResizeHandler.height() / 2) - 1
                    });

                    $sResizeHandler.css({
                        cursor: 's-resize',
                        display: display,
                        left: options.selectionPosition[0] + Math.round(options.selectionWidth / 2 - $seResizeHandler.width() / 2) - 1,
                        top: options.selectionPosition[1] + options.selectionHeight - Math.round($seResizeHandler.height() / 2) - 1
                    });

                    $seResizeHandler.css({
                        cursor: 'se-resize',
                        display: display,
                        left: options.selectionPosition[0] + options.selectionWidth - Math.round($seResizeHandler.width() / 2) - 1,
                        top: options.selectionPosition[1] + options.selectionHeight - Math.round($seResizeHandler.height() / 2) - 1
                    });
            }
        }

        // Update the cursor type
        function _updateCursor(cursorType) {
            $trigger.css({
                cursor: cursorType
            });

            $outline.css({
                cursor: cursorType
            });

            $selection.css({
                cursor: cursorType
            });
        }

        // Update the plug-in interface
        function _updateInterface(sender) {
            switch (sender) {
                case 'setSelection':
                    _updateOverlayLayer();
                    _updateSelection();
                    _updateResizeHandlers('hide-all');
                    break;
                case 'pickSelection':
                    _updateResizeHandlers('hide-all');
                    break;
                case 'pickResizeHandler':
                    _updateResizeHandlers('hide-all');
                    break;
                case 'resizeSelection':
                    _updateSelection();
                    _updateResizeHandlers('hide-all');
                    _updateCursor('crosshair');
                    break;
                case 'moveSelection':
                    _updateSelection();
                    _updateResizeHandlers('hide-all');
                    _updateCursor('move');
                    break;
                case 'releaseSelection':
                    _updateTriggerLayer();
                    _updateOverlayLayer();
                    _updateSelection();
                    _updateResizeHandlers();
                    break;
                default:
                    _updateTriggerLayer();
                    _updateOverlayLayer();
                    _updateSelection();
                    _updateResizeHandlers();
            }
        }

        // Set a new selection
        function _setSelection(event) {
            event.preventDefault();
            event.stopPropagation();
            $(document).mousemove(_resizeSelection);
            $(document).mouseup(_releaseSelection);

            selectionExists = true;
            options.selectionWidth = 0;
            options.selectionHeight = 0;
            selectionOrigin = _getMousePosition(event);

            options.selectionPosition[0] = selectionOrigin[0];
            options.selectionPosition[1] = selectionOrigin[1];

            _updateInterface('setSelection');
        }

        // Pick the current selection
        function _pickSelection(event) {
            event.preventDefault();
            event.stopPropagation();

            $(document).mousemove(_moveSelection);
            $(document).mouseup(_releaseSelection);

            var mousePosition = _getMousePosition(event);

            selectionOffset[0] = mousePosition[0] - options.selectionPosition[0];
            selectionOffset[1] = mousePosition[1] - options.selectionPosition[1];

            _updateInterface('pickSelection');
        }

        // Pick one of the resize handlers
        function _pickResizeHandler(event) {
            event.preventDefault();
            event.stopPropagation();

            switch (event.target.id) {
                case 'image-crop-nw-resize-handler':
                    selectionOrigin[0] += options.selectionWidth;
                    selectionOrigin[1] += options.selectionHeight;
                    options.selectionPosition[0] = selectionOrigin[0] - options.selectionWidth;
                    options.selectionPosition[1] = selectionOrigin[1] - options.selectionHeight;
                    break;
                case 'image-crop-n-resize-handler':
                    selectionOrigin[1] += options.selectionHeight;
                    options.selectionPosition[1] = selectionOrigin[1] - options.selectionHeight;
                    resizeHorizontally = false;
                    break;
                case 'image-crop-ne-resize-handler':
                    selectionOrigin[1] += options.selectionHeight;
                    options.selectionPosition[1] = selectionOrigin[1] - options.selectionHeight;
                    break;
                case 'image-crop-w-resize-handler':
                    selectionOrigin[0] += options.selectionWidth;
                    options.selectionPosition[0] = selectionOrigin[0] - options.selectionWidth;
                    resizeVertically = false;
                    break;
                case 'image-crop-e-resize-handler':
                    resizeVertically = false;
                    break;
                case 'image-crop-sw-resize-handler':
                    selectionOrigin[0] += options.selectionWidth;
                    options.selectionPosition[0] = selectionOrigin[0] - options.selectionWidth;
                    break;
                case 'image-crop-s-resize-handler':
                    resizeHorizontally = false;
                    break;
            }
            $(document).mousemove(_resizeSelection);
            $(document).mouseup(_releaseSelection);
            _updateInterface('pickResizeHandler');
        }

        // Resize the current selection
        function _resizeSelection(event) {
            event.preventDefault();
            event.stopPropagation();

            var mousePosition = _getMousePosition(event);

            var height = mousePosition[1] - selectionOrigin[1],
                width = mousePosition[0] - selectionOrigin[0];

            if (Math.abs(width) < options.minSize[0])
                width = width >= 0 ? options.minSize[0] : - options.minSize[0];

            if (Math.abs(height) < options.minSize[1])
                height = height >= 0 ? options.minSize[1] : - options.minSize[1];

            if (selectionOrigin[0] + width < 0 || selectionOrigin[0] + width > $image.width())
                width = - width;

            if (selectionOrigin[1] + height < 0 || selectionOrigin[1] + height > $image.height())
                height = - height;

            if (options.maxSize[0] > options.minSize[0] && options.maxSize[1] > options.minSize[1]) {

                if (Math.abs(width) > options.maxSize[0])
                    width = width >= 0 ? options.maxSize[0] : - options.maxSize[0];

                if (Math.abs(height) > options.maxSize[1])
                    height = height >= 0 ? options.maxSize[1] : - options.maxSize[1];
            }

            if (resizeHorizontally)
                options.selectionWidth = width;

            if (resizeVertically)
                options.selectionHeight = height;

            if (options.aspectRatio) {
                if (width > 0 && height > 0 || width < 0 && height < 0)
                    if (resizeHorizontally)
                        height = Math.round(width / options.aspectRatio);
                    else
                        width = Math.round(height * options.aspectRatio);
                else
                    if (resizeHorizontally)
                        height = - Math.round(width / options.aspectRatio);
                    else
                        width = - Math.round(height * options.aspectRatio);

                if (selectionOrigin[0] + width > $image.width()) {
                    width = $image.width() - selectionOrigin[0];
                    height = height > 0 ? Math.round(width / options.aspectRatio) : - Math.round(width / options.aspectRatio);
                }

                if (selectionOrigin[1] + height < 0) {
                    height = - selectionOrigin[1];
                    width = width > 0 ? - Math.round(height * options.aspectRatio) : Math.round(height * options.aspectRatio);
                }

                if (selectionOrigin[1] + height > $image.height()) {
                    height = $image.height() - selectionOrigin[1];
                    width = width > 0 ? Math.round(height * options.aspectRatio) : - Math.round(height * options.aspectRatio);
                }
                options.selectionWidth = width;
                options.selectionHeight = height;
            }

            if (options.selectionWidth < 0) {
                options.selectionWidth = Math.abs(options.selectionWidth);
                options.selectionPosition[0] = selectionOrigin[0] - options.selectionWidth;
            } else {
                options.selectionPosition[0] = selectionOrigin[0];
            }

            if (options.selectionHeight < 0) {
                options.selectionHeight = Math.abs(options.selectionHeight);
                options.selectionPosition[1] = selectionOrigin[1] - options.selectionHeight;
            } else {
                options.selectionPosition[1] = selectionOrigin[1];
            }

            options.onChange(_getCropData());

            _updateInterface('resizeSelection');
        }

        // Move the current selection
        function _moveSelection(event) {
            event.preventDefault();
            event.stopPropagation();

            var mousePosition = _getMousePosition(event);

            if (mousePosition[0] - selectionOffset[0] > 0)
                if (mousePosition[0] - selectionOffset[0] + options.selectionWidth < $image.width())
                    options.selectionPosition[0] = mousePosition[0] - selectionOffset[0];
                else
                    options.selectionPosition[0] = $image.width() - options.selectionWidth;
            else
                options.selectionPosition[0] = 0;


            if (mousePosition[1] - selectionOffset[1] > 0)
                if (mousePosition[1] - selectionOffset[1] + options.selectionHeight < $image.height())
                    options.selectionPosition[1] = mousePosition[1] - selectionOffset[1];
                else
                    options.selectionPosition[1] = $image.height() - options.selectionHeight;
            else
                options.selectionPosition[1] = 0;

            options.onChange(_getCropData());

            _updateInterface('moveSelection');
        }

        // Release the current selection
        function _releaseSelection(event) {
            event.preventDefault();
            event.stopPropagation();

            $(document).unbind('mousemove');
            $(document).unbind('mouseup');

            selectionOrigin[0] = options.selectionPosition[0];
            selectionOrigin[1] = options.selectionPosition[1];

            resizeHorizontally = true;
            resizeVertically = true;

            if (options.selectionWidth > options.minSelect[0] && options.selectionHeight > options.minSelect[1])
                selectionExists = true;
            else
                selectionExists = false;

            options.onSelect(_getCropData());

            _updateInterface('releaseSelection');
        }
    };

    $.fn.imageCrop = function (customOptions) {
        this.each(function() {
        //    $.imageCrop($(this), customOptions);
        //});
            var currentObject = this,
                image = new Image();
            //var w = $(currentObject).width();
            //$(image).on("load", function () {
            //    $.imageCrop(currentObject, customOptions);
            //});
            image.onload = function() {
                $.imageCrop(currentObject, customOptions);
            };
            image.src = currentObject.src;
        });
        return this;
    };
})(jQuery);