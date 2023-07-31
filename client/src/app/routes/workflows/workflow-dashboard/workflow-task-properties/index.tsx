import { LabelsLeftLayout, LabelsTopLayout, computable } from "cx/ui";
import { LookupField, TextField } from "cx/widgets";
import InputParams from "../../input-params";
import ConditionExecution from "../../condition-execution";
import Controller from "./Controller";

export default () => (
    <cx>
        <div
            className="grid md:grid-cols-1 lg:grid-cols-2 gap-5"
            controller={Controller}
            if-expr="{$page.currentWorkflowInUndoneList} == true"
        >
            <div className="flex items-center justify-middle gap-2">
                <TextField label="Reference name: " value-bind="$task.taskReferenceName" />
            </div>
            <div className="flex items-center justify-middle gap-2">
                <LookupField label="Task Type" className="flex-1" value-bind="$task.type" options={taskTypes} />
            </div>
            <div className="gap-2 -mt-3">
                <InputParams />
            </div>
            <div className="flex items-center justify-middle gap-2 md:ml-0 lg:ml-10">
                <ConditionExecution />
            </div>
        </div>

        <div className="grid md:grid-cols-1 gap-5" controller={Controller} if-expr="{$page.currentWorkflowInUndoneList} == false">
            <div className="grid md:grid-cols-2 sm:grid-cols-1 gap-5 flex items-center justify-middle">
                <div className="flex items-center justify-middle gap-2">
                    <TextField label="Reference name: " value-bind="$task.taskReferenceName" readOnly />
                </div>
                <div className="flex items-center justify-middle gap-2">
                    <TextField value-bind="$task.type" label="Task Type" className="flex-1" readOnly />
                </div>
            </div>
            <div className="gap-2 -mt-3">
                <InputParams />
            </div>
        </div>
    </cx>
);

const taskTypes = [{ id: 0, text: "SIMPLE" }];
