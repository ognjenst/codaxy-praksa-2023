import { LabelsLeftLayout, UseParentLayout } from "cx/ui";
import { Button, DateTimeField, Grid, LookupField, PureContainer, Section, TextField, ValidationGroup, Window } from "cx/widgets";
import { encodeDateWithTimezoneOffset } from "cx/util";
import Controller from "./Controller";

export default () => (
    <cx>
        <div controller={Controller}>
            <div className="flex">
                <Section title="Add automation">
                    <div className="flex flex-row">
                        <ValidationGroup invalid-bind="$page.invalid" layout={LabelsLeftLayout}>
                            <LookupField
                                label="Select trigger type"
                                options={options}
                                optionTextField="name"
                                value-bind="$page.automation.triggerType"
                                required
                            />

                            <LookupField
                                label="Select trigger"
                                options-bind="$page.periodicTriggers"
                                optionTextField="name"
                                value-bind="$page.automation.iotId"
                                visible-expr="{$page.automation.triggerType}==1"
                                required-expr="{$page.automation.triggerType}==1 ? 'true' : 'false'"
                            />
                            <LookupField
                                label="Select trigger"
                                options-bind="$page.iotTriggers"
                                optionTextField="name"
                                value-bind="$page.automation.periodicId"
                                visible-expr="{$page.automation.triggerType}==2"
                                required-expr="{$page.automation.triggerType}==2 ? 'true' : 'false'"
                            />
                            <LookupField
                                label="Select workflow"
                                options-bind="$page.workflows"
                                optionTextField="name"
                                value-bind="$page.automation.workflowId"
                                required
                            />
                            <TextField value-bind="$page.automation.name" label="Name" required />
                            <Button text="Add automation" onClick="addAutomation" icon="plus" mod="primary" disabled-bind="$page.invalid" />
                        </ValidationGroup>
                    </div>
                </Section>
            </div>
            <div>
                <Grid
                    className="text-slate-600 h-full"
                    records-bind="$page.automations"
                    headerMode="plain"
                    sortField="time"
                    columns={automationColumns}
                    scrollable
                    buffered
                />
            </div>
        </div>
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

const unitEnums = [
    { id: 0, name: "DAYS" },
    { id: 1, name: "HOURS" },
    { id: 2, name: "MINUTES" },
];
