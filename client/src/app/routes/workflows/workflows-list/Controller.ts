import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class extends Controller {
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
            { name: "Task 4", flagShow: false, conditions: arrFill, inputs: arrInput },
            { name: "Task 6", flagShow: false, conditions: arrFill, inputs: arrInput },
        ];

        this.loadData();
        // this.store.set("$page.currentWorkflow", "$page.workflows");
    }

    async loadData() {
        try {
            let resp = await GET("/workflows");
            this.store.set("$page.workflows", resp);
            this.store.set("$page.currentWorkflow", resp[0]);
        } catch (err) {
            console.error(err);
        }
    }

    itemClicked(currentWorkflow) {
        this.store.set("$page.currentWorkflow", currentWorkflow);
        this.store.set("$page.arrTasks", this.store.get("$page.currentWorkflow.tasks"));
        this.store.set("$page.currentWorkflowInUndoneList", false);
    }
}
