Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.ShowRawHtml', (elementId, html) => {
  let el = document.getElementById(elementId);
  if (el === null) {
    el = document.getElementsByClassName(elementId)[0];
  }
  if (el === null) {
    el = document.getElementsByTagName(elementId)[0];
  }

  if (el) {
    el.innerHTML = html;
    return el.innerHTML;
  }
  else {
    console.log("HTML not rendered");
  }
});


Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.RawHtml', function (element, value) {
  element.innerHTML = value;
  for (var i = element.childNodes.length - 1; i >= 0; i--) {
    var childNode = element.childNodes[i];
    element.parentNode.insertBefore(childNode, element.nextSibling);
  }
  element.parentNode.removeChild(element);
  return true;
});

Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.CurrentUrl', ()=> {
  let path = window.location.pathname;
  if (path) {
    return path;
  }
  else {
    console.log("No current path");
  }
});

Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.GetJsonValue', (name, jsonAsString) => {
  var jsonObj = JSON.parse(jsonAsString);

  return jsonObj[name];
  
});

Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.GetJsonFields', (jsonAsString) => {
  var jsonObj = JSON.parse(jsonAsString);

  return jsonObj[name];

});

Blazor.registerFunction('Foundation.BlazorExtensions.BlazorExtensionsInterop.HardReload', () => {
  window.location.reload();
  return true;
});
