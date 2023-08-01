import { LabelsLeftLayout, LabelsTopLayout, computable } from "cx/ui";
import { LookupField, TextField, ValidationGroup } from "cx/widgets";
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
            <ValidationGroup invalid-bind="$page.flagRegisterWorkflow">
                <div className="flex items-center justify-middle gap-2">
                    <TextField
                        label="Reference name: "
                        value-bind="$task.taskReferenceName"
                        required
                        validationRegExp={new RegExp("^[A-Za-z]{1,}[A-Za-z0-9_]*$")}
                    />
                </div>
                <div className="flex items-center justify-middle gap-2">
                    <LookupField label="Task Type" className="flex-1" value-bind="$task.type" options={taskTypes} required />
                </div>
                <div className="gap-2 -mt-3">
                    <InputParams />
                </div>
                <div className="flex items-center justify-middle gap-2 md:ml-0 lg:ml-10">
                    <ConditionExecution />
                </div>
            </ValidationGroup>
        </div>

        <div className="grid md:grid-cols-1 gap-5" controller={Controller} if-expr="{$page.currentWorkflowInUndoneList} == false">
            <div className="grid md:grid-cols-2 sm:grid-cols-1 gap-5 flex items-center justify-middle">
                <div className="flex items-center justify-middle gap-2">
                    <TextField label="Reference name: " value-bind="$task.taskReferenceName" readOnly />
                </div>
                <div className="flex items-center justify-middle gap-2">
                    <TextField value-bind="$task.type" label="Task Type" className="flex-1" readOnly />
                </div>
                <div className="flex items-center justify-middle gap-2" if-expr="{$task.from_switch} != null">
                    <TextField label="From Switch: " value-bind="$task.from_switch" readOnly />
                </div>
                <div className="flex items-center justify-middle gap-2">
                    <TextField
                        value-bind="$task.switch_decision"
                        label="Decision Type"
                        className="flex-1"
                        readOnly
                        if-expr="{$task.switch_decision} != null"
                    />
                </div>
            </div>
            <div className="gap-2 -mt-3 flex items-center justify-middle">
                <TextField
                    layout={LabelsLeftLayout}
                    value-bind="$task.expression"
                    if-expr="{$task.expression} != null"
                    label="Task Type"
                    className="flex-1"
                    readOnly
                />
            </div>
            <div className="gap-2 -mt-3">
                <InputParams />
            </div>
        </div>
    </cx>
);

const taskTypes = [{ id: 0, text: "SIMPLE" }];
