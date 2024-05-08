
const callbackAssembly = {};

export async function GoogleLoginInit(clientId, divName) {
    google.accounts.id.initialize({
        client_id: clientId,
        cancel_on_tap_outside: false,
        use_fedcm_for_prompt: true,
        callback: _googleLoginCallback
    });
    google.accounts.id.renderButton(document.getElementById(divName), { theme: "outline", size: "small", text: "continue_with", width: "64" });
    google.accounts.id.prompt();
}

function _googleLoginCallback(credentialResponse) {
    // TODO: notify .net logic that google credential was callback
    //window.Headers['provider'] = "google";
    //window.Headers['openId'] = credentialResponse.credential
    //window.location.reload();
    var xhr = new XMLHttpRequest();
    xhr.open('GET', window.location.href, true);
    xhr.setRequestHeader('provider', 'google');
    xhr.setRequestHeader('openId', credentialResponse.credential);
    xhr.onload = function () {
        window.location.reload();
    };
    xhr.send();
}