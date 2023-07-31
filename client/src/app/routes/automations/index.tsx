import { LabelsLeftLayout, UseParentLayout } from "cx/ui";
import { Button, DateTimeField, Grid, LookupField, PureContainer, Section, TextField } from "cx/widgets";
import { encodeDateWithTimezoneOffset } from "cx/util";
import Controller from "./Controller";

export default () => (
    <cx>
        <PureContainer controller={Controller}>
            <div className="flex">
                <Section title="Add automation">
                    <div className="flex-1">
                        <LabelsLeftLayout>
                            <LookupField
                                label="Select trigger type"
                                options={options}
                                optionTextField="name"
                                value-bind="$page.trigger.type"
                            />
                            <LookupField
                                label="Select Periodic trigger"
                                options-bind="$page.periodicTriggers"
                                optionTextField="name"
                                value-bind="$page.trigger.selected"
                                visible-expr="{$page.trigger.type}==1"
                            />
                            <LookupField
                                label="Select IoT trigger"
                                options-bind="$page.iotTriggers"
                                optionTextField="name"
                                value-bind="$page.trigger.selected"
                                visible-expr="{$page.trigger.type}==2"
                            />
                            <LookupField
                                label="Select workflow"
                                options-bind="$page.workflows"
                                optionTextField="name"
                                value-bind="$page.automation.workflows"
                            />
                            <Button text="Add automation" onClick="addAutomation" icon="plus" mod="primary" />
                        </LabelsLeftLayout>
                    </div>
                </Section>
                <Section title="Add trigger">
                    <div className="flex-1">
                        <LabelsLeftLayout>
                            <LookupField label="Trigger type" options={options} optionTextField="name" value-bind="$page.trigger.type" />

                            <PureContainer layout={UseParentLayout} visible-expr="{$page.trigger.type} == 1">
                                <DateTimeField
                                    encoding={encodeDateWithTimezoneOffset}
                                    value-bind="$page.trigger.start"
                                    label="Start"
                                    required
                                />
                                <TextField value-bind="$page.trigger.period" label="Period" required />
                                <TextField value-bind="$page.trigger.unit" label="Unit" required />
                            </PureContainer>
                            <PureContainer layout={UseParentLayout} visible-expr="{$page.trigger.type} == 2">
                                <TextField value-bind="$page.trigger.property" label="Property" required />
                                <TextField value-bind="$page.trigger.value" label="Value" required />
                                <TextField value-bind="$page.trigger.condition" label="Condition" required />
                            </PureContainer>
                            <Button text="Add trigger" icon="plus" mod="primary" />
                        </LabelsLeftLayout>
                    </div>
                </Section>
            </div>
            <div>
                <Grid
                    className="text-slate-600 h-full"
                    records-bind="$page.automation"
                    headerMode="plain"
                    sortField="time"
                    columns={automationColumns}
                    scrollable
                    buffered
                />
            </div>
        </PureContainer>
    </cx>
);

const options = [
    { id: 1, name: "Periodic Trigger" },
    { id: 2, name: "IoT Trigger" },
];

const automationColumns = [
    {
        header: "Workflow",
        field: "workflowId",
        sortable: true,
    },
    {
        header: "Trigger",
        field: "triggerId",
        sortable: true,
    },
    {
        header: "Name",
        field: "name",
        sortable: true,
    },
    {
        header: "Input Parameters",
        field: "inputParameters",
        sortable: true,
        items: (
            <cx>
                <Button text="More information" icon="document-report" onClick="showInputParameters" />
            </cx>
        ),
    },
];
