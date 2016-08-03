(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], factory);
    } else if (typeof module === 'object' && module.exports) {
        // Node. Does not work with strict CommonJS, but
        // only CommonJS-like environments that support module.exports,
        // like Node.
        module.exports = factory();
    } else {
        // Browser globals (root is window)
        root.CAC = factory();
    }
}(this, function () {

    var callbacks = [];
    var isActive = false;

    function sign(str, success, fail) {
        if (!isActive) {
            fail();
            return;
        }

        window.postMessage({
            type: "ChromeCAC_sign",
            payload: str,
            _id: callbacks.length
        }, "*"); // TODO dont use "*"

        callbacks.push({
            success: success,
            fail: fail
        });
    }

    window.addEventListener("message", function (event) {
        console.log("[Webpage] Recv'd message:");
        console.log(event);

        if (event.source != window || !event.isTrusted || (!'type' in event.data))
            return;

        if (event.data.type == "ChromeCAC_setactive") {
            isActive = true;
            console.log("ChromeCAC is active.")
        } else if (event.data.type == "ChromeCAC_signed") {
            var cb = callbacks[event.data.data._id];
            if (event.data.success) {
                cb.success(event.data.signature);
            }
            else {
                cb.fail({
                    data: event.data
                });
            }
        }
    })

    window.postMessage({
        type: "ChromeCAC_init"
    }, "*"); // TODO dont use "*", target the specific extension

    return {
        sign: sign
    };
})); 
