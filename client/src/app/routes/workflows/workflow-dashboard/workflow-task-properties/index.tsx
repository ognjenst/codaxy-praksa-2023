import { LabelsLeftLayout, LabelsTopLayout } from "cx/ui";
import { LookupField, TextField } from "cx/widgets";
import InputParams from "../../input-params";
import ConditionExecution from "../../condition-execution";
import Controller from "./Controller";

export default () => (
    <cx>
        <div className="grid md:grid-cols-1 lg:grid-cols-2 gap-5" controller={Controller}>
            <div className="flex items-center justify-middle gap-2">
                <TextField label="Reference name: " value-bind="$task.taskReferenceName" />
            </div>
            <div className="flex items-center justify-middle gap-2">
                <LookupField label="Task" className="flex-1" value-bind="$task.type" options={taskTypes} />
            </div>
            <div className="gap-2 -mt-3">
                <InputParams />
            </div>
            <div className="flex items-center justify-middle gap-2 md:ml-0 lg:ml-10">
                <ConditionExecution />
            </div>
        </div>
    </cx>
);

const taskTypes = [{ id: 0, text: "SIMPLE" }];
