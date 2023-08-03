import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import { openDryRunWindow } from "../dry-run";

export default class extends Controller {
    onInit(): void {
        this.store.set("$page.selecteTab", "Input1");
    }

    addNewInputVariable() {
        var params = [];
        this.store.get("$page.currentWorkflow.inputParameters").forEach((element, index) => {
            params.push({
                id: index,
                text: element,
            });
        });

        let conditions = this.store.get("$task.conditions");
        conditions.push({
            tab: "Input" + (this.store.get("$task.conditions").length + 1),
            source: this.store.get("$task.conditions")[this.store.get("$task.conditions").length - 1].source,
        });

        this.store.set("$task.conditions", [...conditions]);
    }

    openDryWindow() {
        openDryRunWindow({
            task: {
                inputs: this.store.get("$task.conditions"),
                expression: this.store.get("$task.expression"),
            },
        });
    }
}
