import { Controller } from 'cx/ui';
import { GET } from '../../../api/util/methods';

export default class extends Controller {
    onInit(): void {
        let arr = [
            {
                tab: "Input1",
                source: [
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "one",
                    },
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "two",
                    },
                ],

                param: [
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "one",
                    },
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "two",
                    },
                ],
            },
            {
                tab: "Input2",
                source: [
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "one",
                    },
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "two",
                    },
                ],
                param: [
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "one",
                    },
                    {
                        id: this.getRandomNumber(1, 1000),
                        text: "two",
                    },
                ],
            },
        ];

        this.store.set("$page.condition.arr", arr);
    }

    getRandomNumber(min, max):any{
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
};