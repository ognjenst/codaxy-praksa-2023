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
    }
}
