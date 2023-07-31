import { Controller } from "cx/ui";
import { GET } from "../../../../api/util/methods";

export default class extends Controller {
    onInit(): void {
        this.addTrigger("t1", ["$task.taskReferenceName"], (task) => {
            var arrTasks = this.store.get("$page.currentWorkflow.tasks");
            var arrPrevious = [];

            for (let i = 0; i < arrTasks.length; i++) {
                var arrConditions = arrTasks[i].conditions;
                //here i need for every task(inside their inputs) to fill task ref name if it is defined
                for (let j = 0; j < arrPrevious.length; j++) {
                    if (arrPrevious[j].value != null) {
                        var arrInputs = this.store.get("$page.currentWorkflow.tasks." + i + ".inputs");

                        for (let k = 0; k < arrInputs.length; k++) {
                            var arrSource = this.store.get("$page.currentWorkflow.tasks." + i + ".inputs." + k + ".source");

                            for (let ii = 0; ii < arrSource.length; ii++) {
                                if (arrSource[ii].id === arrPrevious[j].id) {
                                    arrSource[ii].text = arrPrevious[j].value + ".output";
                                    break;
                                }
                            }

                            this.store.set("$page.currentWorkflow.tasks." + i + ".inputs." + k + ".source", arrSource);
                        }

                        for (let k = 0; k < arrConditions.length; k++) {
                            var arrSource = this.store.get("$page.currentWorkflow.tasks." + i + ".conditions." + k + ".source");

                            for (let ii = 0; ii < arrSource.length; ii++) {
                                if (arrSource[ii].id === arrPrevious[j].id) {
                                    arrSource[ii].text = arrPrevious[j].value + ".output";
                                    break;
                                }
                            }

                            this.store.set("$page.currentWorkflow.tasks." + i + ".conditions." + k + ".source", arrSource);
                        }
                    }
                }

                arrPrevious.push({
                    id: i + 1,
                    value: arrTasks[i].taskReferenceName,
                });
            }
        });
    }
}
