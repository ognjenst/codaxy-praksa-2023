import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";
import { openAddPeriodicTriggerWindow } from "./openAddPeriodicTriggerWindow";
import { openAddIoTTriggerWindow } from "./openAddIoTTriggerWindow";

export default class extends Controller {
    async onInit() {
        this.loadData();
    }

    async loadData() {
        try {
            let periodicTriggers = await GET("/triggers/PeriodicTrigger");
            this.store.set("$page.periodicTriggers", periodicTriggers);
            let iotTriggers = await GET("/triggers/IoTTrigger");
            this.store.set("$page.iotTriggers", iotTriggers);
        } catch (err) {
            console.log(err);
        }
    }

    async showAddPeriodicTriggerWindow() {
        let res = await openAddPeriodicTriggerWindow();
        if (res) this.loadData();
    }

    async showAddIoTTriggerWindow() {
        let res = await openAddIoTTriggerWindow();
        if (res) this.loadData();
    }
}
