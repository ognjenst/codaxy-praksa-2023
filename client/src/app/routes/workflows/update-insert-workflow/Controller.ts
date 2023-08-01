import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import { MsgBox } from "cx/widgets";

export default (reslove, props) =>
    class extends Controller {
        onInit(): void {
            //flagShow is used for not showing row-expanded

            this.store.set("$insert.workflowParamNames", []);
            this.store.set("$insert.workflowTasks", []);
            this.store.set("$page.insertUpdateName", props.name);
            this.store.set("$page.flagShowCorrectTextField", props.name == null ? false : true);
            this.store.set("$page.insertUpdateDescription", props.description);
            this.store.set("$page.insertUpdateVersion", props.version);

            this.loadData();
        }

        addParam() {
            if (this.store.get("$insert.workflowParamNames").includes(this.store.get("$insert.singleParamName"))) {
                MsgBox.alert("You already defined this workflow input param name!!!");

                return;
            }

            this.store.set("$insert.workflowParamNames", [
                ...this.store.get("$insert.workflowParamNames"),
                this.store.get("$insert.singleParamName"),
            ]);

            this.store.set("$insert.singleParamName", "");
        }

        createWorkflowInfo() {
            if (!this.checkValididy()) {
                return;
            }

            var arrTaskResp = [];
            var arrImplGlobal = []; //this is for remembering output keys from previous tasks if they have
            var selectedTasks = this.store.get("$insert.workflowTasks");
            let workflowParamNames = this.store.get("$insert.workflowParamNames");

            var arrParam = workflowParamNames.map((text, id) => ({ id, text }));
            for (let i = 0; i < selectedTasks.length; i++) {
                var arrInputs = [];

                if (i > 0 && selectedTasks[i - 1].outputKeys.length > 0) {
                    var arrImpl = [];
                    for (let j = 0; j < selectedTasks[i - 1].outputKeys.length; j++) {
                        arrImpl.push({
                            id: j,
                            text: selectedTasks[i - 1].outputKeys[j],
                        });
                    }

                    arrImplGlobal.push(arrImpl);

                    var num = 0;
                    var objOutput = [
                        {
                            id: num,
                            text: "$workflow.input",
                            param: arrParam,
                        },
                    ];

                    var conditionObject = [
                        {
                            id: num++,
                            text: "$workflow.input",
                            param: arrParam,
                        },
                    ];

                    for (let j = 0; j < arrImplGlobal.length; j++) {
                        objOutput.push({
                            id: num,
                            text: IGNORE_OUTPUTKEYS,
                            param: arrImplGlobal[j],
                        });

                        conditionObject.push({
                            id: num++,
                            text: IGNORE_OUTPUTKEYS,
                            param: arrImplGlobal[j],
                        });
                    }
                } else {
                    objOutput = [
                        {
                            id: 0,
                            text: "$workflow.input",
                            param: arrParam,
                        },
                    ];

                    conditionObject = [
                        {
                            id: 0,
                            text: "$workflow.input",
                            param: arrParam,
                        },
                    ];
                }

                for (let j = 0; j < selectedTasks[i].inputKeys.length; j++) {
                    arrInputs.push({
                        tab: selectedTasks[i].inputKeys[j],
                        source: objOutput,
                    });
                }

                var arrFill = [];
                arrFill.push({
                    tab: "Input1",
                    source: conditionObject,
                });

                var obj = {
                    name: selectedTasks[i].name,
                    flagShow: false,
                    inputs: arrInputs,
                    conditions: arrFill,
                };

                arrTaskResp.push(obj);
            }

            var arrObject = {
                name: this.store.get("$page.insertUpdateName"),
                description: this.store.get("$page.insertUpdateDescription"),
                version: this.store.get("$page.insertUpdateVersion"),
                inputParameters: this.store.get("$insert.workflowParamNames"),
                tasks: arrTaskResp,
            };

            reslove(arrObject);
        }

        deleteTaskFromController(ind) {
            this.store.set(
                "$insert.workflowTasks",
                this.store.get("$insert.workflowTasks").filter((value, index, arr) => {
                    if (index === ind) {
                        return false;
                    }

                    return true;
                })
            );

            if (this.store.get("$insert.workflowTasks").length == 0) {
                this.store.set("$page.className", "grid sm:grid-cols-1 mt-2");
            }
        }

        addTaskToController(taskInfo) {
            this.store.set("$insert.workflowTasks", [...this.store.get("$insert.workflowTasks"), taskInfo]);

            this.store.set("$page.className", "grid grid-cols-2 mt-2");
        }

        async loadData() {
            try {
                let resp = await GET(BACKEND_REQUEST_GET_ALL_TASKS);

                this.store.set("$insert.arrTasks", resp);
            } catch (err) {
                console.error(err);
            }
        }

        checkValididy() {
            //name and version are checked using validation group
            //description we do not check
            //you need at least one task in workflow and that is checked in this function
            //you can have any number of workflow param names but they need to follow specific regex expression

            var message = "";
            if (this.store.get("$insert.workflowTasks").length == 0) {
                message += "You need to select at least one task for workflow.\n";
            }

            if (message !== "") {
                MsgBox.alert(message);
            }

            return message !== "" ? false : true;
        }
    };

const BACKEND_REQUEST_GET_ALL_TASKS = "/workflows/getalltasks";
const IGNORE_OUTPUTKEYS = "<FILL_REF_NAME>.output";
