import { Controller } from 'cx/ui';
import { GET } from '../../../api/util/methods';

export default class extends Controller {
    onInit(): void {
        let arr = [
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

        this.store.set("$page.selecteTab", "Input1");
        this.store.set("$page.condition.arr", arr);
    }

    getRandomNumber(min, max):any{
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
};