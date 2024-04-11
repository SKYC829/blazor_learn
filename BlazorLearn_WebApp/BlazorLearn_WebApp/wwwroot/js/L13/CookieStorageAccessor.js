/**
 * 读取 Cookie
 * @param {any} name
 * @returns
 */
export function get(name) {
    var nameQuery = name + "=";
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i]
        while (cookie.charAt(0) == ' ') {
            cookie = cookie.substring(1, cookie.length);
        }
        if (cookie.indexOf(nameQuery) == 0) {
            return cookie.substring(nameQuery.length,cookie.length)
        }
    }
    return "";
}

/**
 * 新增或修改 Cookie
 * @param {any} name
 * @param {any} value
 * @param {any} expiresms
 * @param {any} path
 */
export function set(name, value, expiresms = '', path = '/') {
    if (expiresms && expiresms.toLowerCase() !== 'never') {
        var dt = new Date(expiresms);
        expiresms = "; expires=" + dt.toUTCString();
    } else if (expiresms && expiresms.toLowerCase() === 'never') {
        expiresms = "; expires=never";
    }
    else {
        expiresms = '';
    }
    if (!path || typeof path !== 'string') {
        path = document.location.pathname
    }
    document.cookie = name + "=" + value + expiresms + "; path=" + path;
}

/**
 * 删除 Cookie
 * @param {any} name
 */
export function remove(name, path = '/') {
    if (!path || typeof path !== 'string') {
        path = document.location.pathname
    }
    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT; path=" + path;
}

/**
 * 清空 Cookie
 */
export function clear() {
    document.cookie = '';
}