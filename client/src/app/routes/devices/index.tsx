import { createAccessorModelProxy } from "cx/data";
import { FirstVisibleChildLayout, Instance, PureContainer, computable, expr } from "cx/ui";
import { Button, Grid, GridColumnConfig, Icon, Link, Section } from "cx/widgets";
import { ButtonMod } from "../../types/buttonMod";
import { Icons } from "../../types/icons";
import { Status } from "../../types/status";
import Controller from "./Controller";
import { DevicesPageModel } from "./page";

let { $page, $log, $device, $capability } = createAccessorModelProxy<DevicesPageModel>();
const gridColumns = [
    {
        header: "Type",
        align: "center",
        field: "$record.type",
        defaultWidth: 60,
        items: (
            <cx>
                <Icon
                    name={computable("$record.type", (type) => {
                        if (type === "SOCKET") return "pause-circle";
                        else if (type === "LIGHTBULB") return "light-bulb";
                        else if (type === "SENSOR") return "signal";
                        else if (type === "BUTTON") return "play-circle";
                    })}
                />
            </cx>
        ),
    },
    {
        header: "Device name",
        field: "$record.name",
        items: (
            <cx>
                <Link
                    href-tpl="~/devices/{$record.id}"
                    url-bind="url"
                    text-bind="$record.name"
                    className="text-blue-400 hover:text-blue-600"
                />
            </cx>
        ),
    },
    {
        header: "State",
        field: "$record.state.state",
        align: "center",
        defaultWidth: 150,
        items: (
            <cx>
                <span
                    className="text-slate-500"
                    text={computable("$record", (record) => {
                        if (record.state) {
                            if (record.state.state) return "ON";
                            else return "OFF";
                        } else if (record.temperature) {
                            return record.temperature.value + " ˚C / " + record.humidity.value + " %";
                        } else if (record.contact) {
                            if (!record.contact.value) return "CLOSED";
                            else return "OPEN";
                        }
                    })}
                />
            </cx>
        ),
    },
] as GridColumnConfig[];
export default () => (
    <cx>
        <div controller={Controller}>
            <Section
                header={
                    <cx>
                        <div
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                            }}
                        >
                            <h3 className="text-slate-600">Devices</h3>
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

                    <Grid records-bind={$page.devices} columns={gridColumns} scrollable />
                </FirstVisibleChildLayout>
            </Section>
        </div>
    </cx>
);
