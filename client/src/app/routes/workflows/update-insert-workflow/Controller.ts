import { Controller } from "cx/ui";

export default (reslove, props) =>
    class extends Controller {
        onInit(): void {
            var arrFill = [
                {
                    tab: "Input1",
                    source: [
                        {
                            id: 1,
                            text: "one",
                        },
                        {
                            id: 2,
                            text: "two",
                        },
                    ],

                    param: [
                        {
                            id: 1,
                            text: "one",
                        },
                        {
                            id: 2,
                            text: "two",
                        },
                    ],
                },
                {
                    tab: "Input2",
                    source: [
                        {
                            id: 1,
                            text: "one 2",
                        },
                        {
                            id: 2,
                            text: "two 2",
                        },
                    ],
                    param: [
                        {
                            id: 1,
                            text: "one 2",
                        },
                        {
                            id: 2,
                            text: "two 2",
                        },
                    ],
                },
            ];

            let arrInput = [
                {
                    tab: "DeviceIP",
                    source: [
                        {
                            id: 1,
                            text: "one 1",
                        },
                        {
                            id: 2,
                            text: "two 1",
                        },
                    ],

                    param: [
                        {
                            id: 1,
                            text: "one 1",
                        },
                        {
                            id: 2,
                            text: "two 1",
                        },
                    ],
                },
                {
                    tab: "NumberOfRepetitions",
                    source: [
                        {
                            id: 1,
                            text: "one 2",
                        },
                        {
                            id: 2,
                            text: "two 2",
                        },
                    ],
                    param: [
                        {
                            id: 1,
                            text: "one 2",
                        },
                        {
                            id: 2,
                            text: "two 2",
                        },
                    ],
                },
            ];

            var arr = [
                {
                    name: "Task 1",
                    flagShow: false,
                    conditions: arrFill,
                    inputs: arrInput,
                },
                { name: "Task 2", flagShow: false, conditions: arrFill, inputs: arrInput },
                { name: "Task 3", flagShow: false, conditions: arrFill, inputs: arrInput },
                { name: "Task 4", flagShow: false, conditions: arrFill, inputs: arrInput },
                { name: "Task 5", flagShow: false, conditions: arrFill, inputs: arrInput },
                { name: "Task 6", flagShow: false, conditions: arrFill, inputs: arrInput },
            ];

            this.store.set("$insert.arrTasks", arr);
            this.store.set("$insert.workflowParamNames", []);
            this.store.set("$insert.workflowTasks", []);
            this.store.set("$page.insertUpdateName", props.name);
            this.store.set("$page.insertUpdateDescription", props.description);
            this.store.set("$page.insertUpdateVersion", props.version);
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
        }

        addTaskToController(taskInfo) {
            this.store.set("$insert.workflowTasks", [...this.store.get("$insert.workflowTasks"), taskInfo]);
        }
    };
