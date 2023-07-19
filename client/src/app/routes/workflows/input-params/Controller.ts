import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class extends Controller {
    onInit(): void {
        let inputBindings = [
            {
                tab: "DeviceIP",
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
                tab: "NumberOfRepetitions",
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
        ];

        this.store.set("intro.core.inputBindings", inputBindings);
    }
}
