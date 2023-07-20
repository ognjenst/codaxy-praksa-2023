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
            { name: "Task 3", flagShow: false, conditions: arrFill, inputs: arrInput },
        ];

        this.store.set("$page.arrTasks", arr);
    }

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
}
