import { Controller } from "cx/ui";

export default (task) =>
    class extends Controller {
        onInit(): void {
            this.store.set("$inputs", task.inputs);
            this.store.set("$result", "");
        }

        getEval() {
            try {
                var expr = task.expression;

                var arr = this.store.get("$inputs");
                for (let i = 0; i < arr.length; i++) {
                    expr = expr.replace(FIXED_CHARS + arr[i].tab, arr[i].value);
                }

                var result = eval(expr);
            } catch (err) {
                result = false;
            }
            this.store.set("$result", result);
        }
    };

const FIXED_CHARS = "$.";
