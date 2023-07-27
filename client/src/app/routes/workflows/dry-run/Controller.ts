import { Controller } from "cx/ui";

export default ({ task }) =>
    class extends Controller {
        onInit(): void {}

        getEval() {
            console.log(task);
            console.log("getEval ....");
        }
    };
