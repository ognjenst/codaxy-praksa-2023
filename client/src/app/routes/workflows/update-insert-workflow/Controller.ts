import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default (reslove, props) =>
    class extends Controller {
        onInit(): void {
            //flagShow is used for not showing row-expanded

            this.store.set("$insert.workflowParamNames", []);
            this.store.set("$insert.workflowTasks", []);
            this.store.set("$page.insertUpdateName", props.name);
            this.store.set("$page.insertUpdateDescription", props.description);
            this.store.set("$page.insertUpdateVersion", props.version);

            this.store.set("$page.className", "grid sm:grid-cols-1 mt-2");

            this.loadData();
        }

        addParam() {
            this.store.set("$insert.workflowParamNames", [
                ...this.store.get("$insert.workflowParamNames"),
                this.store.get("$insert.singleParamName"),
            ]);

            this.store.set("$insert.singleParamName", "");
        }

        createWorkflowInfo() {
            var arrObject = {
                name: this.store.get("$page.insertUpdateName"),
                description: this.store.get("$page.insertUpdateDescription"),
                version: this.store.get("$page.insertUpdateVersion"),
                tasks: this.store.get("$insert.workflowTasks"),
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
            var arrFill = [
                {
                    tab: "Input1",
                    param: [
                        {
                            id: 1,
                            text: "Input1",
                        },
                    ],
                },
                {
                    tab: "Input2",
                    param: [
                        {
                            id: 1,
                            text: "Input2",
                        },
                    ],
                },
            ];

            try {
                let resp = await GET("/workflows/getalltasks");

                var arrTasks = [];
                for (let i = 0; i < resp.length; i++) {
                    var arrInputs = [];
                    for (let j = 0; j < resp[i].inputKeys.length; j++) {
                        arrInputs.push({
                            tab: resp[i].inputKeys[j],
                            param: [
                                {
                                    id: 1,
                                    text: resp[i].inputKeys[j],
                                },
                            ],
                        });
                    }

                    var obj = {
                        name: resp[i].name,
                        flagShow: false,
                        inputs: arrInputs,
                        conditions: arrFill,
                    };

                    arrTasks.push(obj);
                }

                this.store.set("$insert.arrTasks", arrTasks);
            } catch (err) {
                console.error(err);
            }
        }
    };
