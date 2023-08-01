import { Controller, LabelsLeftLayout, PureContainer, UseParentLayout } from "cx/ui";
import { encodeDateWithTimezoneOffset } from "cx/util";
import { Button, DateTimeField, LookupField, NumberField, Section, TextField, Window } from "cx/widgets";
import { POST, PUT } from "../../api/util/methods";

const getController = (resolve) =>
    class extends Controller {
        onInit(): void {}
        async addPeriodicTrigger() {
            try {
                await POST(`/triggers/PeriodicTrigger`, this.store.get("trigger"));
                resolve(true);
            } catch (err) {
                resolve(false);
            }
            // @ts-expect-error
            this.instance.dismiss();
        }
    };

export const openAddPeriodicTriggerWindow = () =>
    new Promise((resolve) => {
        {
            let w = Window.create(
                <cx>
                    <Window
                        title="Add Periodic Trigger"
                        center
                        modal
                        closeOnEscape
                        autoFocus
                        controller={getController(resolve)}
                        onDestroy={() => resolve(false)}
                    >
                        <Section>
                            <div className="flex flex-col flex-1 items-center space-y-5">
                                <LabelsLeftLayout>
                                    <PureContainer layout={UseParentLayout}>
                                        <TextField value-bind="trigger.name" label="Name" required />
                                        <DateTimeField
                                            encoding={encodeDateWithTimezoneOffset}
                                            value-bind="trigger.start"
                                            label="Start"
                                            required
                                        />
                                        <NumberField value-bind="trigger.period" label="Period" required />
                                        <LookupField label="Unit" options={unitEnums} optionTextField="name" value-bind="trigger.unit" />
                                    </PureContainer>
                                    <div putInto="footer" className="flex justify-end gap-2">
                                        <Button text="Add trigger" icon="plus" mod="primary" onClick="addPeriodicTrigger" />
                                        <Button text="Cancel" dismiss />
                                    </div>
                                </LabelsLeftLayout>
                            </div>
                        </Section>
                    </Window>
                </cx>
            );
            w.open();
        }
    });

const unitEnums = [
    { id: 0, name: "DAYS" },
    { id: 1, name: "HOURS" },
    { id: 2, name: "MINUTES" },
];
