export function setAsync(key, value) {
    return new Promise((resolve) => {
        window.sessionStorage.setItem(key, value)
        resolve()
    });
}

export function getAsync(key) {
    return new Promise((resolve) => {
        let value = window.sessionStorage.getItem(key)
        resolve(value)
    })
}

export function deleteAsync(key) {
    return new Promise((resolve) => {
        window.sessionStorage.removeItem(key)
        resolve()
    })
}

export function clearAsync() {
    return new Promise((resolve) => {
        window.sessionStorage.clear();
        resolve()
    })
}