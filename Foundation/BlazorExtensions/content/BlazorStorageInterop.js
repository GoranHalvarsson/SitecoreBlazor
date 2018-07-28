// Copyright (c) 2018 cloudcrate solutions UG (haftungsbeschraenkt)
var storageAssembly = 'Foundation_BlazorExtensions';
var storageNamespace = "" + storageAssembly;
var storages = {
    LocalStorage: localStorage,
    SessionStorage: sessionStorage
};
var _loop_1 = function () {
    if (storages.hasOwnProperty(storageTypeName)) {
        var storage_1 = storages[storageTypeName];
        var storageFullTypeName = storageNamespace + "_" + storageTypeName;
        window[storageFullTypeName] = {
            Clear: function () {
                clear(storage_1);
            },
            GetItem: function (key) {
                return getItem(storage_1, key);
            },
            Key: function (index) {
                return key(storage_1, index);
            },
            Length: function () {
                return getLength(storage_1);
            },
            RemoveItem: function (key) {
                removeItem(storage_1, key);
            },
            SetItem: function (key, data) {
                setItem(storage_1, key, data);
            },
            GetItemString: function (key) {
                return getItemString(storage_1, key);
            },
            SetItemString: function (key, data) {
                setItemString(storage_1, key, data);
            },
            GetItemNumber: function (index) {
                return getItemNumber(storage_1, index);
            },
            SetItemNumber: function (index, data) {
                setItemNumber(storage_1, index, data);
            }
        };
    }
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