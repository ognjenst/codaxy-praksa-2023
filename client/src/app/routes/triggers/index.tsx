import { Button, Grid, Heading } from "cx/widgets";
import Controller from "./Controller";
import { computable } from "cx/ui";

export default () => (
    <cx>
        <div controller={Controller} className="p-4 flex gap-4 overflow-hidden">
            <div className="flex-1">
                <div className="flex  items-center p-5">
                    <div className="flex-1">
                        <Heading text={"Periodic Triggers"} className="text-slate-600 w-5/6"></Heading>
                    </div>
                    <div className="flex-1">
                        <Button
                            className="float-right bg-blue-400 text-white"
                            text="Add trigger"
                            icon="plus"
                            onClick="showAddPeriodicTriggerWindow"
                        />
                    </div>
                </div>
                <div className="px-5 flex-1 overflow-y-auto">
                    <Grid
                        className="text-slate-600 h-full"
                        records-bind="$page.periodicTriggers"
                        headerMode="plain"
                        columns={periodicTriggersColumns}
                        scrollable
                        buffered
                    />
                </div>
            </div>
            <div className="flex-1">
                <div className="flex items-center p-5">
                    <div className="flex-1">
                        <Heading text={"IoT Triggers"} className="text-slate-600 w-5/6"></Heading>
                    </div>
                    <div className="flex-1">
                        <Button
                            className="float-right bg-blue-400 text-white"
                            text="Add trigger"
                            icon="plus"
                            onClick="showAddIoTTriggerWindow"
                        />
                    </div>
                </div>
                <div className="px-5 flex-1 overflow-y-auto">
                    <Grid
                        className="text-slate-600 h-full"
                        records-bind="$page.iotTriggers"
                        headerMode="plain"
                        columns={iotTriggersColumns}
                        scrollable
                        buffered
                    />
                </div>
            </div>
        </div>
    </cx>
);

const units = ["Days", "Hours", "Minutes"];
const conditions = ["=", "<", ">", "≥", "≤"];

const periodicTriggersColumns = [
    {
        header: "Name",
        field: "name",
        sortable: true,
    },
    {
        header: "Start",
        field: "start",
        sortable: true,
        format: "datetime;ddMMMyyyyHHmmN",
    },
    {
        header: "Period",
        field: "period",
        sortable: true,
    },
    {
        header: "Unit",
        field: "unit",
        sortable: true,
        items: (
            <cx>
                <span text={computable("$record", (r) => r && units[r.unit])} />
            </cx>
        ),
    },
    {
        header: "Delete",
        items: (
            <cx>
                <Button icon="trash" mod="hollow" className="hover:text-red-600" onClick="deletePeriodicTrigger" />
            </cx>
        ),
    },
];

const iotTriggersColumns = [
    {
        header: "Name",
        field: "name",
        sortable: true,
    },
    {
        header: "Property",
        field: "property",
        sortable: true,
    },
    {
        header: "Condition",
        field: "condition",
        sortable: true,
        items: (
            <cx>
                <span text={computable("$record", (r) => r && conditions[r.condition])} />
            </cx>
        ),
    },
    {
        header: "Value",
        field: "value",
        sortable: true,
    },
    {
        header: "Device ID",
        field: "deviceId",
        sortable: true,
    },
    {
        header: "Delete",
        items: (
            <cx>
                <Button icon="trash" mod="hollow" className="hover:text-red-600" onClick="deleteIotTrigger" />
            </cx>
        ),
    },
];
