import { Store } from "cx/data";
import { History, Widget, enableCultureSensitiveFormatting, startHotAppLoop } from "cx/ui";
import { Debug, Timing } from "cx/util";

enableCultureSensitiveFormatting();

//store
const store = new Store({
    data: { login: true },
});

//Remove in the latter stage of the project
window.store = store;

//routing
//Url.setBaseFromScript("app*.js");
History.connect(store, "url");

//debug
Widget.resetCounter();
Timing.enable("app-loop");
Debug.enable("app-data");

//app loop
import Routes from "./routes";

startHotAppLoop(module, document.getElementById("app"), store, Routes);
