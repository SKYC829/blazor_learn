export function promptHello() {
    alert("Hello! Blazor! Collocated!")
    callDotNetHelloAsync();
}

async function callDotNetHelloAsync() {
    var runtime = globalThis.getDotnetRuntime(0); // ��ȡ�� .Net ����ʱ
    var assembly = await runtime.getAssemblyExports("BlazorLearn_WebApp.Client.dll"); // ���ݳ������ƻ�ȡ������
    assembly.BlazorLearn_WebApp.Client.Components.L08.L08_JsInteropService.SayHelloInConsole(); // ʹ�� �����ռ�+������.�������� �ķ�ʽ���� CSharp ����
}