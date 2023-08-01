import { createAccessorModelProxy } from "cx/data";
import { FirstVisibleChildLayout, Instance, LabelsLeftLayout, PureContainer, UseParentLayout, computable, expr } from "cx/ui";
import {
    Button,
    DateTimeField,
    Grid,
    GridColumnConfig,
    Heading,
    Icon,
    Label,
    Link,
    LookupField,
    NumberField,
    Section,
    TextField,
    Window,
} from "cx/widgets";
import { ButtonMod } from "../../types/buttonMod";
import { Icons } from "../../types/icons";
import { Status } from "../../types/status";
import Controller from "./Controller";
import { encodeDateWithTimezoneOffset } from "cx/util";

export default () => (
    <cx>
        <div controller={Controller} className="p-4 flex gap-4 overflow-hidden">
            <div className="flex-1">
                <div className="flex flex-1 items-center p-5">
                    <Heading text={"Periodic Triggers"} className="text-slate-600 w-5/6"></Heading>
                    <Button
                        className="w-1/6 bg-blue-400 text-white"
                        text="Add trigger"
                        icon="plus"
                        onClick="showAddPeriodicTriggerWindow"
                    />
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
                <div className="flex flex-1 items-center p-5">
                    <Heading text={"IoT Triggers"} className="text-slate-600 w-5/6"></Heading>
                    <Button className="w-1/6 bg-blue-400 text-white" text="Add trigger" icon="plus" onClick="showAddIoTTriggerWindow" />
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
        header: "Value",
        field: "value",
        sortable: true,
    },
    {
        header: "Condition",
        field: "condition",
        sortable: true,
    },
    {
        header: "Device ID",
        field: "deviceId",
        sortable: true,
    },
];
