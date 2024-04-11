async function openCacheStorageAsync() {
    return await window.caches.open("My Cache Storage");
}

function createRequest(url, method, body = '') {
    let requestInit = {
        method: method
    }
    if (body != '') {
        requestInit.body = body
    }
    return new Request(url, requestInit)
}

/**
 * 增改缓存数据
 * @param {any} url
 * @param {any} method
 * @param {any} body
 * @param {any} response
 */
export async function storeAsync(url, method, body = '', responseString) {
    let request = createRequest(url, method, body);
    let response = new Response(responseString);
    let cache = await openCacheStorageAsync();
    //if ((await cache.keys()).findIndex(request) > -1) {
    //    await cache.delete(request);
    //}
    //cache.add
    await cache.put(request, response);
}

/**
 * 获取缓存数据
 * @param {any} url
 * @param {any} method
 * @param {any} body
 * @returns
 */
export async function getAsync(url, method, body = '') {
    let request = createRequest(url, method, body);
    let cache = await openCacheStorageAsync();
    let response = await cache.match(request);
    if (response === undefined) {
        return '';
    }
    return await response.text();
}

/**
 * 删除缓存数据
 * @param {any} url
 * @param {any} method
 * @param {any} body
 */
export async function deleteAsync(url, method, body = '') {
    let request = createRequest(url, method, body);
    let cache = await openCacheStorageAsync();
    await cache.delete(request);
}

/**
 * 清空缓存数据
 */
export async function clearAsync() {
    let cache = await openCacheStorageAsync();
    let keys = await cache.keys();
    for (var i = 0; i < keys.length; i++) {
        await cache.delete(keys[i])
    }
}