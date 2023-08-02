import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";
import { openAddPeriodicTriggerWindow } from "./openAddPeriodicTriggerWindow";
import { openAddIoTTriggerWindow } from "./openAddIoTTriggerWindow";

const units = ["Days", "Hours", "Minutes"];
const conditions = ["=", "<" , ">",  ">=", "<="]

export default class extends Controller {
    async onInit() {
        this.loadData();
    }

    async loadData() {
        try {
            let periodicTriggers = await GET("/triggers/PeriodicTrigger");
            this.store.set("$page.periodicTriggers", periodicTriggers.map((trigger) => { return {...trigger, start: new Date(trigger.start).toLocaleString("sr-SR"), unit: units[trigger.unit]}}));
            let iotTriggers = await GET("/triggers/IoTTrigger");
            this.store.set("$page.iotTriggers", iotTriggers.map((trigger) => { return {...trigger, condition: conditions[trigger.condition]}}));
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
