import { Controller } from "cx/ui";
import { DELETE, GET, POST, PUT } from "../../../api/util/methods";
import { openInsertUpdateWindow } from "../update-insert-workflow";
import { openPlayWorkflowWindow } from "../play-workflow";
import { MsgBox } from "cx/widgets";

export default class extends Controller {
    onInit(): void {}

    deleteUndoneWorkflow() {
        var arrUndone = this.store.get("$page.undoneWorkflows").filter((value, index, arr) => {
            if (value.name == this.store.get("$page.currentWorkflow.name")) return false;

            return true;
        });

        this.store.set("$page.undoneWorkflows", arrUndone);

        this.changeSelected();
    }

    async deleteWorkflow() {
        try {
            var curr = this.store.get("$page.currentWorkflow");
            await DELETE("/workflows/" + curr.name + "/" + curr.version);

            this.store.set(
                "$page.workflows",
                this.store.get("$page.workflows").filter((value, index, arr) => {
                    if (value.name === curr.name && value.version === curr.version) {
                        return false;
                    }

                    return true;
                })
            );

            this.changeSelected();
        } catch (err) {
            console.error(err);
        }
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

            this.store.set("$page.currentWorkflow", newObj);
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
        if (!this.checkValidity()) {
            return;
        }

        try {
            var arrTasks = [];
            var workflowTasks = this.store.get("$page.currentWorkflow.tasks");

            for (let i = 0; i < workflowTasks.length; i++) {
                var inputParameters = new Map();
                var conditionInputParameters = new Map();

                for (let j = 0; j < workflowTasks[i].inputs.length; j++) {
                    var sourceIndex = workflowTasks[i].inputs[j].sourceDecision;
                    var paramIndex = workflowTasks[i].inputs[j].paramDecision;

                    try {
                        var sourceText = workflowTasks[i].inputs[j].source[sourceIndex].text;
                        var paramText = workflowTasks[i].inputs[j].source[sourceIndex].param[paramIndex].text;
                        inputParameters.set(
                            workflowTasks[i].inputs[j].tab,
                            "${" + (sourceText.includes("$") ? sourceText.slice(1, sourceText.length) : sourceText) + "." + paramText + "}"
                        );
                    } catch (err) {}
                }

                if (workflowTasks[i].expression !== null) {
                    for (let j = 0; j < workflowTasks[i].conditions.length; j++) {
                        var sourceIndex = workflowTasks[i].conditions[j].sourceDecision;
                        var paramIndex = workflowTasks[i].conditions[j].paramDecision;

                        try {
                            var sourceText = workflowTasks[i].conditions[j].source[sourceIndex].text;
                            var paramText = workflowTasks[i].conditions[j].source[sourceIndex].param[paramIndex].text;
                            conditionInputParameters.set(
                                workflowTasks[i].conditions[j].tab,
                                "${" +
                                    (sourceText.includes("$") ? sourceText.slice(1, sourceText.length) : sourceText) +
                                    "." +
                                    paramText +
                                    "}"
                            );
                        } catch (err) {}
                    }
                }

                var tempObj = {
                    name: workflowTasks[i].name,
                    taskReferenceName: workflowTasks[i].taskReferenceName,
                    inputParameters: Object.fromEntries(inputParameters),
                    conditionInputParameters: Object.fromEntries(conditionInputParameters),
                    expression: workflowTasks[i].expression,
                    type: taskTypes[this.store.get("$task.type")].text,
                };

                arrTasks.push(tempObj);
            }

            var obj = {
                name: this.store.get("$page.currentWorkflow.name"),
                description: this.store.get("$page.currentWorkflow.description"),
                version: this.store.get("$page.currentWorkflow.version"),
                inputParameters: this.store.get("$page.currentWorkflow.inputParameters"),
                tasks: arrTasks,
            };

            console.log(obj);

            if (this.store.get("$page.currentWorkflow.action") === "Insert") await POST(BACKEND_REQUEST_REGISTER_WORKFLOW, obj);
            else await PUT(BACKEND_REQUEST_REGISTER_WORKFLOW, obj);

            this.store.set("", [...this.store.get("$page.workflows"), this.store.get("currentWorkflow")]);
            var arrUndone = this.store.get("$page.undoneWorkflows").filter((value, index, arr) => {
                if (value.name == this.store.get("$page.currentWorkflow.name")) return false;

                return true;
            });

            this.store.set("$page.undoneWorkflows", arrUndone);

            await this.loadData();

            this.changeSelected();
        } catch (err) {
            MsgBox.alert("Not able to create workflow ...");
        }
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

    changeSelected() {
        if (this.store.get("$page.undoneWorkflows").length == 0) {
            if (this.store.get("$page.workflows").length > 0) {
                this.store.set("$page.currentWorkflow", this.store.get("$page.workflows")[0]);
                this.store.set("$page.arrTasks", this.store.get("$page.currentWorkflow.tasks"));
                this.store.set("$page.currentWorkflowInUndoneList", false);
            } else {
                //if there are no elements then don't show
                this.store.set("$page.flagDashboard", false);
            }
        } else {
            this.store.set("$page.currentWorkflow", this.store.get("$page.undoneWorkflows")[0]);
            this.store.set("$page.arrTasks", this.store.get("$page.currentWorkflow.tasks"));
            this.store.set("$page.currentWorkflowInUndoneList", true);
        }
    }

    checkValidity() {
        try {
            var workflowTasks = this.store.get("$page.currentWorkflow.tasks");

            var regexRefName = new RegExp("^[A-Za-z]{1,}[A-Za-z0-9_]*$");
            var message = "";
            var setOfTaskRefNames = new Set();
            for (let i = 0; i < workflowTasks.length; i++) {
                if (!message.includes("Incorrect task reference name. ")) {
                    if (workflowTasks[i].taskReferenceName == null) {
                        message += "Incorrect task reference name. ";
                    } else {
                        if (!regexRefName.test(workflowTasks[i].taskReferenceName)) {
                            message += "Incorrect task reference name. ";
                            setOfTaskRefNames.clear();
                        }

                        setOfTaskRefNames.add(workflowTasks[i].taskReferenceName);
                    }
                }

                if (workflowTasks[i].type == null && !message.includes("You need to select task type for every task.")) {
                    message += "You need to select task type for every task. ";
                }

                for (let j = 0; j < workflowTasks[i].inputs.length; j++) {
                    if (
                        (workflowTasks[i].inputs[j].sourceDecision == null || workflowTasks[i].inputs[j].paramDecision == null) &&
                        !message.includes("You need to select for every input a source and parameter of that source. ")
                    ) {
                        message += "You need to select for every input a source and parameter of that source. ";
                    }

                    if (
                        workflowTasks[i].inputs[j].sourceDecision != null &&
                        !message.includes("Invalid task reference name used[" + IGNORE_OUTPUTKEYS + "]. ") &&
                        workflowTasks[i].inputs[j].source[workflowTasks[i].inputs[j].sourceDecision].text.includes(IGNORE_OUTPUTKEYS)
                    ) {
                        message += "Invalid task reference name used[" + IGNORE_OUTPUTKEYS + "]. ";
                    }
                }

                for (let j = 0; j < workflowTasks[i].conditions.length; j++) {
                    if (
                        (workflowTasks[i].conditions[j].sourceDecision == null || workflowTasks[i].conditions[j].paramDecision == null) &&
                        !message.includes(
                            "You need to select for every input[of the condition task] a source and parameter of that source. "
                        ) &&
                        workflowTasks[i].expression != null
                    ) {
                        message += "You need to select for every input[of the condition task] a source and parameter of that source. ";
                    }
                }

                for (let j = 0; j < workflowTasks[i].conditions.length; j++) {
                    if (
                        workflowTasks[i].expression != null &&
                        !workflowTasks[i].expression.includes(FIXED_CHARS + workflowTasks[i].conditions[j].tab) &&
                        !message.includes("Condition input parameters are missing in expression. ")
                    ) {
                        message += "Condition input parameters are missing in expression. ";
                    }
                }
            }

            if (setOfTaskRefNames.size != workflowTasks.length && !message.includes("Incorrect task reference name. ")) {
                message += "Multiple tasks with same reference name. ";
            }

            if (message !== "") {
                MsgBox.alert(message);
                return false;
            }

            return true;
        } catch (err) {}

        return false;
    }
}

const BACKEND_REQUEST_REGISTER_WORKFLOW = "/workflows";
const taskTypes = [{ id: 0, text: "SIMPLE" }];
const IGNORE_OUTPUTKEYS = "<INSERT_REF_NAME>.output";
const FIXED_CHARS = "$.";
