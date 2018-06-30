var registerFunction = Blazor.registerFunction;
var storageAssembly = 'Foundation.BlazorExtensions';
var storageNamespace = "" + storageAssembly;
var storages = {
    LocalStorage: localStorage,
    SessionStorage: sessionStorage
};
var _loop_1 = function () {
    var storage = storages[storageTypeName];
    var storageFullTypeName = storageNamespace + "." + storageTypeName;
    registerFunction(storageFullTypeName + ".Clear", function () {
        clear(storage);
    });
    registerFunction(storageFullTypeName + ".GetItem", function (key) {
        return getItem(storage, key);
    });
    registerFunction(storageFullTypeName + ".Key", function (index) {
        return key(storage, index);
    });
    registerFunction(storageFullTypeName + ".Length", function () {
        return getLength(storage);
    });
    registerFunction(storageFullTypeName + ".RemoveItem", function (key) {
        removeItem(storage, key);
    });
    registerFunction(storageFullTypeName + ".SetItem", function (key, data) {
        setItem(storage, key, data);
    });
    registerFunction(storageFullTypeName + ".GetItemString", function (key) {
        return getItemString(storage, key);
    });
    registerFunction(storageFullTypeName + ".SetItemString", function (key, data) {
        setItemString(storage, key, data);
    });
    registerFunction(storageFullTypeName + ".GetItemNumber", function (index) {
        return getItemNumber(storage, index);
    });
    registerFunction(storageFullTypeName + ".SetItemNumber", function (index, data) {
        setItemNumber(storage, index, data);
    });
};
for (var storageTypeName in storages) {
    _loop_1();
}
function clear(storage) {
    storage.clear();
}
function getItem(storage, key) {
    return storage.getItem(key);
}
function key(storage, index) {
    return storage.key(index);
}
function getLength(storage) {
    return storage.length;
}
function removeItem(storage, key) {
    storage.removeItem(key);
}
function setItem(storage, key, data) {
    storage.setItem(key, data);
}
function getItemString(storage, key) {
    return storage[key];
}
function setItemString(storage, key, data) {
    storage[key] = data;
}
function getItemNumber(storage, index) {
    return storage[index];
}
function setItemNumber(storage, index, data) {
    storage[index] = data;
}
//# sourceMappingURL=BlazorStorageInterop.js.map