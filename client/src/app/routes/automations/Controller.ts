import { Controller } from "cx/ui";
import { GET, POST } from "../../api/util/methods";
import { openInputParametersWindow } from "./openInputParametersWindow";

export default class extends Controller {
    onInit() {
        this.store.set("$page.automation.triggerType", 1);
        this.loadWorkflows();
        this.loadTriggers();
        this.loadAutomations();
    }

    async loadWorkflows() {
        try {
            let workflows = await GET("/workflows/db");
            this.store.set("$page.workflows", workflows);
        } catch (err) {
            console.log(err);
        }
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

    async addAutomation() {
        let workflowId = this.store.get("$page.automation.workflows");
        let triggerId = this.store.get("$page.automation.triggers");

        let automation = {
            workflowId,
            triggerId,
        };
        console.log(automation);
        try {
            let createdAutomation = await POST("/automation", automation);
            this.store.set("$page.createdAutomation", createdAutomation);
        } catch (err) {
            console.log(err);
        }
    }

    async loadAutomations() {
        try {
            let automation = await GET("/automation");
            this.store.set("$page.automations", automation);
        } catch (err) {
            console.log(err);
        }
    }

    showInputParameters(e, { store }) {
        let info = store.get("$record");
        openInputParametersWindow(info);
    }

    addTrigger() {}
}
