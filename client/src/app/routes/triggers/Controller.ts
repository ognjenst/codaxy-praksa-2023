import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";

export default class extends Controller {
    async onInit() {
        this.loadTriggers();
    }

    async loadTriggers() {
        try {
            let periodicTriggers = await GET("/triggers/PeriodicTrigger");
            this.store.set("$page.periodicTriggers", periodicTriggers);
            let iotTriggers = await GET("/triggers/IoTTrigger");
            this.store.set("$page.iotTriggers", iotTriggers);
        } catch (err) {
            console.log(err);
        }
    }
}
