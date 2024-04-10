
/**
 * 初始化 IndexedDB 数据库
 * @param {any} dbName
 * @param {any} version
 */
export function initializeDb(dbName, version) {
    let db = indexedDB.open(dbName, version);
    db.onupgradeneeded = function () {
        let newDb = db.result;
        newDb.createObjectStore("testStore", { keyPath:"key" })
    }
}                                                                                                                                                                                                                      

export async function setAsync(dbName, version, value) {
    let request = new Promise((resolve) => {
        let db = indexedDB.open(dbName, version);
        db.onsuccess = function () {
            let newDb = db.result;
            let trans = newDb.transaction("testStore", "readwrite")
            let table = trans.objectStore("testStore");
            let result = table.put(value);
            result.onsuccess = function (e) {
                resolve(result.result)
            }
        }
    })
}

export async function getAsync(dbName, version, key) {
    let request = new Promise((resolve) => {
        let db = indexedDB.open(dbName, version);
        db.onsuccess = function () {
            let newDb = db.result;
            let trans = newDb.transaction("testStore", "readonly")
            let table = trans.objectStore("testStore");
            let result = table.get(key);
            result.onsuccess = function (e) {
                resolve(result.result)
            }
        }
    });

    return await request;
}

export async function deleteAsync(dbName, version, key) {
    let request = new Promise((resolve) => {
        let db = indexedDB.open(dbName, version);
        db.onsuccess = function () {
            let newDb = db.result;
            let trans = newDb.transaction("testStore", "readwrite")
            let table = trans.objectStore("testStore");
            let result = table.delete(key);
            result.onsuccess = function (e) {
                resolve()
            }
        }
    });

    return await request;
}

export async function clearAsync(dbName, version) {
    let request = new Promise((resolve) => {
        let db = indexedDB.open(dbName, version);
        db.onsuccess = function () {
            let newDb = db.result;
            let trans = newDb.transaction("testStore", "readwrite")
            let table = trans.objectStore("testStore");
            let result = table.clear()
            result.onsuccess = function (e) {
                resolve();
            }
        }
    });

    return await request;
}