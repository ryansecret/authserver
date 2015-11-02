
(function($) {
    $.ajaxAntiForgery = function(settings) {
        settings = settings || {};
        settings.data = settings.data || {};
        settings.data.__RequestVerificationToken = $("input[name='__RequestVerificationToken']").val() || "";

        return $.ajax(settings);
    };
})(jQuery);