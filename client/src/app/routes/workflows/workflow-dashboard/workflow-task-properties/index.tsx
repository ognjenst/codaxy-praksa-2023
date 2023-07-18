import { LabelsLeftLayout, LabelsTopLayout } from "cx/ui";
import { LookupField, TextField } from "cx/widgets";

export default () => (
    <cx>
        <div className="grid grid-cols-2 gap-5">
            <div className="flex items-center justify-middle gap-2">
                <TextField label="Reference name: " value-bind="$page.text" required />
            </div>
            <div className="flex items-center justify-middle gap-2">
                <LookupField label="Task" className="flex-1" value-bind="$page.task.type" options={taskTypes} required />
            </div>
            <div className="flex items-center justify-middle gap-2">
                <LookupField label="Task type: " value-bind="$page.task.type" options={taskTypes} required />
            </div>
            <div className="flex items-center justify-middle gap-2">
                <LookupField label="Task type: " value-bind="$page.task.type" options={taskTypes} required />
            </div>
        </div>
    </cx>
);

const taskTypes = [
    { id: 1, text: "PingDevice 1" },
    { id: 2, text: "PingDevice 2" },
];
