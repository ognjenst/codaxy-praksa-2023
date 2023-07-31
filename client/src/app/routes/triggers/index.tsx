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
                    <Heading text={"Periodic Triggers"}></Heading>
                    <Button
                        className="ml-10"
                        text="Add trigger"
                        icon="plus"
                        mod="primary"
                        onClick={(e, { store }) => {
                            store.set("$page.addPeriodicTrigger.visible", true);
                        }}
                    />
                </div>
                <Window
                    title="Add Periodic Trigger"
                    visible={{ bind: "$page.addPeriodicTrigger.visible", defaultValue: false }}
                    center
                    modal
                    closeOnEscape
                >
                    <Section>
                        <div className="flex-1">
                            <LabelsLeftLayout>
                                <PureContainer layout={UseParentLayout}>
                                    <TextField value-bind="$page.trigger.name" label="Name" required />
                                    <DateTimeField
                                        encoding={encodeDateWithTimezoneOffset}
                                        value-bind="$page.trigger.start"
                                        label="Start"
                                        required
                                    />
                                    <TextField value-bind="$page.trigger.period" label="Period" required />
                                    <LookupField label="Unit" options={unitEnums} optionTextField="name" value-bind="$page.trigger.unit" />
                                </PureContainer>
                                <Button text="Add trigger" icon="plus" mod="primary" />
                            </LabelsLeftLayout>
                        </div>
                    </Section>
                </Window>
                <div className="px-8 flex-1 overflow-y-auto">
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
                    <Heading text={"IoT Triggers"}></Heading>
                    <Button
                        className="ml-10"
                        text="Add trigger"
                        icon="plus"
                        mod="primary"
                        onClick={(e, { store }) => {
                            store.set("$page.addIoTTrigger.visible", true);
                        }}
                    />
                </div>
                <Window
                    title="Add IoT Trigger"
                    visible={{ bind: "$page.addIoTTrigger.visible", defaultValue: false }}
                    center
                    modal
                    closeOnEscape
                >
                    <Section>
                        <div className="flex-1">
                            <LabelsLeftLayout>
                                <PureContainer layout={UseParentLayout}>
                                    <TextField value-bind="$page.trigger.name" label="Name" required />
                                    <TextField value-bind="$page.trigger.property" label="Property" required />
                                    <TextField value-bind="$page.trigger.value" label="Value" required />
                                    <LookupField
                                        label="Condition"
                                        options={operators}
                                        optionTextField="name"
                                        value-bind="$page.trigger.condition"
                                    />
                                </PureContainer>
                                <Button text="Add trigger" icon="plus" mod="primary" />
                            </LabelsLeftLayout>
                        </div>
                    </Section>
                </Window>
                <div className="px-8 flex-1 overflow-y-auto">
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
];

const unitEnums = [
    { id: 0, name: "DAYS" },
    { id: 1, name: "HOURS" },
    { id: 2, name: "MINUTES" },
];

const operators = [
    { id: 0, name: "=" },
    { id: 1, name: "<" },
    { id: 2, name: ">" },
    { id: 3, name: ">=" },
    { id: 4, name: "<=" },
];
