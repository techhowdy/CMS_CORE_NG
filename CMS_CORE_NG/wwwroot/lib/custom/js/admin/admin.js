(function () {
    'use strict';

    $(initSidebar);

    function initSidebar() {

        $(document).ready(function () {

            $('#sidebarCollapse').on('click', function () {

                $('#sidebar').toggleClass('active');
            });

        });

    }
})();

/* EXTENSION METHOD TO GET BROWSER COOKIE */
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}