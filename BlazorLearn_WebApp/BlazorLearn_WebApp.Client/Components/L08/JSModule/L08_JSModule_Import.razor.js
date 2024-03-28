export const sayHello1 = function (arg) {
    console.info("Hello! colocated " + arg);
}

export const passingCSharpObj = function (obj) {
    obj.age = 17
    console.info(obj);
}

export const passingCSharpObjAndSayHi = function (obj) {
    obj.age = 17;
    console.info(obj);
    obj.invokeMethodAsync("SayHi", obj)
        .then(res => {
            console.warn(res);
        });
}