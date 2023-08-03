import { Controller } from "cx/ui";
import { DELETE, GET } from "../../api/util/methods";
import { openAddPeriodicTriggerWindow } from "./openAddPeriodicTriggerWindow";
import { openAddIoTTriggerWindow } from "./openAddIoTTriggerWindow";

export default class extends Controller {
    onInit() {
        this.loadData();
    }

    async loadData() {
        try {
            let periodicTriggers = await GET("/triggers/PeriodicTrigger");
            console.log(periodicTriggers);
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

    async deletePeriodicTrigger(e, { store }) {
        const { id } = store.get("$record");
        await DELETE(`/triggers/PeriodicTrigger/${id}`);
        this.loadData();
    }

    async deleteIotTrigger(e, { store }) {
        const { id } = store.get("$record");
        await DELETE(`/triggers/IoTTrigger/${id}`);
        this.loadData();
    }
}
