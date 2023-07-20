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
            { name: "Task 4", flagShow: false, conditions: arrFill, inputs: arrInput },
            { name: "Task 5", flagShow: false, conditions: arrFill, inputs: arrInput },
            { name: "Task 6", flagShow: false, conditions: arrFill, inputs: arrInput },
        ];

        let workflows = [
            {
                name: "Morning routine 1",
                version: 0,
                enabled: false,
                createdAt: "0001-01-01T00:00:00",
                updatedAt: "0001-01-01T00:00:00",
                tasks: arr,
            },
            {
                name: "Mail received 2",
                version: 0,
                enabled: false,
                createdAt: "0001-01-01T00:00:00",
                updatedAt: "0001-01-01T00:00:00",
                tasks: arr,
            },
            {
                name: "Locked lab mode 3",
                version: 0,
                enabled: false,
                createdAt: "0001-01-01T00:00:00",
                updatedAt: "0001-01-01T00:00:00",
                tasks: arr,
            },
        ];

        this.store.set("$page.undoneWorkflows", workflows);
    }

    async loadData() {
        console.log("load data");
        try {
            let resp = await GET("/workflows");
            this.store.set("$page.workflows", resp);
        } catch (err) {
            console.log(err);
        }
    }
}
