import { ContentResolver, LabelsLeftLayout, Repeater, UseParentLayout, bind, computable, expr } from "cx/ui";
import { Button, DateTimeField, Grid, Heading, LookupField, PureContainer, Section, TextField, ValidationGroup, Window } from "cx/widgets";
import { encodeDateWithTimezoneOffset } from "cx/util";
import Controller from "./Controller";
import { CodeMirror } from "../../components/CodeMirror";

export default () => (
    <cx>
        <div controller={Controller}>
            <div className="flex">
                <Section title="Add automation" className="">
                    <ValidationGroup invalid-bind="$page.invalid">
                        <div className="flex flex-col gap-5">
                            <div className="flex gap-10">
                                <div className=" flex-col" layout={LabelsLeftLayout}>
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
                                </div>
                                <div className="flex-col">
                                    <Heading
                                        text="Parameters"
                                        style={{ marginBottom: 10 }}
                                        visible-expr={"Object.keys({$page.automation.inputParameters}).length > 0"}
                                    />
                                    <Repeater records={computable("$page.automation.inputParameters", (params) => Object.keys(params))}>
                                        <ContentResolver
                                            params={{
                                                name: bind("$record"),
                                            }}
                                            onResolve={({ name }) => {
                                                if (!name) return;

                                                return (
                                                    <cx>
                                                        <LabelsLeftLayout>
                                                            <TextField
                                                                value-bind={`$page.automation.inputParameters.${name}`}
                                                                label={name}
                                                                required
                                                            />
                                                        </LabelsLeftLayout>
                                                    </cx>
                                                );
                                            }}
                                        />
                                    </Repeater>
                                </div>
                            </div>
                            <div className="flex-1">
                                <Button
                                    className="float-left"
                                    text="Add automation"
                                    onClick="addAutomation"
                                    icon="plus"
                                    mod="primary"
                                    disabled-bind="$page.invalid"
                                />
                            </div>
                        </div>
                    </ValidationGroup>
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
        field: "workflow.name",
        sortable: true,
    },
    {
        header: "Trigger",
        field: "trigger.name",
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
    {
        header: "Delete",
        items: (
            <cx>
                <Button icon="trash" mod="hollow" className="hover:text-red-600" onClick="deleteAutomation" />
            </cx>
        ),
    },
];

const unitEnums = [
    { id: 0, name: "DAYS" },
    { id: 1, name: "HOURS" },
    { id: 2, name: "MINUTES" },
];
