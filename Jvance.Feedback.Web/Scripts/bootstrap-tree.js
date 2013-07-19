/* ==========================================================
 * bootstrap-tree.js v2.2.0
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

    var Tree = function (element, options) {
        this.options = $.extend({}, $.fn.tree.defaults, options)
        this.$element = $(element)
        this.$element.on('click', 'label[data-toggle="folder"]', this.folder)
    }
    
    Tree.prototype = {

        constructor: Tree

        , init: function () {
            var checked = this.$element.find(':checked');
            if (checked.length) {
                checked.parentsUntil(this.$element, 'ol').parent().children('label[data-toggle="folder"]').trigger('click')
                this.$element.scrollTop(this.$element.scrollTop() + checked.position().top - this.$element.height() / 2 + checked.height() / 2)                
            }
        }

        , folder: function (e) {
            // TODO: use config classes
            $(this).children('.icon-folder-close, .icon-folder-open').toggleClass('icon-folder-close').toggleClass('icon-folder-open')
            $(this).parent().children('ol').toggle()
            e.stopPropagation()
        }
    }

    /* TREE PLUGIN DEFINITION
     * ======================== */

    $.fn.tree = function (option) {
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('tree')
              , options = typeof option == 'object' && option
            if (!data) $this.data('tree', (data = new Tree(this, options)))
            if (data.init) data.init()
        })
    }

    $.fn.tree.defaults = {
        openClass: 'icon-folder-open',
        closeClass: 'icon-folder-close'
    }

    $.fn.tree.Constructor = Tree
    
    /* TREE DATA-API
     * ============== */

    var onLoadOrLoaded = function () {

        $('ol[data-component="tree"]').each(function () {
            var $tree = $(this)
              , data = $tree.data()
            data.init = true
            $tree.tree(data)
        })
    }

    $(window).on('load', onLoadOrLoaded)
    $('.modal').on('loaded', onLoadOrLoaded)

}(window.jQuery);
