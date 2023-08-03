import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class extends Controller {
    onInit(): void {
        this.store.set("$page.undoneWorkflows", []);
    }

    itemClicked(currentWorkflow) {
        this.store.set("$page.currentWorkflow", currentWorkflow);
        this.store.set("$page.arrTasks", this.store.get("$page.currentWorkflow.tasks")); //$page.arrTasks shows tasks to the client which belong to the selected workflow($page.currentWorkflow)
        this.store.set("$page.currentWorkflowInUndoneList", true); //used for button save workflow
        this.store.set("$page.flagDashboard", true);

        if (this.store.get("$page.currentWorkflow.inputParameters").length == 0) {
            this.store.set("$page.showInputParameters", []);
        } else {
            this.store.set("$page.showInputParameters", []);
            for (let i = 0; i < this.store.get("$page.currentWorkflow.inputParameters").length; i++) {
                var obj = {
                    id: i,
                    text: this.store.get("$page.currentWorkflow.inputParameters")[i],
                };

                this.store.set("$page.showInputParameters", [...this.store.get("$page.showInputParameters"), obj]);
            }
        }
    }
}
