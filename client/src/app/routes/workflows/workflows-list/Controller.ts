import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class Controller1 extends Controller {
    onInit(): void {
        this.store.set("$page.workflows", []);

        this.loadData();
    }

    async loadData() {
        try {
            let resp = await GET("/workflows");
            this.store.set("$page.workflows", resp);

            if (resp.length > 0) {
                this.store.set("$page.currentWorkflow", resp[0]);
            } else {
                this.store.set("$page.flagDashboard", false);
            }
        } catch (err) {
            console.error(err);
        }
    }

    itemClicked(currentWorkflow) {
        this.store.set("$page.currentWorkflow", currentWorkflow);
        this.store.set("$page.arrTasks", this.store.get("$page.currentWorkflow.tasks"));
        this.store.set("$page.currentWorkflowInUndoneList", false);
        this.store.set("$page.flagDashboard", true);
    }
}
