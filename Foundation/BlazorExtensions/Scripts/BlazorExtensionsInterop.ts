
class BlazorExtensionsInterop {
    public hardReload() {
        window.location.reload();
    }

    public setPageTitle(pageTitle: string) {
        window.document.title = pageTitle;
    }

    public scrollToFragment(elementId: string) {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({
                behavior: 'smooth'
            });
        }
    }

}



function loadBlazorExtensionsInterop(): void {
    window['blazorExtensionsInterop'] = new BlazorExtensionsInterop();
}


loadBlazorExtensionsInterop();