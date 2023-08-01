import { Controller, LabelsLeftLayout, PureContainer, UseParentLayout, computable } from "cx/ui";
import { encodeDateWithTimezoneOffset } from "cx/util";
import { Button, DateTimeField, LookupField, Section, TextField, Window } from "cx/widgets";
import { GET, POST, PUT } from "../../api/util/methods";

const getController = (resolve) =>
    class extends Controller {
        async onInit() {
            this.store.init("devices", []);
            try {
                const devices = await GET("/devices");
                this.store.set(
                    "devices",
                    devices.filter((device) => device.name != null && device.capabilities.some((item) => properties.includes(item)))
                );
            } catch (err) {
                console.error(err);
                this.store.set("devices", []);
            }
        }

        async addIoTTrigger() {
            try {
                await POST(`/triggers/IoTTrigger`, this.store.get("trigger"));
                resolve(true);
            } catch (err) {
                resolve(false);
            }
            // @ts-expect-error
            this.instance.dismiss();
        }
    };

export const openAddIoTTriggerWindow = () =>
    new Promise((resolve) => {
        {
            let w = Window.create(
                <cx>
                    <Window
                        title="Add IoT Trigger"
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
                                        <LookupField
                                            label="Device"
                                            options-bind="devices"
                                            optionTextField="name"
                                            value-bind="trigger.deviceId"
                                        />
                                        <LookupField
                                            label="Device"
                                            options={computable("trigger.deviceId", "devices", (deviceId, devices) => {
                                                const device = devices.find((d) => d.id == deviceId);
                                                if (device) {
                                                    return device.capabilities
                                                        .filter((item) => properties.includes(item))
                                                        .map((capability, id) => {
                                                            const name = `${capability[0].toUpperCase()}${capability.slice(1)}`;
                                                            return {
                                                                name,
                                                                id,
                                                            };
                                                        });
                                                } else return [];
                                            })}
                                            optionTextField="name"
                                            value-bind="trigger.property"
                                        />
                                        <LookupField
                                            label="Condition"
                                            options={operators}
                                            optionTextField="name"
                                            value-bind="trigger.condition"
                                        />
                                        <TextField value-bind="trigger.value" label="Value" required />
                                    </PureContainer>
                                </LabelsLeftLayout>
                                <div putInto="footer" className="flex justify-end gap-2">
                                    <Button text="Add trigger" icon="plus" mod="primary" onClick="addIoTTrigger" />
                                    <Button text="Cancel" dismiss />
                                </div>
                            </div>
                        </Section>
                    </Window>
                </cx>
            );
            w.open();
        }
    });

const operators = [
    { id: 0, name: "=" },
    { id: 1, name: "<" },
    { id: 2, name: ">" },
    { id: 3, name: ">=" },
    { id: 4, name: "<=" },
];

const properties = ["temperature", "humidity", "contact"];
