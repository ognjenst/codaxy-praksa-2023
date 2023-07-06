import Controller from "./Controller";
import { createAccessorModelProxy } from "cx/data";
import { DevicesPageModel } from "./page";
import { FirstVisibleChildLayout, Instance, PureContainer, Repeater, expr } from "cx/ui";
import { Button, Icon, LookupField, Section, TextArea } from "cx/widgets";
import { Status } from "../../types/status";
import { Icons } from "../../types/icons";
import { ButtonMod } from "../../types/buttonMod";

let { $page, $log, $device, $capability } = createAccessorModelProxy<DevicesPageModel>();

export default (
    <cx>
        <div controller={Controller} style={{ display: "flex", flexWrap: "wrap" }}>
            <Section
                style={{ width: "50%" }}
                header={
                    <cx>
                        <div
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                            }}
                        >
                            <h3>Devices</h3>
                            <Button
                                mod={ButtonMod.Primary}
                                icon={expr($page.status.request, (status) => (status === Status.Loading ? Icons.Loading : Icons.Refresh))}
                                onClick={(_, { controller }: Instance<DevicesPageModel, Controller>) => controller.onGetDevices()}
                                disabled={expr($page.status.request, (status) => status === Status.Loading)}
                            />
                        </div>
                    </cx>
                }
            >
                <FirstVisibleChildLayout>
                    <PureContainer if={expr($page.status.request, (status) => status === Status.Loading)}>
                        <Icon name={Icons.Loading} style={{ paddingRight: "0.5rem" }} />
                        <span>Loading...</span>
                    </PureContainer>
                    <Repeater records={$page.devices} recordAlias={$device}>
                        <div>
                            <span>ID: </span>
                            <span text={$device.id} />
                            <span style={{ paddingLeft: "0.5rem" }}> Capabilities: </span>
                            <Repeater records={$device.capabilities} recordAlias={$capability}>
                                <span text={$capability} />
                                <span ws> </span>
                            </Repeater>
                        </div>
                    </Repeater>
                </FirstVisibleChildLayout>
            </Section>
            <Section
                style={{ width: "50%" }}
                header={
                    <cx>
                        <div
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                            }}
                        >
                            <h3>Send payload</h3>
                            <LookupField options={$page.devices} optionTextField="id" value={$page.selectedDeviceId} />
                            <Button
                                mod={ButtonMod.Primary}
                                text="Send"
                                onClick={(_, { controller }: Instance<DevicesPageModel, Controller>) => controller.onSendPayload()}
                                disabled={expr($page.selectedDeviceId, (id) => !id)}
                            />
                        </div>
                    </cx>
                }
            >
                <TextArea value={$page.devicePayload} style={{ width: "100%", height: 200 }} />
            </Section>
            <Section title="Device state history" style={{ width: "100%" }}>
                <Repeater sortField="order" sortDirection="DESC" records={$page.deviceLog} recordAlias={$log}>
                    <div>
                        <span text={$log.order} />
                        <span style={{ paddingLeft: "0.5rem" }} text={$log.id} />
                        <span style={{ paddingLeft: "0.5rem" }} text={expr($log, (log) => JSON.stringify(log))} />
                    </div>
                </Repeater>
            </Section>
        </div>
    </cx>
);
