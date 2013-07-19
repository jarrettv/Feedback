/* ==========================================================
 * bootstrap-save.js v2.2.0
 * http://
 * ==========================================================
 * Copyright 2012 CTS, Inc. by Jarrett Vance
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================== */


!function ($) {

    "use strict"; // jshint ;_;


    var Save = function (element, options) {
        this.$element = $(element)
        this.options = $.extend({}, $.fn.save.defaults, options)
    }
    
    Save.prototype = {

        constructor: Save

        , save: function () {
            this.$element.closest('.modal').modal('loading')
            var that = this;
            $.ajax(this.$element.prop('action'), {
                type: "POST",
                data: this.$element.serialize(),
                statusCode: {
                    200: function (data) { that.success(data) },
                    205: function () { window.location.reload() }, // no content, reload
                    400: function (xhr) { that.error(xhr.responseText) }
                }
            });
        }

        , success: function (data) {
            var $html = $($.parseHTML($.trim(data)))
            this.$element.closest('.modal').modal('hide')
            var item = $(this.options.container).find('[data-id=' + $html.data('id') + ']')
            if (item.length) {
                item.after($html)
                item.remove()
                //if (item.hasClass('deleted'))
                //    item.delay('300').fadeOut();
            } else {
                $(this.options.container).prepend($html) // insert new item at top
                $(this.options.container).find('.none').remove() // remove none message
            }
            $html.trigger('loaded');
        }

        , error: function (data) {
            var modal = this.$element.closest('.modal')
            //modal.children().empty()
            modal.html(data)
            modal.modal('loaded')
            modal.trigger('shown')
        }
    }

    /* SAVE PLUGIN DEFINITION
     * ======================== */

    $.fn.save = function (option) {
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('save')
              , options = typeof option == 'object' && option
            if (!data) $this.data('save', (data = new Save(this, options)))
            if (option == 'save' || options.submit == 'save') data.save()
        })
    }

    $.fn.save.defaults = {
        container: 'tbody'
    }

    $.fn.save.Constructor = Save
    
    /* SAVE DATA-API
     * ============== */

    $(document).on('submit.data-api', 'form[data-submit=save]', function (e) {
        e.preventDefault()
        var $trigger = $(e.target)
        $trigger.save($trigger.data())
    });

}(window.jQuery);
