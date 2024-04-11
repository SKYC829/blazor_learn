export async function getAsync(name) {
    let request = new Promise((resolve) => {
        let result = window.localStorage.getItem(name);
        resolve(result)
    });
    return await request;
}

export async function setAsync(key, value) {
    let request = new Promise((resolve) => {
        window.localStorage.setItem(key, value);
        resolve();
    });
    await request;
}

export async function deleteAsync(key) {
    let request = new Promise((resolve) => {
        window.localStorage.removeItem(key);
        resolve()
    });
    await request;
}

export async function clearAsync() {
    let request = new Promise((resolve) => {
        window.localStorage.clear();
        resolve();
    })
    await request;
}