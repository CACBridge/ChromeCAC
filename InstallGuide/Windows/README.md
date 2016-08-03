## Manually Setup Native App for Chrome

Go to Chrome->Settings->Extensions. Turn on developer mode.

Click load unpacked extension, load the ChromeExtension folder from this repo.

Place the manifest.json file with the compiled executable.

Edit manifest.json, replace `{extension-id}` with the ID of the unpacked extension in the Chrome Extensions page

Edit registry.reg. Change the path to the path where the manifest file is located. Make sure to use `\\` instead of `\` or `/`!

Double click registry.reg to set the registry keys, Chrome should now be able to connect to cacbridge.chromecac for native messaging.