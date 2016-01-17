//site.js
(function () {

    //var ele = $("#userName");
    //ele.text("Hayriye Türer");
    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background-color:#888;";
    //});
    //main.on("mouseleave", function () {
    //    main.style = "";
    //});
    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    alert("hello");
    //});
    var $sibarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sibarAndWrapper.toggleClass("hide-sidebar");
        if ($sibarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
})();