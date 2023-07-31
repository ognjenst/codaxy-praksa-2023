import { Controller } from "cx/ui";
import { GET, POST } from "../../api/util/methods";

export default class extends Controller {
    onInit() {
        this.loadWorkflows();
        this.loadTriggers();
    }

    async loadWorkflows() {
        try {
            let workflows = await GET("/workflows");
            this.store.set("$page.workflows", workflows);
        } catch (err) {
            console.log(err);
        }
    }
    async loadTriggers() {
        let type = this.store.get("$route.type");
        try {
            let triggers = await GET("/triggers/{type}");
            this.store.set("$page.triggers", triggers);
        } catch (err) {
            console.log(err);
        }
    }

    async addAutomation() {
        let workflowId = this.store.get("$page.automations.workflows");
        let triggerId = this.store.get("$page.automations.triggers");

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

    addTrigger() {}
}
