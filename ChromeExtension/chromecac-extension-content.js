var bridge = {
	active: false,
	origin: null,
	sign: function (payload, success, fail) {
		chrome.runtime.sendMessage({
			data: payload
		}, function (data) {
			console.log("BG->Content");
			console.log(data);
			if (data === undefined || data === null) {
				fail();
			}
			else {
				if (typeof data.signature === 'string' && data.signature != "")
					success(data);
				else
					fail();
			}
		});
	},
	send: function (obj) {
		window.postMessage(obj, bridge.origin)
	}
}

window.addEventListener("message", function(event) {
	console.log("[Extension] Recv'd msg:");
	console.log(event);
	
	if (event.source != window || !event.isTrusted || !('type' in event.data))
		return;
	
	if (event.data.type == "ChromeCAC_init") {
		if (bridge.active)
			return;
		bridge.active = true;
		if (bridge.origin == null)
			bridge.origin = event.origin;

		bridge.send({
			type: "ChromeCAC_setactive",
			active: true
		});
	}
	else if (event.data.type == "ChromeCAC_sign") {
		if (!bridge.active)
			return;
		
		bridge.sign(event.data.payload, function (signed) {
			bridge.send({
				type: "ChromeCAC_signed",
				success: true,
				data: event.data,
				signature: signed
			});
		}, function (error) {
			bridge.send({
				type: "ChromeCAC_signed",
				success: false,
				data: event.data,
				signature: null
			});
		});
	}
});