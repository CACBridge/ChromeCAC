function sendNativeMessage(obj, callback) {
    var port = chrome.runtime.connectNative("cacbridge.chromecac");
    var handled = false;
    port.onMessage.addListener(function (message) {
        callback(message);
        handled = true;
        port.disconnect();
    })
    port.onDisconnect.addListener(function() {
        if (!handled) {
            callback();
        }
    })
    port.postMessage(obj);
}

// Receive messages from the content script
chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
        // sender.tab is from content script 
        if (sender.tab)
        {
            console.log("Background about to do request");
            console.log(request);
            // Send a native message and callback to this listener's callback
            sendNativeMessage(request, function(response) {
                sendResponse(response);
            })
            // Returning true tells the response to handle asynchronously
            return true;
        }
    }
)