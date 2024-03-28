export function promptHello() {
    alert("Hello! Blazor! Collocated!")
    callDotNetHelloAsync();
}

async function callDotNetHelloAsync() {
    var runtime = globalThis.getDotnetRuntime(0); // 获取到 .Net 运行时
    var assembly = await runtime.getAssemblyExports("BlazorLearn_WebApp.Client.dll"); // 根据程序集名称获取到程序集
    assembly.BlazorLearn_WebApp.Client.Components.L08.L08_JsInteropService.SayHelloInConsole(); // 使用 命名空间+类名称.方法名称 的方式调用 CSharp 方法
}