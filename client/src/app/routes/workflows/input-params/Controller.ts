import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import WorkflowVariables from "../WorkflowVariables";

export default class extends Controller {
    onInit(): void {
        if (!this.store.get("$page.currentWorkflowInUndoneList")) {
            this.store.set("$task.inputs", []);

            var arrInputParam = this.store.get("$task.inputParameters");
            var arr = [];
            Object.keys(arrInputParam).forEach(function (key) {
                var arrSourceParts = arrInputParam[key].slice(2, arrInputParam[key].length - 1).split(".");
                var sourceString = WorkflowVariables.DEFAULT_SIGN;
                var paramString = arrSourceParts[arrSourceParts.length - 1];

                for (let i = 0; i < arrSourceParts.length - 1; i++) {
                    sourceString += arrSourceParts[i] + ".";
                }

                if (sourceString !== WorkflowVariables.DEFAULT_SIGN) {
                    sourceString = sourceString.slice(0, sourceString.length - 1); //remove the last dot
                }

                arr.push({
                    tab: key,
                    source: sourceString,
                    param: paramString,
                });
            });

            this.store.set("$task.inputs", arr);
        }
    }
}
