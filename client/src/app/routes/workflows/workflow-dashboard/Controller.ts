import { Controller } from "cx/ui";
import { PUT } from "../../../api/util/methods";
import { openInsertUpdateWindow } from "../update-insert-workflow";
import { openPlayWorkflowWindow } from "../play-workflow";

export default class extends Controller {
    onInit(): void {}

    deleteUndoneWorkflow() {
        var arrUndone = this.store.get("$page.undoneWorkflows").filter((value, index, arr) => {
            if (value.name == this.store.get("$page.currentWorkflow.name")) return false;

            return true;
        });

        this.store.set("$page.undoneWorkflows", arrUndone);
    }

    deleteWorkflow() {
        console.log("http delete workflow ...");
    }

    async updateWorkflow() {
        let newObj: any = await openInsertUpdateWindow({
            props: {
                action: "Update",
                name: this.store.get("$page.currentWorkflow.name"),
                description: this.store.get("$page.currentWorkflow.description"),
                version: this.store.get("$page.currentWorkflow.version"),
            },
        });

        if (!newObj) return;

        if (this.store.get("$page.currentWorkflowInUndoneList") == true) {
            this.store.update("$page.undoneWorkflows", (elements) =>
                elements.map((el) => (el.name == this.store.get("$page.currentWorkflow.name") ? newObj : el))
            );
        } else {
            this.store.update("$page.undoneWorkflows", (elements) => [...elements, newObj]);

            var arrUndone = this.store.get("$page.workflows").filter((value, index, arr) => {
                if (value.name == this.store.get("$page.currentWorkflow.name")) return false;

                return true;
            });

            this.store.set("$page.workflows", arrUndone);
        }
    }

    playWorkflow() {
        openPlayWorkflowWindow({
            props: {
                name: this.store.get("$page.currentWorkflow").name,
                version: this.store.get("$page.currentWorkflow").version,
            },
        });
    }

    async registerWorkflow() {
        var arrTasks = [];
        var workflowTasks = this.store.get("$page.arrTasks");
        for (let i = 0; i < workflowTasks.length; i++) {
            var inputParameters = new Map();

            for (let j = 0; j < workflowTasks[i].inputs.length; j++) {
                var sourceIndex = workflowTasks[i].inputs[j].sourceDecision;
                var paramIndex = workflowTasks[i].inputs[j].paramDecision;

                try {
                    var sourceText = workflowTasks[i].inputs[j].source[sourceIndex - 1].text;
                    var paramText = workflowTasks[i].inputs[j].param[paramIndex - 1].text;
                    inputParameters.set(workflowTasks[i].inputs[j].tab, sourceText + "." + paramText);
                } catch (err) {}
            }

            var tempObj = {
                name: workflowTasks[i].name,
                taskReferenceName: workflowTasks[i].taskReferenceName,
                inputParameters,
            };

            arrTasks.push(tempObj);
        }

        var obj = {
            name: this.store.get("$page.currentWorkflow.name"),
            description: this.store.get("$page.currentWorkflow.description"),
            version: this.store.get("$page.currentWorkflow.version"),
            tasks: arrTasks,
        };

        //console.log(obj);

        /*
        try {
            var resp = PUT(BACKEND_REQUEST_REGISTER_WORKFLOW, obj);
        } catch (err) {
            console.error(err);
        }
        */
    }
}

const BACKEND_REQUEST_REGISTER_WORKFLOW = "/workflows/register_workflow";
