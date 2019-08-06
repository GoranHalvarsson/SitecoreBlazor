
class BlazorExtensionsInterop {
    public  hardReload() {
        window.location.reload();
    }

    public  setPageTitle(pageTitle: string) {
        window.document.title = pageTitle;
    }
}



function loadBlazorExtensionsInterop(): void {
    window['blazorExtensionsInterop'] = new BlazorExtensionsInterop();
}


loadBlazorExtensionsInterop();