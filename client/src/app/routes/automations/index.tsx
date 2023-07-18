import { LabelsLeftLayout, UseParentLayout } from "cx/ui";
import { Button, DateTimeField, LookupField, PureContainer, Section, TextField } from "cx/widgets";
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
                                label="Select trigger"
                                options-bind="$page.triggers"
                                optionTextField="name"
                                value-bind="$page.automations.triggers"
                            />
                            <LookupField
                                label="Select workflow"
                                options-bind="$page.workflows"
                                optionTextField="name"
                                value-bind="$page.automations.workflows"
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
        </PureContainer>
    </cx>
);

const options = [
    { id: 1, name: "Periodic Trigger" },
    { id: 2, name: "IoT Trigger" },
];

/*const triggers = [
    { id: 1, text: "Trigger 1" },
    { id: 2, text: "Trigger 2" },
];*/

/*const workflows = [
    { id: 1, text: "Workflow 1" },
    { id: 2, text: "Workflow 2" },
];*/
