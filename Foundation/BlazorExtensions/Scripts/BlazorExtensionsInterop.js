var BlazorExtensionsInterop = /** @class */ (function () {
    function BlazorExtensionsInterop() {
    }
    BlazorExtensionsInterop.prototype.hardReload = function () {
        window.location.reload();
    };
    BlazorExtensionsInterop.prototype.setPageTitle = function (pageTitle) {
        window.document.title = pageTitle;
    };
    return BlazorExtensionsInterop;
}());
function loadBlazorExtensionsInterop() {
    window['blazorExtensionsInterop'] = new BlazorExtensionsInterop();
}
loadBlazorExtensionsInterop();
//# sourceMappingURL=BlazorExtensionsInterop.js.map