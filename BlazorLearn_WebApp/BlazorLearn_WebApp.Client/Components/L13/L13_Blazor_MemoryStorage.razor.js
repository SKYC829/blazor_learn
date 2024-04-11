
async function getDotNetAssembly() {
    var runtime = globalThis.getDotnetRuntime(0);
    var assembly = await runtime.getAssemblyExports("BlazorLearn_WebApp.Client.dll");
    return assembly;
}

function getKey() {
    return document.getElementById('tb_Key').value;
}

function getValue() {
    return document.getElementById('tb_Val').value;
}

function setStatus(status) {
    document.getElementById('lb_Status').innerHTML = status;
}

async function onSubmit() {
    let asm = await getDotNetAssembly();
    asm.BlazorLearn_WebApp.Client.Components.L13.MemoryStorageAccessorForJs.Set(getKey(), getValue());
    setStatus('Successfully saved data to MemoryStorage from JavaScript!')
}

async function onGet() {
    let asm = await getDotNetAssembly();
    let value = asm.BlazorLearn_WebApp.Client.Components.L13.MemoryStorageAccessorForJs.Get(getKey())
    setStatus('The result of getting ' + getKey() + ' from MemoryStorage in JavaScript is ' + value)
}

async function onDel() {
    let asm = await getDotNetAssembly();
    asm.BlazorLearn_WebApp.Client.Components.L13.MemoryStorageAccessorForJs.Delete(getKey())
    setStatus('Successfully deleted ' + getKey() + ' from MemoryStorage in JavaScript')
}

async function onClear() {
    let asm = await getDotNetAssembly();
    asm.BlazorLearn_WebApp.Client.Components.L13.MemoryStorageAccessorForJs.Clear();
    setStatus('Successfully cleared MemoryStorage from JavaScript')
}