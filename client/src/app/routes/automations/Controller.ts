import { Controller } from "cx/ui";
import { DELETE, GET, POST } from "../../api/util/methods";
import { openInputParametersWindow } from "./openInputParametersWindow";
import { debounce } from "cx/util";

export default class extends Controller {
    onInit() {
        this.store.init("$page.automation.inputParameters", {});
        this.addTrigger("workflow-selection-change", ["$page.automation.workflowId"], (w) => this.initInputParameters(w));
        this.store.set("$page.automation.triggerType", 1);
        this.loadWorkflows();
        this.loadTriggers();
        this.loadAutomations();
    }

    async loadWorkflows() {
        try {
            let workflows = await GET("/workflows/db");
            this.store.set("$page.workflows", workflows);
            // let workflowsConductor = await GET("/workflows");
            // this.store.set("$page.workflowsConductor", workflowsConductor);
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
        let automation = this.store.get("$page.automation");
        const type = this.store.get("$page.automation.triggerType");
        automation = {
            ...automation,
            triggerId: type == 1 ? this.store.get("$page.automation.iotId") : this.store.get("$page.automation.periodicId"),
            inputParameters: JSON.stringify(automation.inputParameters),
        };
        try {
            await POST("/automation", automation);
            this.loadAutomations();
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

    initInputParameters(workflowId) {
        const workflows = this.store.get("$page.workflows");
        if (!workflows) return;
        const workflowDb = workflows.find((w) => w.id == workflowId);
        if (!workflowDb) return;
        // const { name, version } = workflowDb;
        // const workflowsConductor = this.store.get("$page.workflowsConductor");
        // if (!workflowsConductor) return;
        // const workflow = workflowsConductor.find((w) => w.name == name && w.version == version);
        // if (!workflow) return;
        const inputParams = workflowDb.inputParameters;
        const params = new Map();
        inputParams.forEach((param) => params.set(param, null));
        this.store.set("$page.automation.inputParameters", Object.fromEntries(params));
    }

    async deleteAutomation(e, { store }) {
        const { id } = store.get("$record");
        await DELETE(`/automation/${id}`);
        this.loadAutomations();
    }
}
